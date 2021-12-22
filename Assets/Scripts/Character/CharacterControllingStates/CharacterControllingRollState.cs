using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingRollState : CharacterControllingBaseState
    {
        public CharacterControllingRollState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }

        public override void GetInput()
        {

        }

        public override void Move()
        {
            base.Move();
        }

        public override void Rotate()
        {
            base.Rotate();
        }
    }
}