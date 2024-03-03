using Managers;
using Managers.Storage;
using Signals.PowerUpSignals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        #region Fields

        [SerializeField] private PrefabsManager _prefabsManager;
        [SerializeField] private CameraController _cameraControllerPrefab;

        #endregion

        #region Methods
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            DeclareSignals();
            
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetInstanceCreator>().AsSingle();
            Container.Bind<PrefabsManager>().FromInstance(_prefabsManager).AsSingle();
            Container.Bind<CameraController>().FromComponentInNewPrefab(_cameraControllerPrefab).AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<SlowdownPowerUpCollectedSignal>();
            Container.DeclareSignal<SprintRunningPowerUpCollectedSignal>();
            Container.DeclareSignal<FlyingPowerUpCollectedSignal>();
        }
        
        #endregion
    }
}
