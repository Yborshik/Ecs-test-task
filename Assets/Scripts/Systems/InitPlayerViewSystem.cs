using Components;
using Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class InitPlayerViewSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter playerFilter = world.Filter<Unit>().End();
            
            EcsPool<UnitView> unitViewPool = world.GetPool<UnitView>();

            foreach (int playerEntity in playerFilter)
            {
                ref UnitView unitView = ref unitViewPool.Add(playerEntity);

                GameObject prefab = Resources.Load<GameObject>("Player");
                GameObject instance = Object.Instantiate(prefab);
                UnitData unitData = instance.GetComponent<UnitData>();

                unitView.Transform = unitData.transform;
                unitView.Animator = unitData.Animator;
            }
        }
    }
}