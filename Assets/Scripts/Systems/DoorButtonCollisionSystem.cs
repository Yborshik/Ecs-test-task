using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class DoorButtonCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter _unitFilter;
        private EcsFilter _doorFilter;
        
        private EcsPool<Unit> _unitPool;
        private EcsPool<Door> _doorPool;
        private EcsPool<CollisionEnterEvent> _collisionEnterPool;
        private EcsPool<CollisionExitEvent> _collisionExitPool;
        private EcsPool<CollisionStay> _collisionStayPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _unitFilter = _world.Filter<Unit>().End();
            _doorFilter = _world.Filter<Door>().End();

            _unitPool = _world.GetPool<Unit>();
            _doorPool = _world.GetPool<Door>();
            _collisionEnterPool = _world.GetPool<CollisionEnterEvent>();
            _collisionExitPool = _world.GetPool<CollisionExitEvent>();
            _collisionStayPool = _world.GetPool<CollisionStay>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int doorEntity in _doorFilter)
            {
                ref Door door = ref _doorPool.Get(doorEntity);
                
                foreach (int unitEntity in _unitFilter)
                {
                    ref Unit unit = ref _unitPool.Get(unitEntity);
                    
                    
                }
            }
        }
    }
}