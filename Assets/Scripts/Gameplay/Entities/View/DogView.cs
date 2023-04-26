using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using Yarde.Input;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.Entities.View
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class DogView : EntityView
    {
        [Inject] [UsedImplicitly] private InputSystem _inputSystem;
        [Inject] [UsedImplicitly] private EntityManager _entityManager;

        [SerializeField] private Animator _animator;

        [Header("Animation settings")] [SerializeField]
        private float _speed = 5f;

        [SerializeField] private float _turnSmoothTime = 0.1f;

        private CharacterController _characterController;

        private bool _isAttacking;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void FixedUpdate()
        {
            _animator.SetFloat(Speed, _inputSystem.Input.magnitude);

            if (!_inputSystem.IsMoving || _isAttacking) return;

            _characterController.Move(_inputSystem.Input * (_speed * Time.fixedDeltaTime));
            transform.rotation = _inputSystem.LookRotationSmoothed(transform.eulerAngles.y, _turnSmoothTime);
        }

        private async void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.gameObject.CompareTag("Monster") || _isAttacking)
            {
                return;
            }
            
            _isAttacking = true;
            _entityManager.AttackEntity(hit.gameObject, 1);
            await _animator.TriggerAndWaitForStateEnd("Attack", this.GetCancellationTokenOnDestroy()).SuppressCancellationThrow();
            _isAttacking = false;
        }
    }
}