using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Player.Interaction;
using HalloGames.RavensRain.Gameplay.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Perk
{
    public class WeaponPerkObject : MonoBehaviour, IPickable
    {
        [SerializeField] private WeaponData _defaultWeaponData;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshFilter _meshFilter;

        private WeaponData _currentData;

        private void Awake()
        {
            ChangeWeaponData(_defaultWeaponData);
        }

        private void ChangeWeaponData(WeaponData weaponData)
        {
            _currentData = weaponData;

            _meshFilter.mesh = weaponData.Mesh;
            _meshRenderer.materials = weaponData.Materials;
        }

        public void PickUp(CharacterEntity characterEntity, out bool needToRemove)
        {
            needToRemove = false;
            ChangeWeaponData(characterEntity.WeaponDataWrapper.SetWeaponData(_currentData));
        }
    }

}

