using Components;
using Leopotam.EcsLite;
using Services;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class MoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject] private TimeService _timeService;
        
        const float DistanceToStop = 0.001f;
        
        private EcsFilter _movingFilter;
        private EcsPool<Unit> _unitPool;
        private EcsPool<Moving> _movingPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _movingFilter = world.Filter<Unit>().Inc<Moving>().End();
            _unitPool = world.GetPool<Unit>();
            _movingPool = world.GetPool<Moving>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _movingFilter)
            {
                ref Unit unit = ref _unitPool.Get(entity);
                ref Moving moving = ref _movingPool.Get(entity);
                
                unit.Position = Vector3.MoveTowards(unit.Position, moving.Position,
                    unit.MoveSpeed * _timeService.DeltaTime);

                Vector3 offset = unit.Position - moving.Position;
                if (offset.sqrMagnitude <= DistanceToStop)
                {
                    unit.Position = moving.Position;
                    _movingPool.Del(entity);
                }
                else
                {
                    unit.Direction = offset.normalized;
                }
            }
        }
    }
}