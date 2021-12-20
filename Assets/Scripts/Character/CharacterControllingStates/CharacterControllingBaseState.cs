using UnityEngine;

namespace SLGame.Gameplay
{
    public abstract class CharacterControllingBaseState : CharacterControllingTransitionState
    {
        [SerializeField] public PlayerMovement playerMovement;

        public CharacterControllingBaseState(ref PlayerMovement playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }
        virtual public void EndTransition(States previousState)
        {
            playerMovement.CharacterAnimator.SetBool(previousState.ToString(), false);
        }

        virtual public void StartTransition(States newState)
        {
            playerMovement.CharacterAnimator.SetBool(newState.ToString(), true);
        }

        virtual public void GetInput()
        {
            throw new System.NotImplementedException();
        }

        virtual public void Rotate()
        {

        }

        virtual public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}