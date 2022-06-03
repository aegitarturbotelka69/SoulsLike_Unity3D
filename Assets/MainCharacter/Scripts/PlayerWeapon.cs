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

        [SerializeField] private Transform _steathedWeaponParentPosition;
        [SerializeField] private Transform _armedWeaponParentPosition;

        [SerializeField] private Gameplay.Weapon _currentlyEquippedLightWeapon;
        [SerializeField] private Gameplay.Weapon _currentlyEquippedHeavyWeapon;

        /// <summary>
        /// Equipping selected weapon to the player
        /// </summary>
        /// <param name="sender"> object </param>
        /// <param name="selectedWeapon"> Weapon to equip </param>
        // private void EquipWeapon(object sender, Weapon selectedWeapon)
        // {
        //     // ! Rework
        //     if (selectedWeapon.Type == WeaponType.Fast)
        //     {
        //         _currentlyEquippedLightWeapon = selectedWeapon;
        //         GameObject weapon = Instantiate(
        //             original: selectedWeapon.Prefab,
        //             position: selectedWeapon.SteathedTransform.Position,
        //             rotation: Quaternion.Euler(selectedWeapon.SteathedTransform.Rotation));
        //     }
        //     else if (selectedWeapon.Type == WeaponType.Heavy)
        //     {
        //         _currentlyEquippedHeavyWeapon = selectedWeapon;
        //         GameObject weapon = Instantiate(
        //             original: selectedWeapon.Prefab,
        //             position: selectedWeapon.SteathedTransform.Position,
        //             rotation: Quaternion.Euler(selectedWeapon.SteathedTransform.Rotation));
        //     }
        // }

        private void SteathWeapon()
        {
            Debug.LogWarning("Steathing current weapon");
        }
        private void Awake()
        {
            FindAndSetPositionsForWeapon();

            _playerAnimator = this.gameObject.GetComponent<Animator>();

            // UI.UI_WeaponSlot.OnWeaponSelectedTEST += EquipWeapon;
            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedHeavyWeapon.EquipWeapon;
            UI.UI_WeaponSlot.OnWeaponSelected += _currentlyEquippedLightWeapon.EquipWeapon;
        }
        private void Update()
        {
            // CheckForWeaponInput();
        }

        /// <summary>
        /// Find and set armed and steahed positions for weapons
        /// </summary>
        private void FindAndSetPositionsForWeapon()
        {
            // applying armed and steathed positions;
            // !Maybe i should rework it
            _steathedWeaponParentPosition = WeaponSteathedPosition.Transform;
            _armedWeaponParentPosition = WeaponArmedPosition.Transform;
        }
    }
}