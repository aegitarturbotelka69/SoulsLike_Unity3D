using UnityEngine;

namespace SLGame.Input
{
    public class Keyboard_InputType : MonoBehaviour, I_ExecuteInputStrategy
    {
        public void Execute()
        {
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.MoveRight = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveRight = false;
            }

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.MoveLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveLeft = false;
            }

            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                VirtualInputManager.Instance.MoveFront = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveFront = false;
            }

            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.Instance.MoveBack = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveBack = false;
            }

            if (UnityEngine.Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.Roll = true;
            }
            else
            {
                VirtualInputManager.Instance.Roll = false;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
            {
                VirtualInputManager.Instance.Run = true;
            }
            else
            {
                VirtualInputManager.Instance.Run = false;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
            {
                VirtualInputManager.Instance.EquipWeapon = true;
            }
            else
            {
                VirtualInputManager.Instance.EquipWeapon = false;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse2))
            {
                VirtualInputManager.Instance.MoveToTopSelected = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveToTopSelected = false;
            }
        }
    }
}