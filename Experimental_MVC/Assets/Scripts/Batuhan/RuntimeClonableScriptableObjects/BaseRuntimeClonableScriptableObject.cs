using Batuhan.CustomEditor;
using UnityEngine;

namespace Batuhan.RuntimeClonableScriptableObjects
{
    public abstract class RuntimeClonableScriptableObject : ScriptableObject
    {
#if UNITY_EDITOR
        [HideInInspector]
        public bool EDITOR_ShowRuntimeClone = true;
        [ShowIf("EDITOR_ShowRuntimeClone")]
        [ReadOnly]
        public ScriptableObject EDITOR_RuntimeClone;

#endif
    }
}
