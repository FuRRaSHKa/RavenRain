using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using HalloGames.RavensRain.Gameplay.Player.Interaction;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public class PerkObject : BasePickable
    {
        [SerializeField] private PerkData _perkData;

        public override void PickUp(CharacterEntity characterEntity, out bool needToRemove)
        {
            needToRemove = true;
            IPerk perk = _perkData.GetPerk(characterEntity);
            characterEntity.PerkHandler.AddPerk(_perkData.name, perk);

            Destroy(gameObject);
        }
    }

    public abstract class BasePickable : MonoBehaviour, IPickable
    {
        public abstract void PickUp(CharacterEntity characterEntity, out bool needToRemove);
    }
}

