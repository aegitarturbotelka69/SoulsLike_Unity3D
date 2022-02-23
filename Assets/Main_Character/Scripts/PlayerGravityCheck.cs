using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerGravityCheck : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] Transform GroundCheck;
        [SerializeField] private BoxCollider _playerBoxCollider;

        [Space(5)]
        [SerializeField] private GameObject _colliderEdgePrefab;

        [Header("Stats: ")]
        [SerializeField] public const float Gravity = -9.81f;

        /// <summary>
        /// Falling distance range 
        /// </summary>
        [SerializeField] public float RangeAltitude;

        [Space(10)]

        [SerializeField] float groundDistance = 0.4f;
        [SerializeField] private float StartingVelocityNumber = -2f;
        [SerializeField] LayerMask groundMask;

        [Header("In game: ")]
        [SerializeField] private List<SphereWithDetectionStatus> _listOfCollisionDetectionSpheres = new List<SphereWithDetectionStatus>();
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
        [SerializeField] public Vector3 Velocity;
        [SerializeField] private Ray FallingDetectionRay;

        private void Awake()
        {
            _playerBoxCollider = this.gameObject.GetComponent<BoxCollider>();

            float left = _playerBoxCollider.bounds.center.x - _playerBoxCollider.bounds.extents.x;
            float right = _playerBoxCollider.bounds.center.x + _playerBoxCollider.bounds.extents.x;

            float bottom = _playerBoxCollider.bounds.center.y - _playerBoxCollider.bounds.extents.y;

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

            Debug.LogWarning(numberOfNotConnectedToTheGroundSpheres + " " + _listOfCollisionDetectionSpheres.Count);
            if (numberOfNotConnectedToTheGroundSpheres != _listOfCollisionDetectionSpheres.Count)
            {
                Velocity.y = StartingVelocityNumber;
            }
            else
            {
                Debug.Log("Player is falling");
            }

            Velocity.y += Gravity * Time.deltaTime;
            _playerMovement.CharacterController.Move(Velocity * Time.deltaTime);
        }

        private void PlayerIsGrounded()
        {
            foreach (SphereWithDetectionStatus sphere in _listOfCollisionDetectionSpheres)
            {
                Debug.DrawRay(sphere.Sphere.transform.position, -Vector3.up * RangeAltitude, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(sphere.Sphere.transform.position, -Vector3.up, out hit, RangeAltitude))
                {
                    Debug.LogWarning(hit.collider.gameObject.name);
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

        /// <summary>
        /// Creates an edge sphere for collision detection
        /// </summary>
        /// <param name="positionToCreate"> Position where u want to create a sphere </param>
        /// <returns> Instanciated Edge Sphere</returns>
        private GameObject CreateEdgeSphere(Vector3 positionToCreate) => Instantiate(_colliderEdgePrefab, positionToCreate, Quaternion.identity);
    }
}