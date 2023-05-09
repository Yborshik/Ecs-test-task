using Components;
using Data;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class CameraSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject] private CameraData _cameraData;

        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<Unit> _unitPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<ControlledByPlayer>().End();
            _unitPool = _world.GetPool<Unit>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerFilter)
            {
                ref Unit unit = ref _unitPool.Get(entity);
                Quaternion rotation = Quaternion.Euler(_cameraData.Angle);
                Vector3 directionPosition = rotation * new Vector3(0, 0, -_cameraData.Distance);
                Vector3 newPosition = unit.Position + directionPosition;

                _cameraData.Transform.rotation = rotation;
                _cameraData.Transform.position = newPosition;                
            }
        }
    }
}