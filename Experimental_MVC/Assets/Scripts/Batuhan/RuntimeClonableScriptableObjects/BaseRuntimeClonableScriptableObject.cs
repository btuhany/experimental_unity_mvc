using UnityEditor;
using UnityEngine;

namespace Batuhan.RuntimeClonableScriptableObjects
{
    //internal class BaseRuntimeClonableScriptableObject<T> : ScriptableObject where T : BaseRuntimeClonableScriptableObject<T> {}
    public class BaseRuntimeClonableScriptableObject : ScriptableObject
    {
#if UNITY_EDITOR
        public ScriptableObject RuntimeClone;
#endif
    }
}
