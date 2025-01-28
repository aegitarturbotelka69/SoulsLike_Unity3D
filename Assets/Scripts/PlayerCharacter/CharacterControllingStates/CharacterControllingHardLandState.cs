using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingHardLandState : CharacterControllingBaseState
    {
        [Header("References:")]
        [SerializeField] protected WeaponLogicAssembler _playerWeapon;
        public CharacterControllingHardLandState(
            States enumState,
            PlayerMovement playerMovementReference,
            CharacterController controller,
            WeaponLogicAssembler playerWeaponReference)
        : base(enumState, playerMovementReference, controller)
        {
            this._playerWeapon = playerWeaponReference;
        }

        public override void Execute()
        {
            //GetInput();
            base.Execute();
        }

        public override void StartTransition()
        {
            _playerMovement.CharacterAnimator.SetBool(this.EnumState.ToString(), true);
            _playerWeapon.CanAttack = false;
        }

        public override void EndTransition(bool endingManually)
        {
            _playerMovement.CharacterAnimator.SetBool(this.EnumState.ToString(), false);

            _playerWeapon.CanAttack = true;

            if (!endingManually)
                return;

            if (VirtualInputManager.Instance.Roll)
            {
                _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Roll];
                _playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), true);
                return;
            }

            if (VirtualInputManager.Instance.MoveBack
                || VirtualInputManager.Instance.MoveFront
                || VirtualInputManager.Instance.MoveLeft
                || VirtualInputManager.Instance.MoveRight)
            {
                _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Move];
                _playerMovement.CharacterAnimator.SetBool(States.Move.ToString(), true);
                return;
            }

            _playerMovement.CurrentCharacterControllingState = _playerMovement.CharacterControllingStates[States.Idle];
            _playerMovement.CharacterAnimator.SetBool(States.Idle.ToString(), true);
            return;
        }
    }
}