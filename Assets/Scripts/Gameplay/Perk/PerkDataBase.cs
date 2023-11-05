using HalloGames.Architecture.Singletones;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using HalloGames.RavensRain.Gameplay.Weapon;
using UnityEngine;

namespace HalloGames.RavensRain.Management.Perks
{
    public class PerkDataBase : SOSingleton<PerkDataBase>
    {
        [SerializeField] private WeaponData[] _weaponDatas;
        [SerializeField] private PerkData[] _perkDatas;

        public WeaponData[] WeaponDatas => _weaponDatas;
        public PerkData[] PerkDatas => _perkDatas;
    }

}

