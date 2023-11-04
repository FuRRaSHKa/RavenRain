using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.RavensRain.Gameplay.Characters.Stats
{
    public abstract class StatModifyer : MonoBehaviour
    {

    }

    public interface IStatModifyer
    {
        public float ModifyStat(float input);
    }

}

