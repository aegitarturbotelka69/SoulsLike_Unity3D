using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using SLGame.Input;
using SLGame.Modules;

namespace SLGame.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] public CharacterController CharacterController;
        [SerializeField] public Transform _cameraTransform;
        [SerializeField] public Animator CharacterAnimator;

        [SerializeField]
        public Dictionary<States, CharacterControllingBaseState> CharacterControllingStates = new Dictionary<States, CharacterControllingBaseState>();

        [Header("Stats:")]
        [SerializeField] public CharacterControllingBaseState CurrentCharacterControllingState;
        [SerializeField] public float MoveSpeed = 2f;

        [Space(10)]
        [SerializeField] public float rotationSpeed = 0.1f;

        [Space(10)]

        [SerializeField] public float RollSpeedMultiplier = 2f;
        [SerializeField] public float RunSpeedMultiplier = 4f;

        [Space(10)]
        [SerializeField] public Vector3 FallVelocity;

        [Space(10)]

        /// <summary>
        /// 1000 = 1 seconds real time
        /// </summary>
        /// <param = RollCooldownDuration> duration in milliseconds</param>
        [SerializeField, Tooltip("Duration in milliseconds")] public int RollCooldownDuration = 135;
        [SerializeField] public bool RollOnCooldown = false;

        [Header("Info:")]
        [SerializeField] public float zAxis;
        [Space(2)]
        [SerializeField] public float xAxis;

        [Space(10)]

        [SerializeField] public float MoveVelocity;
        [SerializeField] public Vector3 Direction;


        private void Start()
        {
            CharacterAnimator = this.gameObject.GetComponent<Animator>();
            // !Attentione! Must be a joke below
            PlayerMovement playerMovementReference = this;
            this.CharacterController = this.gameObject.GetComponent<CharacterController>();
            CharacterController playerCharacterController = this.gameObject.GetComponent<CharacterController>();
            CharacterControllingStates.Add(States.Idle, new CharacterControllingIdleState(ref playerMovementReference, ref playerCharacterController));
            CharacterControllingStates.Add(States.Move, new CharacterControllingMoveState(ref playerMovementReference, ref playerCharacterController));
            CharacterControllingStates.Add(States.Roll, new CharacterControllingRollState(ref playerMovementReference, ref playerCharacterController));
            CharacterControllingStates.Add(States.Run, new CharacterControllingRunState(ref playerMovementReference, ref playerCharacterController));
            CharacterControllingStates.Add(States.StopRun, new CharacterControllingStopRunState(ref playerMovementReference, ref playerCharacterController));
            CharacterControllingStates.Add(States.Falling, new CharacterControllingFallState(ref playerMovementReference, ref playerCharacterController));
            CharacterControllingStates.Add(States.HardLand, new CharacterControllingHardLandState(ref playerMovementReference, ref playerCharacterController));

            CurrentCharacterControllingState = CharacterControllingStates[States.Idle];
        }

        private void Update()
        {
            CurrentCharacterControllingState.Execute();
        }

        private void EndTransition()
        {
            CurrentCharacterControllingState.EndTransition();
        }


        public void ChangeControllingState(States newState)
        {
            CurrentCharacterControllingState.EndTransition();
            CurrentCharacterControllingState = CharacterControllingStates[newState];
            CurrentCharacterControllingState.StartTransition();

        }

        public async void PlaceRollOnCooldown()
        {
            this.RollOnCooldown = true;
            await Task.Delay(RollCooldownDuration);
            this.RollOnCooldown = false;
        }
    }
}