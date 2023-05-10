using Components;
using Data;
using Leopotam.EcsLite;
using Services;
using Zenject;

namespace Systems
{
    public class InitDoorsViewSystem : IEcsInitSystem
    {
        [Inject] private DoorsService _doorsService;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            EcsFilter doorFilter = world.Filter<Door>().End();
            
            EcsPool<Door> doorPool = world.GetPool<Door>();
            EcsPool<DoorView> doorViewPool = world.GetPool<DoorView>();
            
            foreach (int doorEntity in doorFilter)
            {
                ref Door door = ref doorPool.Get(doorEntity);
                ref DoorView doorView = ref doorViewPool.Add(doorEntity);
                
                DoorData doorData = _doorsService.Doors[door.Index];
                doorView.DoorAnimator = doorData.Animator;
            }
        }
    }
}