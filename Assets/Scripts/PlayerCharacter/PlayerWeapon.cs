using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private PlayerMovement _playerMovement;

        [SerializeField] private PlayerGravityCheck _playerGravity;

        #region TransformPositions
        /// <summary>
        /// Transform of object that using to parent steathed weapon
        /// </summary>
        [SerializeField] public Transform _steathedWeaponParentPosition;

        /// <summary>
        /// Transform of object that using to parent armed weapon
        /// </summary>
        [SerializeField] public Transform _armedWeaponParentPosition;
        #endregion TransformPositions

        [Header("In game:")]

        /// <summary>
        /// Defines can player use attack at the current moment
        /// </summary>
        [SerializeField] public bool CanAttack;

        /// <summary>
        /// Represents current weapon state
        /// </summary>
        [SerializeField] private WeaponBaseState _weaponCurrentState;

        /// <summary>
        /// Stores currently equipped light weapon in the menu
        /// </summary>
        [SerializeField] private Weapon _currentlyEquippedLightWeapon;

        /// <summary>
        /// Stores currently equipped heavy weapon in the menu
        /// </summary>
        [SerializeField] private Weapon _currentlyEquippedHeavyWeapon;

        private void Awake()
        {
            _playerAnimator = this.gameObject.GetComponent<Animator>();
            _playerMovement = this.gameObject.GetComponent<PlayerMovement>();
            _playerGravity = this.gameObject.GetComponent<PlayerGravityCheck>();

            this._currentlyEquippedLightWeapon = Weapon.CreateComponent(this.gameObject, this, WeaponType.Light);
            this._currentlyEquippedHeavyWeapon = Weapon.CreateComponent(this.gameObject, this, WeaponType.Heavy);

            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedHeavyWeapon.EquipWeapon;
            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedLightWeapon.EquipWeapon;

            // !Hard-coded
            _weaponCurrentState = new WeaponUnarmedState(_playerAnimator, this);
        }

        private void Start()
        {
            CanAttack = true;
        }

        private void Update()
        {

        }

        /// <summary>
        /// Get PlayerMovement reference
        /// </summary>
        /// <returns>PlayerMovement script component</returns>
        public PlayerMovement GetPlayerMovementReference() => _playerMovement;
    }
}