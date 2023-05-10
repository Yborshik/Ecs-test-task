using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class StartAnimateDoorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _doorCollisionEnterFilter;
        private EcsPool<Animating> _animatingPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _doorCollisionEnterFilter = _world.Filter<Door>().Inc<CollisionEnterEvent>().End();
            _animatingPool = _world.GetPool<Animating>();
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