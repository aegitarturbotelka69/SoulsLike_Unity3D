using UnityEngine;

namespace SLGame.Gameplay
{
    public interface CharacterControllingTransitionState
    {
        void StartTransition(States newState);
        void EndTransition(States previousState);
    }
}