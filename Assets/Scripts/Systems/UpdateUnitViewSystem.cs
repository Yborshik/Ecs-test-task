using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class UpdateUnitViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        
        private EcsFilter _unitViewFilter;
        private EcsPool<UnitView> _unitViewPool;
        private EcsPool<Unit> _unitPool;
        private EcsPool<Moving> _movingPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _unitViewFilter = world.Filter<UnitView>().End();
            _unitViewPool = world.GetPool<UnitView>();
            _unitPool = world.GetPool<Unit>();
            _movingPool = world.GetPool<Moving>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _unitViewFilter)
            {
                ref Unit unit = ref _unitPool.Get(entity);
                ref UnitView unitView = ref _unitViewPool.Get(entity);

                unitView.Transform.position = unit.Position;
                unitView.Transform.rotation = unit.Rotation;

                bool isMoving = _movingPool.Has(entity);
                unitView.Animator.SetBool(IsMoving, isMoving);
            }
        }
    }
}