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
        private void Update()
        {
            if (VirtualInputManager.Instance.EquipWeapon)
            {
                this.gameObject.GetComponent<Animator>().SetBool("EquipWeapon", true);
            }
        }
    }
}