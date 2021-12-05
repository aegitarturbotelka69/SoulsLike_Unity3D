using UnityEngine;

namespace SLGame.Input
{
    public class InputInitialization : MonoBehaviour
    {
        void Awake()
        {
            if (VirtualInputManager.Instance == null)
            {
                VirtualInputManager.Initialization();
            }

            Destroy(this.gameObject);
        }
    }
}