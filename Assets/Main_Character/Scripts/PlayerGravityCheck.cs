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

        [Header("Stats: ")]
        [SerializeField] public const float Gravity = -9.81f;

        [Space(10)]

        [SerializeField] float groundDistance = 0.4f;
        [SerializeField] private float StartingVelocityNumber = -2f;
        [SerializeField] LayerMask groundMask;

        [Header("In game: ")]
        [SerializeField] public Vector3 Velocity;
        [SerializeField] bool isGrounded;
        void Start()
        {
            this._playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        }

        void Update()
        {
            isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

            if (isGrounded && Velocity.y < 0)
            {
                Velocity.y = StartingVelocityNumber;
            }

            Velocity.y += Gravity * Time.deltaTime;

            _playerMovement.CharacterController.Move(Velocity * Time.deltaTime);
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GroundCheck.position, groundDistance);
        }
    }
}