using Character;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameConfigsInstaller : MonoInstaller
    {
        [SerializeField] private CharacterConfig characterConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(characterConfig).AsSingle();
        }
    }
}

