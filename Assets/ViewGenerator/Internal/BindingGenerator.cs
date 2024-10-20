namespace ViewGenerator.Internal
{
    internal class StartBindingGenerator : IGeneratorable
    {
        static readonly string START_BINDING = "var markers = GetComponentsInChildren<IGenMarker>().Where(x => x.IsInclude).ToList();";

        public string Generate()
        {
            return START_BINDING;
        }
    }

    internal class BindingGenerator : IGeneratorable
    {
        static readonly string BINDING_FORMAT = "_{0} = markers.Find(x => x.Name == \"{1}\").GetNativeObject() as {2};";

        IGenMarker genMarker;

        internal BindingGenerator(IGenMarker genMarker)
        {
            this.genMarker = genMarker;
        }

        public string Generate()
        {
            return string.Format(BINDING_FORMAT, genMarker.Name.FirstCharacterToLower(), 
                genMarker.Name, genMarker.GetNativeObject().GetType().Name);
        }
    }
}