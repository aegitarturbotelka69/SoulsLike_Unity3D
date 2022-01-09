using System;
using System.Threading.Tasks;

using UnityEngine;

using SLGame.Input;

namespace SLGame.Gameplay
{
    public class CharacterControllingStopRunState : CharacterControllingBaseState
    {
        [Header("Stats:")]
        [SerializeField]
        /// <summary>
        /// 1000 equals 1 second
        /// </summary>
        /// <param = _getSpeedTimeForRunning> in milliseconds </param>
        private int _getSpeedTimeForRunning = 700;

        [Header("Info")]
        [SerializeField] private bool _running = false;



        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playerMovementReference"> Reference to PlayerMovement script</param>
        /// <returns> void </returns>
        public CharacterControllingStopRunState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }

        private async void StopRunning()
        {
            _running = true;
            await Task.Delay(_getSpeedTimeForRunning);
            CheckInput();
        }

        private void CheckInput()
        {
            playerMovement.ChangeControllingState(States.Idle);
            return;
        }


        public override void Execute()
        {
            Debug.Log("STOP RUNNING STATE");
            if (_running == false)
            {
                StopRunning();
            }
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), true);
        }
        public override void EndTransition()
        {
            _running = false;
            playerMovement.CharacterAnimator.SetBool(States.StopRun.ToString(), false);
        }
    }
}