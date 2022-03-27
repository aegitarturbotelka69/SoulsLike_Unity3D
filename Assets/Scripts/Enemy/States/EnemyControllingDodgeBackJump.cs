using UnityEngine;

using SLGame.Gameplay;

namespace SLGame.Enemy
{
    public class EnemyControllingDodgeBackJumpState : EnemyControllingBaseState
    {
        public EnemyControllingDodgeBackJumpState(Animator enemyAnimator, EnemyAI ai)
            : base(enemyAnimator, ai) { }
        public override void Execute()
        {
            base.Execute();
        }
        public override void StartTransition()
        {
            _animator.SetBool(States.DodgeBackJump.ToString(), true);
        }

        public override void EndTransition(bool endingManually)
        {
            _animator.SetBool(States.DodgeBackJump.ToString(), false);
        }
    }
}