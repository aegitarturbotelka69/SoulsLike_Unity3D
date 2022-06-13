using UnityEngine;

namespace SLGame.Gameplay
{
    public class WeaponUnarmedState : ITransitorBetweenWeapons, IAttack
    {
        public void HeavyAttack()
        {
            Debug.LogWarning("Unarmed Heavy attack");
        }

        public void LightAttack(Animator animator)
        {
            Debug.LogWarning("Unarmed light attack");

            animator.SetLayerWeight(1, 1f);
            animator.SetBool("LightAttack", true);
            //animator.
        }

        public void SwitchWeapon()
        {
            throw new System.NotImplementedException();
        }
    }
}