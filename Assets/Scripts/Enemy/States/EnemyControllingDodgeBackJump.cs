using UnityEngine;

using SLGame.Gameplay;
using System.Threading.Tasks;

namespace SLGame.Enemy
{
    public class EnemyControllingDodgeBackJumpState : EnemyControllingBaseState
    {
        [Header("References: ")]

        /// <summary>
        /// Reference to enemy transform
        /// </summary>
        [SerializeField] private Transform _enemyTransform;

        [Header("Stats: ")]

        /// <summary>
        /// Back jump time build-in.
        /// </summary>
        [SerializeField] private float _backJumpTime = 0.5f;

        [SerializeField] private float _backJumpSpeedMultiplier = 3.5f;

        [Header("In game: ")]
        /// <summary>
        /// Back jump time remain if logic executing, after execution reseting to build-in backJumpTime
        /// </summary>
        [SerializeField] private float _backJumpTimeRemain = 0.5f;

        public EnemyControllingDodgeBackJumpState(Animator enemyAnimator, EnemyAI ai)
            : base(enemyAnimator, ai)
        {
            _enemyTransform = ai.gameObject.GetComponent<Transform>();
        }
        public override void Execute()
        {
            if (_backJumpTimeRemain > 0)
            {
                _enemyAI.Controller.Move(
                        (-_enemyTransform.forward)
                        * _enemyAI.NavMeshAgent.speed
                        * _backJumpSpeedMultiplier
                        * Time.deltaTime);

                _backJumpTimeRemain -= Time.deltaTime;
            }
            else
            {
                EndTransition(true);
            }
        }
        public override void StartTransition()
        {
            _animator.SetBool(States.DodgeBackJump.ToString(), true);
            _backJumpTimeRemain = _backJumpTime;
        }

        public override void EndTransition(bool endingManually)
        {
            _animator.SetBool(States.DodgeBackJump.ToString(), false);

            if ((_enemyAI.StaminaRemain / _enemyAI.Stamina) * 100 < 40)
            {
                _enemyAI.ManualStartTransactionSwitchState(States.RestoringPower);
            }
        }
    }
}