using GameUi;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StartGameWindowPresenter>().AsSingle();
            Container.Bind<GameplayWindowPresenter>().AsSingle();
        }
    }
}
