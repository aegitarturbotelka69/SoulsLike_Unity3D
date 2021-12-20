using UnityEngine;

namespace SLGame.Gameplay
{
    public interface CharacterControllingTransitionState
    {
        void StartTransition(CharacterControllingBaseState newControllingState);
        void EndTransition();
    }
}