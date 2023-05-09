﻿using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class UpdateUnitViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _unitViewFilter;
        private EcsPool<UnitView> _unitViewPool;
        private EcsPool<Unit> _unitPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _unitViewFilter = _world.Filter<UnitView>().End();
            _unitViewPool = _world.GetPool<UnitView>();
            _unitPool = _world.GetPool<Unit>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _unitViewFilter)
            {
                ref Unit unit = ref _unitPool.Get(entity);
                ref UnitView unitView = ref _unitViewPool.Get(entity);

                unitView.Transform.position = unit.Position;
                unitView.Transform.rotation = unit.Rotation;
            }
        }
    }
}