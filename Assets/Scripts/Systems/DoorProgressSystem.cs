using Components;
using Leopotam.EcsLite;
using Services;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class DoorProgressSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject] private TimeService _timeService;
        
        private EcsFilter _animatingDoorFilter;
        private EcsPool<Door> _doorPool;
        private EcsPool<Progress> _progressPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _animatingDoorFilter = world.Filter<Door>().Inc<Animating>().End();
            _doorPool = world.GetPool<Door>();
            _progressPool = world.GetPool<Progress>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _animatingDoorFilter)
            {
                if (!_progressPool.Has(entity))
                {
                    _progressPool.Add(entity);
                }

                ref Door door = ref _doorPool.Get(entity);
                ref Progress progress = ref _progressPool.Get(entity);

                float progressDelta = _timeService.DeltaTime / door.OpenDuration;
                progress.Value = Mathf.Clamp01(progress.Value + progressDelta);
            }
        }
    }
}