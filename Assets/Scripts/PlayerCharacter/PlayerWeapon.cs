using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private PlayerMovement _playerMovement;

        [Header("In game:")]

        /// <summary>
        /// true = player have weapon in the hands, false = player steathed all weapons
        /// </summary>
        [SerializeField] private bool _weaponEquipped = false;

        [Space(10)]

        /// <summary>
        /// Transform of object that using to parent steathed weapon
        /// </summary>
        [SerializeField] public Transform _steathedWeaponParentPosition;

        /// <summary>
        /// Transform of object that using to parent armed weapon
        /// </summary>
        [SerializeField] public Transform _armedWeaponParentPosition;

        [Space(10)]

        /// <summary>
        /// Stores currently equipped light weapon in the menu
        /// </summary>
        [SerializeField] private Weapon _currentlyEquippedLightWeapon;

        /// <summary>
        /// Stores currently equipped heavy weapon in the menu
        /// </summary>
        [SerializeField] private Weapon _currentlyEquippedHeavyWeapon;

        /// <summary>
        /// Contains equipped weapon in hands.
        /// Handles weapon switch logic
        /// </summary>
        [SerializeField] private EquippedWeaponContext _weaponContext;
        private void Awake()
        {
            _playerAnimator = this.gameObject.GetComponent<Animator>();
            _playerMovement = this.gameObject.GetComponent<PlayerMovement>();

            this._currentlyEquippedLightWeapon = Weapon.CreateComponent(this.gameObject, this, WeaponType.Light);
            this._currentlyEquippedHeavyWeapon = Weapon.CreateComponent(this.gameObject, this, WeaponType.Heavy);

            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedHeavyWeapon.EquipWeapon;
            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedLightWeapon.EquipWeapon;

            _weaponContext = new EquippedWeaponContext(this, _playerAnimator, new WeaponUnarmedState(), new WeaponUnarmedState());
        }

        private void Update()
        {
            GetInput();
            _weaponContext.GetInput();
        }

        private void GetInput()
        {
            if (VirtualInputManager.Instance.EquipLightWeapon)
            {
                _weaponContext.ExecuteTransition(WeaponType.Light);
            }

            if (VirtualInputManager.Instance.EquipHeavyWeapon)
            {
                _weaponContext.ExecuteTransition(WeaponType.Heavy);
            }

            if (VirtualInputManager.Instance.LightAttack)
            {
                _weaponContext.LightAttack();
            }

            if (VirtualInputManager.Instance.HeavyAttack)
            {
                _weaponContext.HeavyAttack();
            }
        }

        public PlayerMovement GetPlayerMovementReference() => _playerMovement;
    }
}