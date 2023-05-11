using Components;
using Leopotam.EcsLite;
using Services;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class RotateSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject] private TimeService _timeService;
        
        private EcsFilter _unitFilter;
        private EcsPool<Unit> _unitPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _unitFilter = world.Filter<Unit>().Inc<Moving>().End();
            _unitPool = world.GetPool<Unit>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _unitFilter)
            {
                ref Unit unit = ref _unitPool.Get(entity);
                if (unit.Direction.sqrMagnitude > 0)
                {
                    Quaternion rotation = Quaternion.LookRotation(unit.Direction);
                    unit.Rotation = Quaternion.Lerp(unit.Rotation, rotation, unit.RotateSpeed * _timeService.DeltaTime);
                }
            }
        }
    }
}