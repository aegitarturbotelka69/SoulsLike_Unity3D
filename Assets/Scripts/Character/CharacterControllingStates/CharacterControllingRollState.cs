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
        public CharacterControllingRollState(ref PlayerMovement playerMovementReference, ref CharacterController controller) : base(ref playerMovementReference, ref controller)
        {
            this._playerMovement = playerMovementReference;
        }



        public override void Execute()
        {
            base.Execute();
            Roll();
        }
        private void Roll()
        {
            float lookAngle = Mathf.Atan2(_playerMovement.Direction.x, _playerMovement.Direction.z) * Mathf.Rad2Deg + _playerMovement._cameraTransform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;
            _playerMovement.transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
            _playerMovement.CharacterController.Move(moveDirection.normalized * _playerMovement.MoveSpeed * Time.deltaTime);
        }



        public override void StartTransition()
        {
            _playerMovement.MoveSpeed = _playerMovement.MoveSpeed * _playerMovement.RollSpeedMultiplier;

            _playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), true);
        }

        public override void EndTransition()
        {

            _playerMovement.MoveSpeed = _playerMovement.MoveSpeed / _playerMovement.RollSpeedMultiplier;
            _playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), false);
            _playerMovement.PlaceRollOnCooldown();

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