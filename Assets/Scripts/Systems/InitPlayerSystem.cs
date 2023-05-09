using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class InitPlayerSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsPool<Unit> unitPool = world.GetPool<Unit>();
            EcsPool<ControlledByPlayer> controlledByPlayerPool = world.GetPool<ControlledByPlayer>();

            int entity = systems.GetWorld().NewEntity();

            ref Unit unit = ref unitPool.Add(entity);
            controlledByPlayerPool.Add(entity);

            GameObject prefab = Resources.Load<GameObject>("Player");
            GameObject instance = Object.Instantiate(prefab);
            
            unit.Direction = Vector3.zero;
            unit.Position = Vector3.zero;
            unit.Rotation = Quaternion.identity;
            unit.MoveSpeed = 3;
            unit.RotateSpeed = 10;
            unit.Transform = instance.transform;
        }
    }
}