using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using SLGame.Input;
using SLGame.Modules;

namespace SLGame.Gameplay
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private bool weaponEquiped = false;
        private void Update()
        {
            if (VirtualInputManager.Instance.EquipWeapon)
            {
                weaponEquiped = !weaponEquiped;
                Debug.LogWarning("Equiping weapon");
                Animator animator = this.gameObject.GetComponent<Animator>();
                animator.SetBool("EquipWeapon", weaponEquiped);
                animator.SetLayerWeight(1, 1f);
            }
        }
    }
}