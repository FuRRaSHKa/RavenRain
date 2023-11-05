using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalloGames.Utils.Layer
{
    public static class LayerUtils
    {
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            return (layerMask.value & (1 << layer)) >= 0;
        }
    }

}
