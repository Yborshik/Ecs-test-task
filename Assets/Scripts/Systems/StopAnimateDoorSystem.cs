using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class StopAnimateDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _doorCollisionExitFilter;
        private EcsPool<Animating> _animatingPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _doorCollisionExitFilter = world.Filter<Door>().Inc<CollisionExitEvent>().End();
            _animatingPool = world.GetPool<Animating>();
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