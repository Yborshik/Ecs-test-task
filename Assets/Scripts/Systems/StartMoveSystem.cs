using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class StartMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _moveCommandFilter;
        private EcsPool<Moving> _movingPool;
        private EcsPool<MoveCommand> _moveCommandPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _moveCommandFilter = world.Filter<MoveCommand>().End();
            _movingPool = world.GetPool<Moving>();
            _moveCommandPool = world.GetPool<MoveCommand>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _moveCommandFilter)
            {
                if (_movingPool.Has(entity))
                {
                    _movingPool.Del(entity);
                }

                ref MoveCommand moveCommand = ref _moveCommandPool.Get(entity); 
                ref Moving moving = ref _movingPool.Add(entity);
                moving.Position = moveCommand.Position;
            }
        }
    }
}