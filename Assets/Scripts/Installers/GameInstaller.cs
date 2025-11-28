using Managers;
using Managers.Storage;
using Signals.PowerUpSignals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PrefabsManager prefabsManager;
        [SerializeField] private CameraController cameraControllerPrefab;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            DeclareSignals();
            
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetInstanceCreator>().AsSingle();
            Container.Bind<PrefabsManager>().FromInstance(prefabsManager).AsSingle();
            Container.Bind<CameraController>().FromComponentInNewPrefab(cameraControllerPrefab).AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<SlowdownPowerUpCollectedSignal>();
            Container.DeclareSignal<SprintRunningPowerUpCollectedSignal>();
            Container.DeclareSignal<FlyingPowerUpCollectedSignal>();
        }
    }
}
