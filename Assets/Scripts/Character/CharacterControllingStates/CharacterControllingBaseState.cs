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
        virtual public void EndTransition()
        {
            throw new System.NotImplementedException();
        }

        virtual public void StartTransition(CharacterControllingBaseState newControllingState)
        {
            throw new System.NotImplementedException();
        }

        virtual public void GetInput()
        {
            throw new System.NotImplementedException();
        }

        virtual public void Rotate()
        {
            throw new System.NotImplementedException();
        }

        virtual public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}