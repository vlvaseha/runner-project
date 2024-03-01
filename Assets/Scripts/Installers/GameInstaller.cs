using Managers;
using Managers.Storage;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        #region Fields

        [SerializeField] private PrefabsManager _prefabsManager;

        #endregion

        #region Methods
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetInstanceCreator>().AsSingle();
            Container.Bind<PrefabsManager>().FromInstance(_prefabsManager).AsSingle();
        }
        
        #endregion
    }
}
