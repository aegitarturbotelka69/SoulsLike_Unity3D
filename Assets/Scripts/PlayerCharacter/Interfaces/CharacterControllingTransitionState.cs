using UnityEngine;

namespace SLGame.Gameplay
{
    public interface ControllingTransitionState
    {
        void StartTransition();
        void EndTransition(bool endingManually);
    }
}