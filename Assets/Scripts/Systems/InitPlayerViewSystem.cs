using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class InitPlayerViewSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter playerFilter = world.Filter<Unit>().Inc<ControlledByPlayer>().End();
            
            EcsPool<UnitView> unitViewPool = world.GetPool<UnitView>();

            int playerEntity = playerFilter.GetRawEntities()[0];

            ref UnitView unitView = ref unitViewPool.Add(playerEntity);

            GameObject prefab = Resources.Load<GameObject>("Player");
            GameObject instance = Object.Instantiate(prefab);

            unitView.Transform = instance.transform;
        }
    }
}