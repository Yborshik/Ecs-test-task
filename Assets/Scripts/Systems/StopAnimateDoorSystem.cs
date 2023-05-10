using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class StopAnimateDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _doorCollisionExitFilter;
        private EcsPool<Animating> _animatingPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _doorCollisionExitFilter = _world.Filter<Door>().Inc<CollisionExitEvent>().End();
            _animatingPool = _world.GetPool<Animating>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _doorCollisionExitFilter)
            {
                _animatingPool.Del(entity);
            }
        }
    }
}