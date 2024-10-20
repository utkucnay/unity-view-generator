using UnityEngine;
using System.Reflection;
using System;
using NUnit.Framework;
using System.Text;

namespace ViewGenerator.Internal
{
    internal class MethodGenerator : IGeneratorable, IScopeGenerator, IDisposable
    {
        static readonly string METHOD_FORMAT = "{1} void {0}({2})";
        static readonly string METHOD_VIRTUAL_FORMAT = "{1} override void {0}({2})";

        public event Action DisposeEvent;

        string methodName;
        GenType methodType;
        GenAccess methodAccess;
        StringBuilder parameterBuilder;

        internal MethodGenerator(string methodName, GenType methodType, GenAccess methodAccess)
        {
            this.methodName = methodName;
            this.methodType = methodType;
            this.methodAccess = methodAccess;
        }

        public void AppendParameter(System.Type type, string name = null)
        {
            if (parameterBuilder == null)
            {
                parameterBuilder = new StringBuilder();
                parameterBuilder.Append($"{(type.Name == "Object" ? "object" : type.Name)} {(name == null ? type.Name.ToLower() : name)}");
            }
            else
            {
                parameterBuilder.Append($", {(type.Name == "Object" ? "object" : type.Name)} {(name == null ? type.Name.ToLower() : name)}");
            }
        }

        public void AppendParameter(MarkerParamaterEvent x) => AppendParameter(x.Type, x.Name);
        
        public string Generate()
        {
            var parameterString = parameterBuilder == null ? string.Empty : parameterBuilder.ToString();
            switch (methodType)
            {
                case GenType.None:
                    return string.Format(METHOD_FORMAT, methodName, methodAccess.ToString().ToLower(), parameterString);
                case GenType.Virtual:
                    return string.Format(METHOD_VIRTUAL_FORMAT, methodName, methodAccess.ToString().ToLower(), parameterString);
                default:
                    return string.Format(METHOD_FORMAT, methodName, methodAccess.ToString().ToLower(), parameterString);
            }
        }

        public void Dispose() 
        {
            DisposeEvent?.Invoke();
        }
    }
}