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
        [SerializeField] public float Gravity = -9.81f;
        [SerializeField] public Vector3 Velocity;
        void Start()
        {
            this._playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            Velocity.y += Gravity * Time.deltaTime;
            _playerMovement.CharacterController.Move(Velocity * Time.deltaTime);
        }
    }
}