using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class StartAnimateDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _doorCollisionEnterFilter;
        private EcsPool<Animating> _animatingPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _doorCollisionEnterFilter = world.Filter<Door>().Inc<CollisionEnterEvent>().End();
            _animatingPool = world.GetPool<Animating>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _doorCollisionEnterFilter)
            {
                _animatingPool.Add(entity);
            }
        }
    }
}