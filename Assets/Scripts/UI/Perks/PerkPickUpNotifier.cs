using HalloGames.Architecture.CoroutineManagement;
using HalloGames.RavensRain.Gameplay.Characters;
using HalloGames.RavensRain.Gameplay.Perk.Data;
using HalloGames.RavensRain.Gameplay.Weapon;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HalloGames.RavensRain.UI.Perks
{
    public class PerkPickUpNotifier : MonoBehaviour
    {
        [SerializeField] private CharacterPerkHandler _playerPerk;
        [SerializeField] private WeaponDataWrapper _playerWeapon;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _image;

        [Header("Fade settings")]
        [SerializeField] private List<MaskableGraphic> _graphics;
        [SerializeField] private float _fadeTime;
        [SerializeField] private float _showTime;

        private IStopable _stopable;
        private float _currentAlpha;

        private void Awake()
        {
            SetAlpha(0);

            _playerPerk.OnPerkAdd += ShowNotify;
            _playerWeapon.OnWeaponChanged += ShowNotify;
        }

        private void ShowNotify(DescriptionStruct perkDescriptionStruct)
        {
            if (_stopable != null)
                Hide(FadeIn);
            else
                FadeIn();

            void FadeIn()
            {
                SetGraphic(perkDescriptionStruct);

                _stopable?.Stop();
                _stopable = RoutineManager.CreateRoutine(this).AnimateFloat(_currentAlpha, 1, SetAlpha, _fadeTime).Finally(Wait).Start();
            }
        }

        private void SetGraphic(DescriptionStruct perkDescriptionStruct)
        {
            _image.sprite = perkDescriptionStruct.Sprite;
            _description.text = perkDescriptionStruct.ShortDescription;
            _name.text = perkDescriptionStruct.Name;
        }

        private void Wait()
        {
            _stopable = RoutineManager.CreateRoutine(this).Wait(_showTime, () => Hide()).Start();
        }

        private void Hide(Action callback = null)
        {
            _stopable?.Stop();
            _stopable = RoutineManager.CreateRoutine(this).AnimateFloat(_currentAlpha, 0, SetAlpha, _fadeTime).Finally(() => 
            {
                _stopable = null;
                callback?.Invoke();
            }).Start();
        }

        private void SetAlpha(float alpha)
        {
            _currentAlpha = alpha;

            foreach (var graphic in _graphics)
            {
                Color color = graphic.color;
                color.a = alpha;
                graphic.color = color;
            }
        }
    }
}

