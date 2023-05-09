using Leopotam.EcsLite;
using Services;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class TimeSystem : IEcsRunSystem
    {
        [Inject] private TimeService _timeService;

        public void Run(IEcsSystems systems)
        {
            _timeService.Time = Time.time;
            _timeService.UnscaledTime = Time.unscaledTime;
            _timeService.DeltaTime = Time.deltaTime;
            _timeService.UnscaledDeltaTime = Time.unscaledDeltaTime;
        }
    }
}