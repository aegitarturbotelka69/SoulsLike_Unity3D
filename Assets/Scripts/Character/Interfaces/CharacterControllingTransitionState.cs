using UnityEngine;

namespace SLGame.Gameplay
{
    public interface CharacterControllingTransitionState
    {
        void StartTransition();
        void EndTransition();
    }
}