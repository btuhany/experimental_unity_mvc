using Batuhan.RuntimeClonableScriptableObjects;
using UnityEngine;

namespace SnakeExample.Config
{
    [CreateAssetMenu(fileName = "GameConfigDataSO", menuName = "Scriptable Objects/SnakeExample/Data/GameConfigDataSO")]
    internal class GameConfigDataSO : ScriptableObject
    {
        [Header("Grid")]
        public int GridWidth = 5;
        public int GridHeight = 5;
        public float GridCellSize = 1.0f;
        public Vector3 GridOriginPos = Vector3.zero;
        [Header("Tick")]
        public int TickIntervalsMillisecond;

    }
}
