using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SLGame.Gameplay;
using UnityEngine;
using UnityEngine.AI;

namespace SLGame.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        // * References
        [Header("References:")]
        [SerializeField] public NavMeshAgent NavMeshAgent;

        [SerializeField] public Animator Animator;

        [SerializeField] public CharacterController Controller;

        /// <summary>
        /// Current object forward vision
        /// </summary>
        [SerializeField] private ForwardFOV _enemyForwardVision;

        [SerializeField] public Dictionary<States, EnemyControllingBaseState> EnemyControllingStates = new Dictionary<States, EnemyControllingBaseState>();

        // * Stats
        [Header("Stats:"), Space(40)]
        [SerializeField] public List<Vector3> PatrolPoints;
        [SerializeField] public float PatrolOffset;

        /// <summary>
        /// Number of build-in stamina
        /// </summary>
        [SerializeField, Space(10)] public float Stamina;

        /// <summary>
        /// Delay before add to StaminaRemain value equals StaminaRestorationPower
        /// </summary>
        [SerializeField] public float DelayBeforeRestoreStamina;

        /// <summary>
        /// This value addig to StaminaRemain in 'in-game' mode
        /// </summary>
        [SerializeField] public float StaminaRestorationPower;

        /// <summary>
        /// Light attack 
        /// </summary>
        [SerializeField, Space(10)] public float LightAttackStaminaConsumption = 55f;
        [SerializeField] public float LightAttackRotationSpeed = 20f;
        [SerializeField] public int LightAttackCooldownDuration;

        // * In game stats
        [Header("In game:"), Space(40)]


        /// <summary>
        /// In-game value of stamina
        /// by default equals to Stamina
        /// </summary>
        [SerializeField] public float StaminaRemain;

        /// <summary>
        /// true if light attack on cooldown
        /// </summary>
        [SerializeField] public bool LightAttackOnCooldown;

        /// <summary>
        /// Current state of the enemy
        /// </summary>
        [SerializeField] public EnemyControllingBaseState CurrentState;

        /// <summary>
        /// In game value to control
        /// how much time before next stamina restoration
        /// </summary>
        [SerializeField] private float _currentLeftDelayBeforeStaminaRestoration = 0f;

        private void Awake()
        {
            this.Controller = this.gameObject.GetComponent<CharacterController>();
            this.Animator = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
            this._enemyForwardVision = this.gameObject.GetComponent<ForwardFOV>();
        }
        private void Start()
        {
            EnemyControllingStates.Add(States.Idle, new EnemyControllingIdleState(Animator, this, _enemyForwardVision));
            EnemyControllingStates.Add(States.Patrolling, new EnemyControllingPatrollingState(Animator, this, _enemyForwardVision));
            EnemyControllingStates.Add(States.Chasing, new EnemyControllingChasingState(Animator, this, _enemyForwardVision));
            EnemyControllingStates.Add(States.LightAttack, new EnemyControllingLightAttackState(Animator, this, _enemyForwardVision));
            EnemyControllingStates.Add(States.DodgeBackJump, new EnemyControllingDodgeBackJumpState(Animator, this));
            EnemyControllingStates.Add(States.RestoringPower, new EnemyControllingRestoringPowerState(Animator, this, _enemyForwardVision));

            CurrentState = EnemyControllingStates[States.Idle];

            StartCoroutine(StaminaPower());
        }

        private void Update()
        {
            CurrentState.Execute();
        }

        /// <summary>
        /// Execute start transaction method for current state
        /// </summary>
        public void ManualStartTransaction()
        {
            CurrentState.StartTransition();
        }

        /// <summary>
        /// Executes to change current state and call StartTransition method of this state
        /// </summary>
        /// <param name="newState"></param>
        public void ManualStartTransactionSwitchState(States newState)
        {
            CurrentState = EnemyControllingStates[newState];
            CurrentState.StartTransition();
        }

        public void ManualEndTransaction()
        {
            CurrentState.EndTransition(true);
        }

        private IEnumerator StaminaPower()
        {
            while (true)
            {
                if (_currentLeftDelayBeforeStaminaRestoration <= 0f
                    && StaminaRemain <= (Stamina + StaminaRestorationPower))
                {
                    _currentLeftDelayBeforeStaminaRestoration = DelayBeforeRestoreStamina;
                    StaminaRemain += StaminaRestorationPower;
                    yield return null;
                }
                else
                {
                    _currentLeftDelayBeforeStaminaRestoration -= Time.deltaTime;
                    yield return null;
                }
            }
        }

        public async void SetLightAttackOnCooldown()
        {
            this.LightAttackOnCooldown = true;
            await Task.Delay(LightAttackCooldownDuration);
            this.LightAttackOnCooldown = false;
        }
        public void ChangeControllingState(States newState, bool endingManually = false)
        {
            CurrentState.EndTransition(endingManually);
            CurrentState = EnemyControllingStates[newState];
            CurrentState.StartTransition();
        }
    }
}