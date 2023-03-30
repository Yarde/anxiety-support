﻿using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using Yarde.Input;

namespace Yarde.Gameplay.Entities
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class DogView : EntityView
    {
        [Inject] [UsedImplicitly] private InputSystem _inputSystem;

        [SerializeField] private Animator _animator;
        
        [Header("Animation settings")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        
        private CharacterController _characterController;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void FixedUpdate()
        {
            _animator.SetFloat(Speed, _inputSystem.Input.magnitude);

            if (!_inputSystem.IsMoving) return;

            _characterController.Move(_inputSystem.Input * (_speed * Time.fixedDeltaTime));
            transform.rotation = _inputSystem.LookRotationSmoothed(transform.eulerAngles.y, _turnSmoothTime);
        }
    }
}