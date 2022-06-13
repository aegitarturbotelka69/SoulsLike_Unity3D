using UnityEngine;
using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingAttackState : CharacterControllingBaseState
    {
        public CharacterControllingAttackState(PlayerMovement playerMovementReference, ref CharacterController controller)
            : base(playerMovementReference, ref controller) { }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StartTransition()
        {
            //base.StartTransition();
        }

        public override void EndTransition(bool endingManually)
        {
            //base.EndTransition(endingManually);
        }
    }
}