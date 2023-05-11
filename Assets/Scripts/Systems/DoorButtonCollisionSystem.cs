using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class DoorButtonCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _unitFilter;
        private EcsFilter _doorFilter;
        
        private EcsPool<Unit> _unitPool;
        private EcsPool<Door> _doorPool;
        private EcsPool<CollisionEnterEvent> _collisionEnterPool;
        private EcsPool<CollisionExitEvent> _collisionExitPool;
        private EcsPool<CollisionStay> _collisionStayPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _unitFilter = world.Filter<Unit>().End();
            _doorFilter = world.Filter<Door>().End();

            _unitPool = world.GetPool<Unit>();
            _doorPool = world.GetPool<Door>();
            _collisionEnterPool = world.GetPool<CollisionEnterEvent>();
            _collisionExitPool = world.GetPool<CollisionExitEvent>();
            _collisionStayPool = world.GetPool<CollisionStay>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int doorEntity in _doorFilter)
            {
                RemoveEvents(doorEntity);
                
                ref Door door = ref _doorPool.Get(doorEntity);
                
                foreach (int unitEntity in _unitFilter)
                {
                    ref Unit unit = ref _unitPool.Get(unitEntity);

                    float distance = Vector3.Distance(door.ButtonPosition, unit.Position);

                    if (distance < door.ButtonSize)
                    {
                        ProcessCollision(doorEntity, unitEntity);
                    }
                    else
                    {
                        ProcessCollisionExit(doorEntity, unitEntity);
                    }
                }
            }
        }

        public void ProcessCollision(in int doorEntity, in int unitEntity)
        {
            if (_collisionStayPool.Has(doorEntity))
            {
                ref CollisionStay collisionStay = ref _collisionStayPool.Get(doorEntity);

                if (!collisionStay.OtherEntities.Contains(unitEntity))
                {
                    AddCollisionEnter(ref collisionStay, doorEntity, unitEntity);
                }
            }
            else
            {
                ref CollisionStay collisionStay = ref _collisionStayPool.Add(doorEntity);
                collisionStay.Entity = doorEntity;
                collisionStay.OtherEntities = new List<int> { unitEntity };
                
                AddCollisionEnter(ref collisionStay, doorEntity, unitEntity);
            }
        }

        private void AddCollisionEnter(ref CollisionStay collisionStay, in int doorEntity, in int unitEntity)
        {
            ref CollisionEnterEvent collisionEnterEvent = ref _collisionEnterPool.Add(doorEntity);
            collisionEnterEvent.Entity = doorEntity;
            collisionEnterEvent.OtherEntity = unitEntity;
            collisionStay.OtherEntities.Add(unitEntity);
        }

        public void ProcessCollisionExit(in int doorEntity, in int unitEntity)
        {
            if (_collisionStayPool.Has(doorEntity))
            {
                ref CollisionStay collisionStay = ref _collisionStayPool.Get(doorEntity);

                if (collisionStay.OtherEntities.Contains(unitEntity))
                {
                    ref CollisionExitEvent collisionExitEvent = ref _collisionExitPool.Add(doorEntity);
                    collisionExitEvent.Entity = doorEntity;
                    collisionExitEvent.OtherEntity = unitEntity;
                    collisionStay.OtherEntities.Remove(unitEntity);

                    if (collisionStay.OtherEntities.Count == 0)
                    {
                        _collisionStayPool.Del(doorEntity);
                    }
                }
            }
        }

        private void RemoveEvents(in int doorEntity)
        {
            if (_collisionEnterPool.Has(doorEntity))
            {
                _collisionEnterPool.Del(doorEntity);
            }

            if (_collisionExitPool.Has(doorEntity))
            {
                _collisionExitPool.Del(doorEntity);
            }
        }
    }
}