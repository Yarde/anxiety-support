using UnityEngine;
using VContainer;
using Yarde.Input;

namespace Yarde.Gameplay
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class DogView : MonoBehaviour
    {
        [Inject] private InputSystem _inputSystem;

        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void FixedUpdate()
        {
            if (!_inputSystem.IsMoving) return;

            _characterController.Move(_inputSystem.Direction * (_speed * Time.fixedDeltaTime));
            transform.rotation = _inputSystem.LookRotationSmoothed(transform.eulerAngles.y, _turnSmoothTime);
        }
    }
}