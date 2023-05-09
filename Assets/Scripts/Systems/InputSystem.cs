using Components;
using Data;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject] private CameraData _cameraData;

        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _unitFilter;
        private EcsPool<MoveCommand> _moveCommandPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<Unit>().Inc<ControlledByPlayer>().End();
            _moveCommandPool = _world.GetPool<MoveCommand>();
        }

        public void Run(IEcsSystems systems)
        {
            bool hasRaycast = false;
            RaycastHit hit = default;
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = _cameraData.Camera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    hasRaycast = true;
                }
            }
            
            foreach (int entity in _playerFilter)
            {
                if (_moveCommandPool.Has(entity))
                {
                    _moveCommandPool.Del(entity);
                }

                if (hasRaycast)
                {
                    ref MoveCommand moveCommand = ref _moveCommandPool.Add(entity);
                    moveCommand.Position = hit.point;
                }
            }
        }
    }
}