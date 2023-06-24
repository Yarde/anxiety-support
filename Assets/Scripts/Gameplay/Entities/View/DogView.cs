using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using Yarde.Audio;
using Yarde.MyInputSystem;
using Yarde.Utils.Extensions;
using AudioType = Yarde.Audio.AudioType;

namespace Yarde.Gameplay.Entities.View
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class DogView : EntityView
    {
        [Inject] [UsedImplicitly] private InputSystem _inputSystem;
        [Inject] [UsedImplicitly] private EntityManager _entityManager;
        [Inject] [UsedImplicitly] private AudioManager _audioManager;

        [SerializeField] private Animator _animator;
        [SerializeField] private List<AudioClip> _attackClips;

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
            _animator.SetFloat(Speed, _inputSystem.InputValue.magnitude);

            if (!_inputSystem.IsMoving || _isAttacking) return;

            _characterController.Move(_inputSystem.InputValue * (_speed * Time.fixedDeltaTime));
            transform.rotation = _inputSystem.LookRotationSmoothed(transform.eulerAngles.y, _turnSmoothTime);
        }

        private async void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.gameObject.CompareTag("Monster") || _isAttacking)
            {
                return;
            }
            
            _isAttacking = true;
            _audioManager.PlayClip(AudioType.Sfx, _attackClips.Random());
            _entityManager.AttackEntity(hit.gameObject, 1);
            await _animator.TriggerAndWaitForStateEnd("Attack", this.GetCancellationTokenOnDestroy()).SuppressCancellationThrow();
            _isAttacking = false;
        }
    }
}