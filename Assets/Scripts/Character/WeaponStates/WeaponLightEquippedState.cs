using UnityEngine;

namespace SLGame.Gameplay
{
    public class WeaponLightEquippedState : ITransitorBetweenWeapons, IAttack
    {
        public void HeavyAttack(Animator animator)
        {
            throw new System.NotImplementedException();
        }

        public void LightAttack(Animator animator)
        {
            throw new System.NotImplementedException();
        }

        public void SwitchWeapon()
        {
            throw new System.NotImplementedException();
        }
    }
}