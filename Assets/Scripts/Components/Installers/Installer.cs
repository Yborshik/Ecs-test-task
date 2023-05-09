using Shared;
using Zenject;

namespace Components.Installers
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TimeService>().AsSingle().Lazy();
        }
    }
}
