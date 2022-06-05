using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using SLGame.Input;
using SLGame.Modules;
using SLGame.ScriptableObjects;

namespace SLGame.Gameplay
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Animator _playerAnimator;

        [Header("Stats: ")]

        [Header("In game:")]

        [SerializeField] public Transform _steathedWeaponParentPosition;
        [SerializeField] public Transform _armedWeaponParentPosition;

        [SerializeField] private Weapon _currentlyEquippedLightWeapon;
        [SerializeField] private Weapon _currentlyEquippedHeavyWeapon;

        private void SteathWeapon()
        {
            Debug.LogWarning("Steathing current weapon");
        }
        private void Awake()
        {
            _playerAnimator = this.gameObject.GetComponent<Animator>();

            this._currentlyEquippedLightWeapon = Weapon.CreateComponent(this.gameObject, this, WeaponType.Fast);
            this._currentlyEquippedHeavyWeapon = Weapon.CreateComponent(this.gameObject, this, WeaponType.Heavy);

            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedHeavyWeapon.EquipWeapon;
            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedLightWeapon.EquipWeapon;
        }
        private void Update()
        {

        }
    }
}