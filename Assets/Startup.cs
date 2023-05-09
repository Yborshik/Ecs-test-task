using Leopotam.EcsLite;
using Systems;
using UnityEngine;
using Zenject;

public class Startup : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    
    private EcsWorld _world;
    private IEcsSystems _systems;
    
    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        AddSystem(new TimeSystem());
        AddSystem(new InitPlayerSystem());
        
        _systems.Init();
    }
    
    private void Update()
    {
        _systems?.Run();
    }

    private void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }

        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }

    public void AddSystem<T>(T system) where T : IEcsSystem
    {
        _diContainer.Inject(system);
        _systems.Add(system);
    }
}
