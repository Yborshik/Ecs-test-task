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

        _systems
            .Add(new TimeSystem())
            .Add(new InitPlayerSystem())
            .Add(new InputSystem())
            .Add(new StartMoveSystem())
            .Add(new MoveSystem())
            .Add(new RotateSystem())
            .Add(new UpdateUnitViewSystem())
            .Add(new CameraSystem());

        Inject();
        
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

    private void Inject()
    {
        foreach (IEcsSystem system in _systems.GetAllSystems())
        {
            _diContainer.Inject(system);
        }
    }
}
