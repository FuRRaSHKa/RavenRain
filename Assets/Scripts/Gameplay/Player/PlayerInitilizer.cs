using HalloGames.RavensRain.Gameplay.Characters.Stats;
using HalloGames.RavensRain.Gameplay.Player.Movement;
using HalloGames.RavensRain.Gameplay.Player.States;
using HalloGames.RavensRain.Gameplay.Player.Weapon;
using HalloGames.RavensRain.Gameplay.Weapon;
using HalloGames.RavensRain.Management.Factories;
using HalloGames.RavensRain.Management.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HalloGames.RavensRain.Gameplay.Player
{

    public class PlayerInitilizer : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerRotator _playerRotator;
        [SerializeField] private PlayerInputController _playerInputController;
        [SerializeField] private PlayerWeaponInitilizer _playerWeaponInitilizer;
        [SerializeField] private CharacterHealth _characterHealth;

        public void Awake()
        {
            _characterHealth.OnDeathEvent += (charatcter) => Death();
        }

        public void InitPlayer(IActionInput actionInput, IValueInput valueInput, IProjectileFactory projectileFactory)
        {
            _playerMovement.InitInput(valueInput);
            _playerRotator.InitInput(valueInput);
            _playerInputController.InitInput(actionInput);
            _playerWeaponInitilizer.InitWeapon(projectileFactory, valueInput);
        }

        private void Death()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
