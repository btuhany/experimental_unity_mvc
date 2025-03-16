using Batuhan.RuntimeClonableScriptableObjects;
using UnityEngine;

namespace SnakeExample.Config
{
    [CreateAssetMenu(fileName = "GameConfigDataSO", menuName = "Scriptable Objects/SnakeExample/Data/GameConfigDataSO")]
    internal class GameConfigDataSO : ScriptableObject
    {
        public int TickIntervalsMillisecond;
    }
}
