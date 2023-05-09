using Data;
using Shared;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class Installer : MonoInstaller
    {
        [SerializeField] private CameraData _cameraData;
        
        public override void InstallBindings()
        {
            Container.Bind<TimeService>().AsSingle().Lazy();
            Container.Bind<CameraData>().FromInstance(_cameraData).AsSingle();
        }
    }
}
