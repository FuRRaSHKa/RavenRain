using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HalloGames.RavensRain.UI.Perks
{
    public class PerkLayoutElement : MonoBehaviour
    {
        [SerializeField] private Image _perkImage;
        [SerializeField] private TMP_Text _stackCountText;

        private int _stackCount = 1;

        private DescriptionStruct _perkDescriptionStruct;

        public void Init(DescriptionStruct perkDescriptionStruct)
        {
            _perkDescriptionStruct = perkDescriptionStruct;

            _perkImage.sprite = _perkDescriptionStruct.Sprite;
        }

        public void Stack()
        {
            _stackCount++;

            if (_stackCount > 1)
                _stackCountText.gameObject.SetActive(true);

            _stackCountText.text = _stackCount.ToString();
        }
    }

}

