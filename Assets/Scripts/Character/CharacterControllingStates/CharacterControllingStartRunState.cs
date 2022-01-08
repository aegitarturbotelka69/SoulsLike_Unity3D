using System;
using System.Threading.Tasks;

using UnityEngine;

using SLGame.Input;

namespace SLGame.Gameplay
{
    public class ChracterControllingStartRunState : CharacterControllingBaseState
    {
        [Header("Stats:")]
        [SerializeField]
        /// <summary>
        /// 1000 equals 1 second
        /// </summary>
        /// <param = _getSpeedTimeForRunning> in milliseconds </param>
        private const int GetSpeedTimeForRunning = 235;

        [Header("Info")]
        [SerializeField] private bool _running = false;



        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playerMovementReference"> Reference to PlayerMovement script</param>
        /// <returns> void </returns>
        public ChracterControllingStartRunState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }

        private async void StartRunning()
        {
            _running = true;
            await Task.Delay(GetSpeedTimeForRunning);
            CheckInput();
        }

        private void CheckInput()
        {
            if (VirtualInputManager.Instance.MoveFront || VirtualInputManager.Instance.MoveLeft || VirtualInputManager.Instance.MoveRight || VirtualInputManager.Instance.MoveBack)
            {
                playerMovement.ChangeControllingState(States.Run);
                return;
            }
            else
            {
                playerMovement.ChangeControllingState(States.StopRun);
                return;
            }
        }


        public override void Execute()
        {
            if (_running == false)
            {
                StartRunning();
            }
            Debug.LogWarning("START RUNNING STATE");
        }

        public override void StartTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.StartRun.ToString(), true);
        }
        public override void EndTransition()
        {
            playerMovement.CharacterAnimator.SetBool(States.StartRun.ToString(), false);
            _running = false;
        }
    }
}