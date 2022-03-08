using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace SLGame.Enemy
{
    public class ForwardFOV : MonoBehaviour
    {
        [Header("Stats: ")]
        [SerializeField] public float Radius;

        [SerializeField, Range(0f, 360f)] public float Angle;

        [SerializeField] private float _forwardCheckDelay;

        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstructionMask;

        [Header("References:")]
        [SerializeField] private GameObject _playerTransform;

        [Header("In game:")]
        [SerializeField] public bool CanSeePlayer = false;
        void Start()
        {
            _playerTransform = Player.Instance.gameObject;

            StartCoroutine(Observe());
        }

        void Update()
        {

        }

        private IEnumerator Observe()
        {

            while (true)
            {
                yield return new WaitForSeconds(_forwardCheckDelay);

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
                            Debug.LogWarning($"Enemy: {this.gameObject.name} Detected Player");
                            CanSeePlayer = true;
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