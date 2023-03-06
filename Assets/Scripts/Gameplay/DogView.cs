using UnityEngine;
using VContainer;
using Yarde.Input;

namespace Yarde.Gameplay
{
    public class DogView : MonoBehaviour
    {
        [Inject] private InputSystem _inputSystem;

        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigidbody;

        public void FixedUpdate()
        {
            _rigidbody.velocity = _inputSystem.GetMovementNormalized() * _speed;
        }
    }
}