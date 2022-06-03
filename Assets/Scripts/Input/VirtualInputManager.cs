using UnityEngine;

namespace SLGame.Input
{
    public class VirtualInputManager : SLGame.Modules.Singleton<VirtualInputManager>
    {
        [Header("References:")]
        [SerializeField] private I_ExecuteInputStrategy _inputStrategy;

        public bool MoveFront;
        public bool MoveBack;

        public bool MoveLeft;
        public bool MoveRight;

        public bool Roll;
        public bool Run;

        public bool LightWeapon;
        public bool HeavyWeapon;
        public bool MoveToTopSelected;

        public bool AdditionalMenu;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        void Start()
        {
            _inputStrategy = this.gameObject.AddComponent<Keyboard_InputType>();
        }

        void Update()
        {
            _inputStrategy.Execute();
        }
        private void ChangeInputStrategy(I_ExecuteInputStrategy newInputStrategy)
        {
            this._inputStrategy = newInputStrategy;
        }
    }
}