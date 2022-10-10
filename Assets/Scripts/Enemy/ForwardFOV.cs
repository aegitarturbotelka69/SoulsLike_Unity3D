using System.Collections;
using UnityEngine;

namespace SLGame.Enemy
{
    public class ForwardFOV : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private EnemyAI _enemyAI;

        /// <summary>
        /// Contains singleton player gameobject 
        /// </summary>
        /// <value> Player gameobject </value>
        [SerializeField] private Transform _playerTransform;
        public Transform PlayerTransform
        {
            get { return _playerTransform; }
            private set { _playerTransform = value; }
        }

        [Header("Stats: ")]
        [SerializeField] public float Radius;

        [SerializeField, Range(0f, 360f)] public float Angle;

        [SerializeField] private float _forwardCheckDelay;

        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstructionMask;

        [SerializeField] private float TargetInterestedTime;

        [Header("In game:")]
        [SerializeField] private float _targetInterestedRemainTime = 0f;
        [SerializeField] private bool _targetInterested = false;
        public bool TargetInterested
        {
            get { return _targetInterested; }
            private set { _targetInterested = value; }
        }
        [SerializeField] public bool CanSeePlayer = false;
        void Start()
        {
            PlayerTransform = Player.Instance.gameObject.transform;
            _enemyAI = this.gameObject.GetComponent<EnemyAI>();

            StartCoroutine(Observe());
        }

        void Update()
        {

        }

        private IEnumerator TargetInterestedRemain()
        {
            _targetInterested = true;
            _targetInterestedRemainTime = TargetInterestedTime;

            while (_targetInterestedRemainTime > 0)
            {
                if (CanSeePlayer)
                {
                    _targetInterestedRemainTime = TargetInterestedTime;
                    yield return null;
                }

                _targetInterestedRemainTime -= Time.deltaTime;
                yield return null;
            }

            _targetInterested = false;
            yield break;
        }

        private IEnumerator Observe()
        {

            while (true)
            {

                Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, _targetMask);

                if (rangeChecks.Length != 0)
                {
                    Transform targetTransform = rangeChecks[0].transform;
                    Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                        {
                            //Debug.LogWarning($"Enemy: {this.gameObject.name} Detected Player");
                            StartCoroutine(TargetInterestedRemain());
                            CanSeePlayer = true;
                            TargetInterested = true;
                            _targetInterestedRemainTime = TargetInterestedTime;
                            yield return new WaitForSeconds(_forwardCheckDelay);

                            yield return null;
                        }
                        else
                        {
                            CanSeePlayer = false;
                            yield return null;

                        }
                    }
                    else
                    {
                        CanSeePlayer = false;
                        yield return null;
                    }
                }
                CanSeePlayer = false;
                yield return null;

            }
        }
    }
}