using UnityEngine;

namespace SLGame.Gameplay
{
    public interface IAttack
    {
        void LightAttack(Animator animator);

        void HeavyAttack(Animator animator);
    }
}