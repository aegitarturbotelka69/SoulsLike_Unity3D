using UnityEngine;

namespace SLGame.Gameplay
{
    public abstract class CharacterAnimBaseState : StateMachineBehaviour
    {
        [SerializeField] protected PlayerMovement _playerMovement;

        protected void GetPlayerMovementReference(Animator animator)
        {
            if (_playerMovement == null)
                _playerMovement = animator.gameObject.GetComponent<PlayerMovement>();
        }
    }
}
