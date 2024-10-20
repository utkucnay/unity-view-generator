using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace ViewGenerator
{
    public class ClassPathFinder
    {
        private string[] classNames;

        public ClassPathFinder(string[] classNames)
        {
            this.classNames = classNames;       
        }

        public ClassPathFinder(string className)
        {
            this.classNames = new string[1] { className };       
        }

        public Dictionary<string, string> GetNameAndPathMap()
        {
            var classPaths = FindClassPath(classNames);
            return classPaths;
        }

        private Dictionary<string, string> FindClassPath(string[] classNames)
        {
            Dictionary<string, string> classPaths = new();
            string[] guids = AssetDatabase.FindAssets("t:Script");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var fileName = Path.GetFileNameWithoutExtension(path);

                if(path.StartsWith("Package"))
                {
                    continue;
                }

                if (fileName.Contains(".gen"))
                {
                    continue;
                }

                foreach (var className in classNames)
                {
                    if (fileName.Equals(className))
                    {
                        classPaths.Add(className, path);
                    }
                }
            }

            return classPaths;
        }
    }
}