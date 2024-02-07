using HalloGames.RavensRain.Gameplay.Enemy;
using HalloGames.RavensRain.Gameplay.Perk;
using HalloGames.RavensRain.Management.Factories;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Enemy
{
    public class EncounterManager : MonoBehaviour
    {
        [SerializeField] private LootBox[] _startLootbox;
        [SerializeField] private EnemyEncounter[] _enemyEncounters;
        [SerializeField] private EnemyRandomEncounter _randomEncounter;

        public void SetService(ICharacterFactory characterFactory)
        {
            _randomEncounter.InstallContext(characterFactory);

            foreach (var encounter in _enemyEncounters)
            {
                encounter.InitContext(characterFactory);
                encounter.SpawnEnemies();
            }

            foreach (var loot in _startLootbox)
            {
                loot.SpawnLoot();
            }
        }
    }
}


