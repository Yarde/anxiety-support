using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using Yarde.Audio;
using Yarde.Utils.Extensions;
using AudioType = Yarde.Audio.AudioType;

namespace Yarde.Gameplay.Entities.View
{
    public class MonsterView : EntityView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private List<AudioClip> _spawmClips;
        [SerializeField] private List<AudioClip> _attackClips;
        [SerializeField] private List<AudioClip> _dieClips;

        [Inject] [UsedImplicitly] private AudioManager _audioManager;

        private List<Material> _materials;

        public bool IsAttackingDistance => _navMeshAgent.hasPath &&
                                           _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;

        private void Awake()
        {
            _materials = new List<Material>();
            var renderers = GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in renderers)
            {
                var newMaterial = new Material(meshRenderer.material);
                meshRenderer.material = newMaterial;
                _materials.Add(newMaterial);
            }
            _audioManager.PlayClip(AudioType.Sfx, _spawmClips.Random());
        }

        public void SetTarget(EntityView target)
        {
            _navMeshAgent.stoppingDistance = Random.Range(3f, 5f);
            _navMeshAgent.SetDestination(target.transform.position);
        }

        public async UniTaskVoid OnDie()
        {
            _audioManager.PlayClip(AudioType.Sfx, _dieClips.Random());
            _characterController.enabled = false;
            var animationTask = _animator.TriggerAndWaitForStateEnd("Die", this.GetCancellationTokenOnDestroy());

            await UniTask.WhenAny(animationTask, UniTask.Delay(1000));
            await Fade(0, 0.5f);

            Destroy(gameObject);
        }

        public async UniTask Attack()
        {
            _audioManager.PlayClip(AudioType.Sfx, _attackClips.Random());
            await _animator.TriggerAndWaitForStateEnd("Attack", this.GetCancellationTokenOnDestroy());
        }

        public async UniTask Fade(float value, float duration)
        {
            foreach (var material in _materials)
            {
                material.DOFade(value, duration);
            }

            await UniTask.Delay(duration.ToMilliseconds());
        }
    }
}