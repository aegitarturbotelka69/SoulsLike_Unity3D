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

        virtual public void StartTransition()
        {
            throw new System.NotImplementedException();
        }

        virtual public void EndTransition()
        {
            throw new System.NotImplementedException();
        }
    }
}