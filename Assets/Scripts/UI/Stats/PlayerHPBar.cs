using HalloGames.RavensRain.Gameplay.Characters.Stats;
using TMPro;
using UnityEngine;

namespace HalloGames.RavensRain.UI.Stats
{
    public class PlayerHPBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private CharacterHealth _characterHealth;
        [SerializeField] private Transform _fillImage;

        private void Awake()
        {
            _characterHealth.OnHealthChange += UpdateHP;
        }

        private void UpdateHP()
        {
            string text = $"{_characterHealth.CurrentHealth}/{_characterHealth.MaxHealth}";
            _text.text = text;

            float curHealth = _characterHealth.CurrentHealth;
            float maxHealth = _characterHealth.MaxHealth;

            _fillImage.localScale = new Vector3(curHealth / maxHealth, 1, 1);
        }
    }

}

