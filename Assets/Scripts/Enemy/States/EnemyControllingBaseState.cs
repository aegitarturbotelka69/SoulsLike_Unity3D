using SLGame.Gameplay;
using UnityEngine;

namespace SLGame.Enemy
{
    public abstract class EnemyControllingBaseState : ControllingTransitionState
    {
        [SerializeField] protected EnemyAI _enemyAI;
        [SerializeField] protected Animator _animator;

        public EnemyControllingBaseState(Animator enemyAnimator, EnemyAI ai)
        {
            this._animator = enemyAnimator;
            this._enemyAI = ai;
        }
        virtual public void Execute()
        {
            Debug.Log("Current State:" + this.GetType().ToString());
        }
        virtual public void StartTransition()
        {
            throw new System.NotImplementedException();
        }

        virtual public void EndTransition(bool endingManually)
        {
            throw new System.NotImplementedException();
        }
    }
}