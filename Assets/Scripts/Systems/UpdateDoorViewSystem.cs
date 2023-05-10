using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class UpdateDoorViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private static readonly int OpenParameter = Animator.StringToHash("OpenParameter");
        
        private EcsWorld _world;
        private EcsFilter _animatingDoorFilter;
        private EcsPool<Progress> _doorProgress;
        private EcsPool<DoorView> _doorViewPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _animatingDoorFilter = _world.Filter<Door>().Inc<Animating>().End();
            _doorProgress = _world.GetPool<Progress>();
            _doorViewPool = _world.GetPool<DoorView>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _animatingDoorFilter)
            {
                ref Progress progress = ref _doorProgress.Get(entity);
                ref DoorView doorView = ref _doorViewPool.Get(entity);
                
                doorView.DoorAnimator.SetFloat(OpenParameter, progress.Value);
            }
        }
    }
}