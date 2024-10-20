using System;

namespace ViewGenerator.Internal
{
    internal class ClassGenerator : IGeneratorable, IScopeGenerator, IDisposable
    {
        static string CLASS_FORMAT = "{1} partial class {0}";
        static string SUB_CLASS_FORMAT = "{2} partial class {0} : {1}";

        string className;
        string subClassName;
        GenAccess genAccess;

        public event Action DisposeEvent;

        internal ClassGenerator(string className, GenAccess genAccess)
        {
            this.className = className;
            this.genAccess = genAccess;
        }

        internal ClassGenerator(string subClassName, string className, GenAccess genAccess)
        {
            this.className = className;
            this.subClassName = subClassName;
            this.className = className;
        }

        public string Generate()
        {
            if (string.IsNullOrEmpty(subClassName))
            {
                return string.Format(CLASS_FORMAT, className, genAccess.ToString().ToLower());
            }
            else
            {
                return string.Format(SUB_CLASS_FORMAT, subClassName, className, genAccess.ToString().ToLower());
            }
        }

        public void Dispose() 
        {
            DisposeEvent?.Invoke();
        }
    }
}
