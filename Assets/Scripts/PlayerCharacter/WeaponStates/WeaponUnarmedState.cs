using UnityEngine;

namespace SLGame.Gameplay
{
    public class WeaponUnarmedState : ITransitorBetweenWeapons, IAttack
    {
        public void HeavyAttack(Animator animator)
        {
            animator.SetBool("HeavyAttack", true);
        }

        public void LightAttack(Animator animator)
        {
            animator.SetLayerWeight((int)PlayerAnimationLayers.UnArmedWeapon, 1f);
        }

        public void SwitchWeapon()
        {
            throw new System.NotImplementedException();
        }
    }
}