using UnityEngine;

using SLGame.Input;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SLGame.Gameplay
{
    public class CharacterControllingRollState : CharacterControllingBaseState
    {
        [Header("Stats")]

        /// <summary>
        /// _rollTIme = 1000f  => 1 sec real time
        /// </summary>
        ///  <param name="_rollTime"> Rolling time in milliseconds</param>
        [SerializeField] private float _rollTime = 650f;

        [Header("Info:")]
        [SerializeField] private bool _rolling = false;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playerMovementReference"> Reference to PlayerMovement script</param>
        /// <returns>void</returns>
        public CharacterControllingRollState(ref PlayerMovement playerMovementReference) : base(ref playerMovementReference)
        {
            this.playerMovement = playerMovementReference;
        }



        public override void Execute()
        {
            base.Execute();

            if (_rolling == false)
            {
                StartRollOperation();
            }
            Roll();
        }

        private async void StartRollOperation()
        {
            _rolling = true;
            await Task.Delay(Convert.ToInt32(_rollTime));
            CheckInput();
        }

        private void CheckInput()
        {
            if (VirtualInputManager.Instance.Run)
            {
                playerMovement.ChangeControllingState(States.Run);
            }

            // !Attentione! Rework this trash below
            if (VirtualInputManager.Instance.MoveFront || VirtualInputManager.Instance.MoveLeft || VirtualInputManager.Instance.MoveRight || VirtualInputManager.Instance.MoveBack)
            {
                playerMovement.ChangeControllingState(States.Move);
                return;
            }
            else
            {
                playerMovement.ChangeControllingState(States.Idle);
                return;
            }
        }
        private void Roll()
        {
            float lookAngle = Mathf.Atan2(playerMovement.Direction.x, playerMovement.Direction.z) * Mathf.Rad2Deg + playerMovement._cameraTransform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, lookAngle, 0f) * Vector3.forward;
            playerMovement.transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
            playerMovement._characterController.Move(moveDirection.normalized * playerMovement.MoveSpeed * Time.deltaTime);
        }



        public override void StartTransition()
        {
            playerMovement.MoveSpeed = playerMovement.MoveSpeed * playerMovement.RollSpeedMultiplier;

            playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), true);
        }

        public override void EndTransition()
        {
            _rolling = false;

            playerMovement.MoveSpeed = playerMovement.MoveSpeed / playerMovement.RollSpeedMultiplier;
            playerMovement.CharacterAnimator.SetBool(States.Roll.ToString(), false);

            playerMovement.SetRollOnCooldown();
        }
    }
}