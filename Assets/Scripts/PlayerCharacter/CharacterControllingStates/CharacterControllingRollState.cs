using UnityEngine;

using SLGame.Input;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SLGame.Gameplay
{
    public class CharacterControllingRollState : CharacterControllingBaseState
    {
        [SerializeField] private float _rollSpeed;
        public CharacterControllingRollState(States enumState, PlayerMovement playerMovementReference, ref CharacterController controller)
            : base(enumState, playerMovementReference, ref controller) { }

        public override void Execute()
        {
            Roll();
            base.Execute();
        }
        private void Roll()
        {
            float lookAngle = Mathf.Atan2(_playerMovement.Direction.x, _playerMovement.Direction.z)
                    * Mathf.Rad2Deg
                    + _playerMovement._cameraTransform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;
            _playerMovement.transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
            _playerMovement.CharacterController.Move(moveDirection.normalized * _rollSpeed * Time.deltaTime);
        }



        public override void StartTransition()
        {
            this._rollSpeed = _playerMovement.MoveSpeed * _playerMovement.RollSpeedMultiplier;

            _playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), false);
            _playerMovement.PlaceRollOnCooldown();

            if (!endingManually)
                return;

            if (VirtualInputManager.Instance.MoveBack
                || VirtualInputManager.Instance.MoveFront
                || VirtualInputManager.Instance.MoveLeft
                || VirtualInputManager.Instance.MoveRight)
            {
                _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Move];
                _playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
                return;
            }

            if (VirtualInputManager.Instance.Roll)
            {
                _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Roll];
                _playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), true);
                return;
            }

            _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Idle];
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
            return;
        }
    }
}