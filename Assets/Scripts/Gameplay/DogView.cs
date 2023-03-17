using UnityEngine;
using VContainer;
using Yarde.Input;

namespace Yarde.Gameplay
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class DogView : MonoBehaviour
    {
        [Inject] private InputSystem _inputSystem;

        [SerializeField] private float _speed;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            _rigidbody.velocity = _inputSystem.GetMovementNormalized() * _speed;
        }
    }
}