using System;
using System.Collections.Generic;
using UnityEngine;
using SLGame.Input;
using SLGame.Modules;

namespace SLGame.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] public CharacterController _characterController;
        [SerializeField] public Transform _cameraTransform;
        [SerializeField] public Animator CharacterAnimator;
        [SerializeField] public Dictionary<States, CharacterControllingBaseState> CharacterControllingStates = new Dictionary<States, CharacterControllingBaseState>();

        [Header("Stats:")]
        [SerializeField] public CharacterControllingBaseState _currentCharacterControllingState;
        [SerializeField] public float MoveSpeed = 2f;
        [SerializeField] public float rotationSpeed = 0.1f;


        [Header("Info:")]
        [SerializeField] public float zAxis;
        [SerializeField] public float xAxis;

        [SerializeField] public float velocity;
        [SerializeField] public Vector3 Direction;


        public Vector3 norm;
        public Vector3 notNorm;

        private void Start()
        {
            CharacterAnimator = this.gameObject.GetComponent<Animator>();
            // ! Must be a joke
            PlayerMovement playerMovementReference = this;
            CharacterControllingStates.Add(States.Idle, new CharacterControllingIdleState(ref playerMovementReference));
            CharacterControllingStates.Add(States.Move, new CharacterControllingMoveState(ref playerMovementReference));
            CharacterControllingStates.Add(States.Roll, new CharacterControllingRollState(ref playerMovementReference));

            _currentCharacterControllingState = CharacterControllingStates[States.Idle];
        }
        private void Update()
        {
            GetHorizontalInput();
            GetVerticalInput();

            _currentCharacterControllingState.GetInput();
            _currentCharacterControllingState.Rotate();
            _currentCharacterControllingState.Move();
        }



        private void GetHorizontalInput()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.MoveBack)
            {
                zAxis = 1f;
                return;
            }
            if (VirtualInputManager.Instance.MoveBack && !VirtualInputManager.Instance.MoveFront)
            {
                zAxis = -1f;
                return;
            }

            zAxis = 0f;

        }
        private void GetVerticalInput()
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
                return;

            if (VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                xAxis = -1f;
                return;
            }
            if (VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                xAxis = 1f;
                return;
            }

            xAxis = 0f;
        }
    }
}