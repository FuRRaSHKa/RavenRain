using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using HalloGames.RavensRain.Gameplay.Player.Interaction;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public class PerkObject : MonoBehaviour, IPickable
    {
        [SerializeField] private PerkData _perkData;

        public void PickUp(CharacterEntity characterEntity, out bool needToRemove)
        {
            needToRemove = true;
            IPerk perk = _perkData.GetPerk(characterEntity);
            characterEntity.PerkHandler.AddPerk(_perkData.name, perk);

            Destroy(gameObject);
        }
    }
}

