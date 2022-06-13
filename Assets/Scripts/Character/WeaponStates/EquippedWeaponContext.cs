using SLGame.Input;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class EquippedWeaponContext
    {
        private Animator _playerAnimator;
        private PlayerWeapon _playerWeapon;

        public ITransitorBetweenWeapons Transitor { get; set; }
        public IAttack Attack { get; set; }

        [SerializeField] private Weapon _currentlyEquippedWeaponInHands;

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
            _playerWeapon.GetPlayerMovementReference().ChangeControllingState(States.Attack);
        }

        public void HeavyAttack()
        {
            Attack.HeavyAttack(_playerAnimator);
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
    }
}