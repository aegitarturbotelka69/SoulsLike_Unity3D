using UnityEngine;

namespace SLGame.Gameplay
{
    public interface IAttack
    {
        public void LightAttack(Animator animator);

        public void HeavyAttack(Animator animator);
    }
}