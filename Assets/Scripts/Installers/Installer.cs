using Data;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class Installer : MonoInstaller
    {
        [SerializeField] private CameraData _cameraData;
        [SerializeField] private DoorsService _doorsService;
        
        public override void InstallBindings()
        {
            Container.Bind<TimeService>().AsSingle().Lazy();
            Container.Bind<CameraData>().FromInstance(_cameraData).AsSingle();
            Container.Bind<DoorsService>().FromInstance(_doorsService).AsSingle();
        }
    }
}
