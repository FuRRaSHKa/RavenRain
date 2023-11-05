using HalloGames.Architecture.UI.ScreenManagement;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using HalloGames.RavensRain.Management.Perks;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HalloGames.RavensRain.UI.Joyrnal
{
    public class JoyrnalScreen : ScreenBehavior
    {
        [SerializeField] private JoyrnalElement _prefab;
        [SerializeField] private Transform _parent;

        [SerializeField] private TMP_Text _nameElement;
        [SerializeField] private TMP_Text _descriptionElement;
        [SerializeField] private TMP_Text _loreElement;

        private void Awake()
        {
            FillElements();
        }

        private void FillElements()
        {
            List<DescriptionStruct> descriptionStructs = new List<DescriptionStruct>();
            descriptionStructs.AddRange(PerkDataBase.Instance.WeaponDatas.Select(s => s.DescriptionStruct));
            descriptionStructs.AddRange(PerkDataBase.Instance.PerkDatas.Select(s => s.DescriptionStruct));

            foreach(var descriptionStruct in descriptionStructs)
            {
                JoyrnalElement element = Instantiate(_prefab, _parent);
                element.SetData(descriptionStruct);

                element.OnMouseEnter += ElementHovered;
                element.OnMouseExit += ElementLosed;
            }
        }

        private void ElementHovered(DescriptionStruct descriptionStruct)
        {
            _nameElement.text = descriptionStruct.Name;
            _descriptionElement.text = descriptionStruct.ShortDescription;
            _loreElement.text = descriptionStruct.LoreDescription;
        }

        private void ElementLosed()
        {
            _nameElement.text = "";
            _descriptionElement.text = "";
            _loreElement.text = "";
        }

    }
}

