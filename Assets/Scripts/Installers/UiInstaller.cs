using GameUi;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UiServiceContainer>().AsSingle().NonLazy();
            Container.Bind<GameplayWindowPresenter>().AsSingle().WithArguments(WindowType.GameplayWindow);
        }
    }
}
