using System;
using SLGame.Gameplay;
using SLGame.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace SLGame.UI
{
    public class UI_WeaponSlot : MonoBehaviour
    {
        //public event EventHandler<Weapon> OnWeaponSelected;
        public static event EventHandler<WeaponArgs> OnWeaponSelected;
        [SerializeField] private WeaponSO _selectedWeapon;

        private void Start()
        {
            ApplyWeapon();
        }

        /// <summary>
        /// Apply selected weapon to UI and player's hands
        /// </summary>
        public void ApplyWeapon()
        {
            UpdateUISprite();
            //Debug.LogWarning("Invoking " + OnWeaponSelected.ToString());
            OnWeaponSelected?.Invoke(
                sender: this,
                e: new WeaponArgs
                {
                    Weapon = _selectedWeapon,
                    WeaponType = _selectedWeapon.Type
                });
        }

        /// <summary>
        /// updates sprite in ui element
        /// </summary>
        private void UpdateUISprite()
        {
            this.gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = _selectedWeapon.Image;
            UIExtensions.SetTransparency(p_image: this.gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>());
        }
    }
}
