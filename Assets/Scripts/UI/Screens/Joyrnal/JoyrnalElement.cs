using HalloGames.RavensRain.Gameplay.Perk.Data;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HalloGames.RavensRain.UI.Joyrnal
{
    public class JoyrnalElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _hoverImage;

        private DescriptionStruct _description;

        public event Action<DescriptionStruct> OnMouseEnter;
        public event Action OnMouseExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverImage.gameObject.SetActive(true);

            OnMouseEnter?.Invoke(_description);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoverImage.gameObject.SetActive(false);

            OnMouseExit?.Invoke();
        }

        public void SetData(DescriptionStruct descriptionStruct)
        {
            _description = descriptionStruct;
            _image.sprite = descriptionStruct.Sprite;
        }
    }
}
