using Components;
using Data;
using Leopotam.EcsLite;
using Services;
using Zenject;

namespace Systems
{
    public class InitDoorsSystem : IEcsInitSystem
    {
        [Inject] private DoorsService _doorsService;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsPool<Door> doorPool = world.GetPool<Door>();

            for (var index = 0; index < _doorsService.Doors.Count; index++)
            {
                DoorData doorData = _doorsService.Doors[index];
                int entity = systems.GetWorld().NewEntity();

                ref Door door = ref doorPool.Add(entity);

                door.Index = index;
                door.ButtonPosition = doorData.ButtonTransform.position;
                door.ButtonSize = doorData.ButtonSize;
                door.OpenDuration = doorData.OpenDuration;
            }
        }
    }
}