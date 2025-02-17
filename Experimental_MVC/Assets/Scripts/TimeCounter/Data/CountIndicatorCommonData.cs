namespace TimeCounter.Data
{
    public struct CountIndicatorCommonData
    {
        public UnityEngine.Vector3 Position;
        public int Index;
        public UnityEngine.Color Color;
    }
    public struct CountIndicatorInitData
    {
        public CountIndicatorCommonData CommonData;
        public UnityEngine.Transform ParentTransform;
    }
}
