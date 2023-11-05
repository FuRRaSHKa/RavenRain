using HalloGames.RavensRain.Gameplay.Characters.Stats;
using TMPro;
using UnityEngine;

namespace HalloGames.RavensRain.UI.Stats
{
    public class PlayerHPBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private CharacterHealth _playerHealth;
        [SerializeField] private Transform _fillImage;

        private void Awake()
        {
            _playerHealth.OnHealthChange += UpdateHP;
        }

        private void UpdateHP()
        {
            string text = $"{_playerHealth.CurrentHealth}/{_playerHealth.MaxHealth}";
            _text.text = text;

            float curHealth = _playerHealth.CurrentHealth;
            float maxHealth = _playerHealth.MaxHealth;

            _fillImage.localScale = new Vector3(curHealth == 0 ? 0 : maxHealth / curHealth, 1, 1);
        }
    }

}

