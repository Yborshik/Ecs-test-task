using Components;
using Leopotam.EcsLite;
using Shared;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class RotateSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject] private TimeService _timeService;
        
        private EcsWorld _world;
        private EcsFilter _unitFilter;
        private EcsPool<Unit> _unitPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _unitFilter = _world.Filter<Unit>().End();
            _unitPool = _world.GetPool<Unit>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _unitFilter)
            {
                ref Unit unit = ref _unitPool.Get(entity);
                Quaternion rotation = Quaternion.LookRotation(unit.Direction);
                unit.Rotation = Quaternion.Lerp(unit.Rotation, rotation, unit.RotateSpeed * _timeService.DeltaTime);
                unit.Transform.rotation = unit.Rotation;
            }
        }
    }
}