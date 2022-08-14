using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerGravityCheck : MonoBehaviour
    {
        [Serializable]
        private class SphereWithDetectionStatus
        {
            [SerializeField] public GameObject Sphere;
            [SerializeField] public bool isGrounded;

            public SphereWithDetectionStatus(GameObject sphere, bool status)
            {
                this.Sphere = sphere;
                this.isGrounded = status;
            }
        }

        [Header("References:")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private BoxCollider _playerBoxCollider;

        [SerializeField] private Animator _playerAnimator;

        [Space(5)]
        [SerializeField] private GameObject _colliderEdgePrefab;

        [Header("Stats: ")]
        [SerializeField] public const float Gravity = -9.81f;

        /// <summary>
        /// Falling distance range 
        /// </summary>
        [SerializeField] public float RangeAltitude;

        [Space(10)]
        [SerializeField] private float StartingVelocityNumber = -2f;

        /// <summary>
        ///  This only using for AnimatorIK
        /// </summary>
        [SerializeField, Range(-1f, 1f)] private float DistanceToGroundForFootIK;
        [SerializeField, Range(-1f, 5f)] private float MaxDistanceToGroundForFootIK;

        [SerializeField] private LayerMask _groundLayerMask;

        [Header("In game: ")]
        [SerializeField] public static bool PLAYER_IS_GROUNDED;
        [SerializeField] private List<SphereWithDetectionStatus> _listOfCollisionDetectionSpheres = new List<SphereWithDetectionStatus>();
        [SerializeField] public Vector3 Velocity;

        private void Awake()
        {
            _playerBoxCollider = this.gameObject.GetComponent<BoxCollider>();
            _playerAnimator = this.gameObject.GetComponent<Animator>();

            float left = _playerBoxCollider.bounds.center.x - _playerBoxCollider.bounds.extents.x;
            float right = _playerBoxCollider.bounds.center.x + _playerBoxCollider.bounds.extents.x;

            float bottom = _playerBoxCollider.bounds.center.y - _playerBoxCollider.bounds.extents.y + (_playerBoxCollider.bounds.extents.y / 2);

            float front = _playerBoxCollider.bounds.center.z + _playerBoxCollider.bounds.extents.z;
            float back = _playerBoxCollider.bounds.center.z - _playerBoxCollider.bounds.extents.z;

            GameObject[] edgeSpheres = new GameObject[]{
                CreateEdgeSphere(new Vector3(left, bottom, front)),
                CreateEdgeSphere(new Vector3(right, bottom, front)),
                CreateEdgeSphere(new Vector3(left, bottom, back)),
                CreateEdgeSphere(new Vector3(right, bottom, back))
            };

            foreach (GameObject sphere in edgeSpheres)
            {
                sphere.transform.parent = this.transform;
                _listOfCollisionDetectionSpheres.Add(new SphereWithDetectionStatus(sphere, false));
            }

            float frontEdgeSphereSectionOffset = (edgeSpheres[0].transform.position - edgeSpheres[2].transform.position).magnitude / 5;
            float sideEdgeSphereSectionOffset = (edgeSpheres[0].transform.position - edgeSpheres[1].transform.position).magnitude / 5;

            for (byte j = 0; j < 4; j++)
            {
                Vector3[] positionArray = new[] {
                    edgeSpheres[2].transform.position + (Vector3.forward * frontEdgeSphereSectionOffset * (j + 1)),
                    edgeSpheres[3].transform.position + (Vector3.forward * frontEdgeSphereSectionOffset * (j + 1)),
                    edgeSpheres[0].transform.position + (Vector3.right * sideEdgeSphereSectionOffset * (j + 1)),
                    edgeSpheres[2].transform.position + (Vector3.right * sideEdgeSphereSectionOffset * (j + 1))};

                foreach (Vector3 position in positionArray)
                {
                    GameObject sphere = CreateEdgeSphere(position);
                    sphere.transform.parent = this.transform;
                    _listOfCollisionDetectionSpheres.Add(new SphereWithDetectionStatus(sphere, true));
                }
            }
        }
        private void Start()
        {
            this._playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        }
        private void Update()
        {
            byte numberOfNotConnectedToTheGroundSpheres = 0;

            PlayerIsGrounded();

            foreach (SphereWithDetectionStatus sphere in _listOfCollisionDetectionSpheres)
            {
                if (sphere.isGrounded == false)
                {
                    numberOfNotConnectedToTheGroundSpheres++;
                }
            }

            if (numberOfNotConnectedToTheGroundSpheres != _listOfCollisionDetectionSpheres.Count)
            {
                Velocity.y = StartingVelocityNumber;
                PLAYER_IS_GROUNDED = true;
            }
            else
            {
                PLAYER_IS_GROUNDED = false;
                _playerMovement.ChangeControllingState(States.Falling, false);
            }

            Velocity.y += Gravity * Time.deltaTime;
            _playerMovement.gameObject.GetComponent<CharacterController>().Move(Velocity * Time.deltaTime);
        }

        private void PlayerIsGrounded()
        {
            foreach (SphereWithDetectionStatus sphere in _listOfCollisionDetectionSpheres)
            {
                Debug.DrawRay(sphere.Sphere.transform.position, -Vector3.up * RangeAltitude, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(sphere.Sphere.transform.position, -Vector3.up, out hit, RangeAltitude))
                {
                    if (hit.collider.gameObject.layer == (int)Layers.Ground)
                    {
                        sphere.isGrounded = true;
                    }
                }
                else
                {
                    sphere.isGrounded = false;
                }
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _playerAnimator.GetFloat("IKLeftFootWeight"));
            _playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _playerAnimator.GetFloat("IKLeftFootWeight"));
            _playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _playerAnimator.GetFloat("IKRightFootWeight"));
            _playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _playerAnimator.GetFloat("IKRightFootWeight"));

            // Left Fook IK
            RaycastHit hit;
            Ray ray = new Ray(_playerAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGroundForFootIK + MaxDistanceToGroundForFootIK, _groundLayerMask))
            {
                if (hit.collider.gameObject.layer == (int)Layers.Ground)
                {
                    Vector3 currentFootPosition = hit.point;
                    currentFootPosition.y += DistanceToGroundForFootIK;
                    _playerAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, currentFootPosition);
                    Vector3 forward = Vector3.ProjectOnPlane(this.gameObject.transform.forward, hit.normal);
                    _playerAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(forward, hit.normal));
                }
            }

            // Right Fook IK
            ray = new Ray(_playerAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGroundForFootIK + MaxDistanceToGroundForFootIK, _groundLayerMask))
            {
                if (hit.collider.gameObject.layer == (int)Layers.Ground)
                {
                    Vector3 currentFootPosition = hit.point;
                    currentFootPosition.y += DistanceToGroundForFootIK;
                    _playerAnimator.SetIKPosition(AvatarIKGoal.RightFoot, currentFootPosition);
                    Vector3 forward = Vector3.ProjectOnPlane(this.gameObject.transform.forward, hit.normal);
                    _playerAnimator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(forward, hit.normal));
                }
            }
        }

        /// <summary>
        /// Creates an edge sphere for collision detection
        /// </summary>
        /// <param name="positionToCreate"> Position where u want to create a sphere </param>
        /// <returns> Instanciated Edge Sphere</returns>
        private GameObject CreateEdgeSphere(Vector3 positionToCreate) => Instantiate(_colliderEdgePrefab, positionToCreate, Quaternion.identity);
    }
}