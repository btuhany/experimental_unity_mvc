using Batuhan.RuntimeClonableScriptableObjects;
using UnityEngine;

namespace SnakeExample.Config
{
    [CreateAssetMenu(fileName = "GameConfigDataSO", menuName = "Scriptable Objects/SnakeExample/Data/GameConfigDataSO")]
    public class GameConfigDataSO : ScriptableObject
    {
        [Header("Grid")]
        public int GridWidth = 5;
        public int GridHeight = 5;
        public float GridCellSize = 1.0f;
        public Vector3 GridOriginPos = Vector3.zero;
        [Header("Tick")]
        public int TickIntervalsMillisecond;
        [Header("Snake")]
        public Vector2Int SnakeStartPos = Vector2Int.zero;
        public Vector2Int SnakeStartDir = Vector2Int.right;
        public float SnakeSpeedAddition = 0.2f;
        public int FoodCount = 5;

    }
}
