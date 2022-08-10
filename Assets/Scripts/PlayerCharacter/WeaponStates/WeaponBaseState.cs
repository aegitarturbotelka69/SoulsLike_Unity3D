using UnityEngine;

namespace SLGame.Gameplay
{
    public abstract class WeaponBaseState : ITransitorBetweenWeapons, IAttack
    {
        public abstract void GetInput();

        /// <summary>
        /// Main logic for Light Attack
        /// </summary>
        public abstract void LightAttack();

        /// <summary>
        /// Main logic for Heavy Attack
        /// </summary>
        public abstract void HeavyAttack();

        /// <summary>
        /// Base method to switch between weapon
        /// </summary>
        public abstract void SwitchWeapon();
    }
}