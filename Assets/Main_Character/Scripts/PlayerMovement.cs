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
        [SerializeField] public CharacterController _characterController;
        [SerializeField] public Transform _cameraTransform;
        [SerializeField] public Animator CharacterAnimator;
        [SerializeField] public Dictionary<States, CharacterControllingBaseState> CharacterControllingStates = new Dictionary<States, CharacterControllingBaseState>();

        [Header("Stats:")]
        [SerializeField] public CharacterControllingBaseState _currentCharacterControllingState;
        [SerializeField] public float MoveSpeed = 2f;
        [SerializeField] public float rotationSpeed = 0.1f;

        [SerializeField] public float RollSpeedMultiplier = 2f;

        /// <summary>
        /// 1000 = 1 seconds real time
        /// </summary>
        /// <param = RollCooldownDuration> duration in milliseconds</param>
        [SerializeField] public int RollCooldownDuration = 135;
        [SerializeField] public bool RollOnCooldown = false;

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

        public void ChangeControllingState(States newState)
        {
            _currentCharacterControllingState.EndTransition();
            _currentCharacterControllingState = CharacterControllingStates[newState];
            _currentCharacterControllingState.StartTransition();

        }

        public async void SetRollOnCooldown()
        {
            this.RollOnCooldown = true;
            await Task.Delay(RollCooldownDuration);
            this.RollOnCooldown = false;
        }
        private void Update()
        {
            _currentCharacterControllingState.Execute();
        }
    }
}