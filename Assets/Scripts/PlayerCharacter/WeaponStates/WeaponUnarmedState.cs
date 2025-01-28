using SLGame.Input;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class WeaponUnarmedState : WeaponBaseState
    {
        /// <summary>
        /// Reference to PlayerWeapon script
        /// </summary>
        [SerializeField] private WeaponLogicAssembler _playerWeapon;

        /// <summary>
        /// Reference to Player Animator Controller
        /// </summary>
        [SerializeField] private Animator _playerAnimatorController;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="animator">Player Animator Controller</param>4
        /// <param name="playerWeapon">PlayerWeapon Script</param>
        public WeaponUnarmedState(Animator animator, WeaponLogicAssembler playerWeapon)
        {
            this._playerAnimatorController = animator;
            this._playerWeapon = playerWeapon;

            animator.SetLayerWeight((int)PlayerAnimationLayers.UnArmedWeapon, 1f);
        }

        public override void LightAttack()
        {
            // future logic for enabling damage dealing 
        }

        public override void HeavyAttack()
        {
            // future logic for enabling damage dealing
        }

        public override void SwitchWeapon()
        {
            _playerAnimatorController.SetLayerWeight((int)PlayerAnimationLayers.UnArmedWeapon, 0f);
        }

    }
}