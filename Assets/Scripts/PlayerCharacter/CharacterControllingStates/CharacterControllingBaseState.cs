using UnityEngine;

namespace SLGame.Gameplay
{
    public abstract class CharacterControllingBaseState : ControllingTransitionState
    {
        [SerializeField] protected PlayerMovement _playerMovement;

        [SerializeField] protected CharacterController _characterController;

        [SerializeField] protected States EnumState;
        private States _enumState { get { return EnumState; } set { EnumState = value; } }


        public CharacterControllingBaseState(States enumState, PlayerMovement playerMovementReference, CharacterController controller)
        {
            this._enumState = enumState;
            this._playerMovement = playerMovementReference;
            this._characterController = controller;
        }

        virtual public void Execute()
        {
            //Debug.Log("Current State:" + this.GetType().ToString());
        }

        virtual public void StartTransition()
        {
            throw new System.NotImplementedException();
        }

        virtual public void EndTransition(bool endingManually)
        {
            throw new System.NotImplementedException();
        }

        virtual public States GetEnumState() => EnumState;
    }
}