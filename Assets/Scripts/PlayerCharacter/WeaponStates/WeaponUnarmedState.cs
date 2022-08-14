using SLGame.Input;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class WeaponUnarmedState : WeaponBaseState
    {
        /// <summary>
        /// Reference to PlayerWeapon script
        /// </summary>
        [SerializeField] private PlayerWeapon _playerWeapon;

        /// <summary>
        /// Reference to Player Animator Controller
        /// </summary>
        [SerializeField] private Animator _playerAnimatorController;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="animator">Player Animator Controller</param>4
        /// <param name="playerWeapon">PlayerWeapon Script</param>
        public WeaponUnarmedState(Animator animator, PlayerWeapon playerWeapon)
        {
            this._playerAnimatorController = animator;
            this._playerWeapon = playerWeapon;

            animator.SetLayerWeight((int)PlayerAnimationLayers.UnArmedWeapon, 1f);
        }
        public override void GetInput()
        {
            if (VirtualInputManager.Instance.LightAttack
                && _playerWeapon.CanAttack
                && PlayerGravityCheck.PLAYER_IS_GROUNDED)
            {
                LightAttack();
                _playerAnimatorController.SetBool(States.LightAttack.ToString(), true);
            }
            else
            {
                _playerAnimatorController.SetBool(States.LightAttack.ToString(), false);
            }

            if (VirtualInputManager.Instance.HeavyAttack
                && _playerWeapon.CanAttack
                && PlayerGravityCheck.PLAYER_IS_GROUNDED)
            {
                HeavyAttack();
                _playerAnimatorController.SetBool(States.HeavyAttack.ToString(), true);
            }
            else
            {
                _playerAnimatorController.SetBool(States.HeavyAttack.ToString(), false);
            }
        }

        public override void LightAttack()
        {
            _playerWeapon.GetPlayerMovementReference().ChangeControllingState(States.LightAttack);
        }

        public override void HeavyAttack()
        {
            _playerWeapon.GetPlayerMovementReference().ChangeControllingState(States.HeavyAttack);
        }

        public override void SwitchWeapon()
        {
            _playerAnimatorController.SetLayerWeight((int)PlayerAnimationLayers.UnArmedWeapon, 0f);
        }

    }
}