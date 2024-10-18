using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

internal class FileGeneratorService
{
    List<UsingNamespaceGenerator> genUsingNamespaces;
    NamespaceGenerator genNamespace;
    ClassGenerator genClass;
    List<FieldGenerator> genFields;
    List<PropertyGenerator> genProps;
    MethodGenerator genInitMethod;

    Type classType;

    IGenMarker[] markers;

    internal FileGeneratorService(Type classType, IGenMarker[] markers)
    {
        var genTypeObjects = markers.Select(x => x.GetNativeObject().GetType()).ToList();

        HashSet<string> uniqueNamespaceHashSet = new HashSet<string>()
        {
            "UnityEngine",
            "System.Linq",
            "System",
        };

        foreach (var genTypeObject in genTypeObjects)
        {
            if(string.IsNullOrEmpty(genTypeObject.Namespace)) continue;
            uniqueNamespaceHashSet.Add(genTypeObject.Namespace);
        }

        genUsingNamespaces = new List<UsingNamespaceGenerator>();
        foreach (var uniqueNamespace in uniqueNamespaceHashSet)
        {
            genUsingNamespaces.Add(new UsingNamespaceGenerator(uniqueNamespace));
        }

        if (!string.IsNullOrEmpty(classType.Namespace))
        {
            genNamespace = new NamespaceGenerator(classType.Namespace);
        }

        if (!string.IsNullOrEmpty(classType.BaseType?.Name))
        {
            genClass = new ClassGenerator(classType.Name, classType.BaseType.Name, GenAccess.Public);
        }
        else
        {
            genClass = new ClassGenerator(classType.Name, GenAccess.Public);
        }

        genFields = new List<FieldGenerator>();
        foreach (var marker in markers)
        {
            genFields.Add(new FieldGenerator(marker.GetNativeObject().GetType().Name, marker.Name, GenAccess.Private));
        }

        genProps = new List<PropertyGenerator>();
        foreach (var marker in markers)
        {
            genProps.Add(new PropertyGenerator(marker.GetNativeObject().GetType().Name, marker.Name, GenAccess.Public));
        }

        genInitMethod = new MethodGenerator("InitializeComponent", GenType.Virtual, GenAccess.Protected);

        this.classType = classType;
        this.markers = markers;
    }

    public void GenerateFile()
    {
        //TODO: Find file and generete in "Generated Folder"
        var generetedViewName = classType.Name;
        var completePath = new ClassPathFinder(generetedViewName).GetNameAndPathMap().First().Value;
        completePath = Path.Combine(Path.GetDirectoryName(completePath), $"{generetedViewName}.gen.cs");

        if (!File.Exists(completePath))
        {
            var fileStream = File.Create(completePath);
            fileStream.Close();
            Debug.Log($"File Created at {completePath}");
        }

        ClassFileBuilder classFileBuilder = new();

        foreach (var genUsingNamespace in genUsingNamespaces)
        {
            classFileBuilder.Append(genUsingNamespace);
        }

        classFileBuilder.AppendEmpty();

        if (genNamespace != null)
        {
            classFileBuilder.Append(genNamespace);
        }

        using (genClass)
        {
            classFileBuilder.Append(genClass);

            foreach (var genField in genFields)
            {
                classFileBuilder.Append(genField);
            }

            classFileBuilder.AppendEmpty();

            foreach (var genProp in genProps)
            {
                classFileBuilder.Append(genProp);
            }

            classFileBuilder.AppendEmpty();


            using (genInitMethod)
            {
                classFileBuilder.Append(genInitMethod);

                classFileBuilder.Append(new StartBindingGenerator());
                classFileBuilder.AppendEmpty();

                foreach (var marker in markers)
                {
                    classFileBuilder.Append(new BindingGenerator(marker));
                }

                classFileBuilder.AppendEmpty();

                foreach (var marker in markers)
                {
                    if (marker is IMarkerEvent markerEvent)
                    {
                        foreach (var baseMarkerEvent in markerEvent.GetMarkerEvents())
                        {
                            classFileBuilder.Append(baseMarkerEvent.SubscribeEvent);
                        }
                    }
                }
            }
        }

        genNamespace?.Dispose();

        File.WriteAllText(completePath, classFileBuilder.ToString());
    }

    public void GenerateEvents()
    {
        var generetedViewName = classType.Name;
        var completePath = new ClassPathFinder(generetedViewName).GetNameAndPathMap().First().Value;
        var eventMarkers = markers.Where(x => x is IMarkerEvent).Select(x => x as IMarkerEvent).ToList();

        if (File.Exists(completePath))
        {
            var methods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(x => x.Name).ToList();

            foreach (var eventMarker in eventMarkers)
            {
                var eventNames = eventMarker.GetMarkerEvents().Select(x => x.EventName);

                foreach (var eventName in eventNames)
                {
                    if (!methods.Contains(eventName))
                    {
                        var classText = File.ReadAllText(completePath);
                        var newClassText = AppendEvent(classText, eventName, eventMarker);
                        File.WriteAllText(completePath, newClassText);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Didnt find a file");
        }
    }

    private string AppendEvent(string classText, string eventName, IMarkerEvent markerEvent)
    {
        var className = classType.Name;
        var classNameIndex = classText.IndexOf(className);
        var classFirstScopeIndex = classText.IndexOf("{", classNameIndex);

        int scopeCount = 1;
        int index = classFirstScopeIndex + 1;

        while (scopeCount > 0 && index < classText.Length)
        {
            if (classText[index] == '{')
                scopeCount++;
            if (classText[index] == '}')
                scopeCount--;

            index++;
        }

        if (index == classText.Length - 1)
        {
            return classText;
        }

        index -= 1;
        while (classText[index] != '\n')
            index -= 1;
        
        int indentCount = 0;

        for (int i = index; i > 0; i--)
        {
            if (classText[i] == '{')
                indentCount++;
            if (classText[i] == '}')
                indentCount--;
        }

        ClassFileBuilder sb = new(indentCount);

        sb.AppendEmpty();

        using (var eventMethodGenerator = new MethodGenerator(eventName, GenType.None, GenAccess.Private))
        {
            var markerEventModel = markerEvent.GetMarkerEvents().Find(x => x.EventName == eventName);

            var paramaterEvents = MarkerEventModel.DefaultParameterEvents;

            if (markerEventModel.ParamaterEvents != null)
            {
                paramaterEvents = markerEventModel.ParamaterEvents;
            }

            foreach (var paramaterEvent in paramaterEvents)
            {
                eventMethodGenerator.AppendParameter(paramaterEvent);
            }

            sb.Append(eventMethodGenerator);
            sb.AppendEmpty();
        }

        classText = classText.Insert(index, sb.ToString());

        return classText;
    }
}
