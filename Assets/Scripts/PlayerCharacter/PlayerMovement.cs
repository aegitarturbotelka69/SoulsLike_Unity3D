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
        [SerializeField] public CharacterControllingBaseState PreviousCharacterControllingState;
        [SerializeField] public float MoveSpeed = 2f;

        [Space(10)]
        [SerializeField] public float rotationSpeed = 0.1f;

        [Space(10)]

        [SerializeField] public float RollSpeedMultiplier = 2f;
        [SerializeField] public float RunSpeedMultiplier = 4f;

        [Space(10)]

        /// <summary>
        /// 1000 = 1 seconds real time
        /// </summary>
        /// <param = RollCooldownDuration> duration in milliseconds</param>
        [SerializeField, Tooltip("Duration in milliseconds")] public int RollCooldownDuration = 135;
        [SerializeField] public bool RollOnCooldown = false;

        [SerializeField] public bool ReadyToTransition = false;

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
            this.CharacterController = this.gameObject.GetComponent<CharacterController>();
            CharacterController playerCharacterController = this.gameObject.GetComponent<CharacterController>();

            CharacterControllingStates.Add(States.Idle, new CharacterControllingIdleState(States.Idle, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.Move, new CharacterControllingMoveState(States.Move, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.Roll, new CharacterControllingRollState(States.Roll, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.Run, new CharacterControllingRunState(States.Run, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.StopRun, new CharacterControllingStopRunState(States.StopRun, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.Falling, new CharacterControllingFallState(States.Falling, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.HardLand, new CharacterControllingHardLandState(States.HardLand, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.LightAttack, new CharacterControllingLightAttackState(States.LightAttack, this, ref playerCharacterController));
            CharacterControllingStates.Add(States.HeavyAttack, new CharacterControllingHeavyAttackState(States.HeavyAttack, this, ref playerCharacterController));

            CurrentCharacterControllingState = CharacterControllingStates[States.Idle];
        }

        private void Update()
        {
            CurrentCharacterControllingState.Execute();
        }

        /// <summary>
        /// Manual end of current state
        /// </summary>
        private void ManualEndTransaction()
        {
            Debug.LogWarning("Manually ending:" + CurrentCharacterControllingState.ToString());
            CurrentCharacterControllingState.EndTransition(true);
        }

        public void ChangeControllingState(States newState, bool endingManually = false)
        {
            CurrentCharacterControllingState.EndTransition(endingManually);
            PreviousCharacterControllingState = CurrentCharacterControllingState;
            CurrentCharacterControllingState = CharacterControllingStates[newState];
            CurrentCharacterControllingState.StartTransition();
        }

        /// <summary>
        /// Async operation that sets roll on cooldown
        /// </summary>
        /// <returns>void</returns>
        public async void PlaceRollOnCooldown()
        {
            this.RollOnCooldown = true;
            await Task.Delay(RollCooldownDuration);
            this.RollOnCooldown = false;
        }
    }
}