namespace TimeCounter.Data
{
    public struct CountIndicatorCommonData
    {
        public UnityEngine.Vector3 Position;
        public int Indice;
        public UnityEngine.Color Color;
    }
    public struct CountIndicatorInitData
    {
        public CountIndicatorCommonData CommonData;
        public UnityEngine.Transform ParentTransform;
    }
}
