using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class StartMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _moveCommandFilter;
        private EcsPool<Moving> _movingPool;
        private EcsPool<MoveCommand> _clickEventPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _moveCommandFilter = _world.Filter<MoveCommand>().End();
            _movingPool = _world.GetPool<Moving>();
            _clickEventPool = _world.GetPool<MoveCommand>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _moveCommandFilter)
            {
                if (_movingPool.Has(entity))
                {
                    _movingPool.Del(entity);
                }

                ref MoveCommand moveCommand = ref _clickEventPool.Get(entity); 
                ref Moving moving = ref _movingPool.Add(entity);
                moving.Position = moveCommand.Position;
            }
        }
    }
}