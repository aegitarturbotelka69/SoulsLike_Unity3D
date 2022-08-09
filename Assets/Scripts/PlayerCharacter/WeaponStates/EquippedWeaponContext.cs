using SLGame.Input;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class EquippedWeaponContext
    {
        [Header("References:")]
        private Animator _playerAnimator;
        private PlayerWeapon _playerWeapon;
        public ITransitorBetweenWeapons Transitor { get; set; }
        public IAttack Attack { get; set; }

        [Header("Stats: ")]
        [SerializeField] private Weapon _currentlyEquippedWeaponInHands;


        [Header("In game:")]
        [SerializeField] private bool _lightAttackOnCooldown;
        [SerializeField] private bool _heavyAttackOnCooldown;

        public EquippedWeaponContext(PlayerWeapon playerWeapon, Animator animator, ITransitorBetweenWeapons transitor, IAttack attack)
        {
            this._playerWeapon = playerWeapon;
            this._playerAnimator = animator;
            this.Transitor = transitor;
            this.Attack = attack;
        }

        public void LightAttack()
        {
            Attack.LightAttack(_playerAnimator);
            _playerWeapon.GetPlayerMovementReference().ChangeControllingState(States.LightAttack);
        }

        public void HeavyAttack()
        {
            Attack.HeavyAttack(_playerAnimator);
            _playerWeapon.GetPlayerMovementReference().ChangeControllingState(States.HeavyAttack);
        }

        /// <summary>
        /// Changes current weapon to selected weapon
        /// </summary>
        /// <param name="type">WeaponType</param>
        public void ExecuteTransition(WeaponType type)
        {
            if (_currentlyEquippedWeaponInHands.GetWeaponType() == type)
            {
                // Steath weapon
            }
            else
            {
                // Disarm current weapon and arm new weapon;
            }
        }

        public void GetInput()
        {
            if (VirtualInputManager.Instance.LightAttack)
            {
                _playerAnimator.SetBool("LightAttack", true);
            }
            else
            {
                _playerAnimator.SetBool("LightAttack", false);
            }

            if (VirtualInputManager.Instance.HeavyAttack)
            {
                _playerAnimator.SetBool("HeavyAttack", true);
            }
            else
            {
                _playerAnimator.SetBool("HeavyAttack", false);
            }
        }
    }
}