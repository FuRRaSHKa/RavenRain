using HalloGames.RavensRain.Gameplay.Perk.Data;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public class LootBox : MonoBehaviour
    {
        [SerializeField] private BasePickable[] _possibleLoot;
        [SerializeField] private Transform _spawnPoint;

        public void SpawnLoot()
        {
            BasePickable loot = _possibleLoot[Random.Range(0, _possibleLoot.Length)];
            BasePickable gameObject = Instantiate(loot, _spawnPoint);
            gameObject.transform.position = _spawnPoint.position;
        }
    }
}

