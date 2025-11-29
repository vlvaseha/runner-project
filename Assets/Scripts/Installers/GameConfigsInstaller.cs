using Character;
using PowerUps;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameConfigsInstaller : MonoInstaller
    {
        [SerializeField] private CharacterConfig characterConfig;
        [SerializeField] private BasePowerUpConfig [] powerUpConfigs;
        
        public override void InstallBindings()
        {
            Container.BindInstance(characterConfig).AsSingle();

            foreach (BasePowerUpConfig basePowerUpConfig in powerUpConfigs)
            {
                Container.BindInstance(basePowerUpConfig).WithId(basePowerUpConfig.id);
            }
        }
    }
}

