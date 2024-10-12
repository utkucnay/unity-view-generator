using UnityEditor.Compilation;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using UnityEditor.Rendering;

public class MonoGenView : MonoBehaviour, IGenMarker
{
    [SerializeField, ShowIf("IsHaveAnyParentGenView")] private bool isInclude;

    private bool IsHaveAnyParentGenView => GetComponentsInParent<MonoGenView>().Any(x => x != this);
    public bool IsInclude { get { return isInclude && enabled; } }

    public string Name => name;

    protected virtual void InitializeComponent()
    {
    }

    public Object GetNativeObject()
    {
        return this;
    }

    [Button("Generate")]
    public void RequestGenerate()
    {
        var parentMonoGenView = GetComponentsInParent<MonoGenView>();

        if (parentMonoGenView.Where(x => x != this).Count() != 0)
        {
            parentMonoGenView.Last().RequestGenerate();
            return;
        }

        Generate();
    }

    private void Generate()
    {
        var monoGenViews = GetComponentsInChildren<MonoGenView>().Reverse();
        var allMarkers = GetComponentsInChildren<IGenMarker>().Where(x => x.IsInclude).Reverse().ToList();
        allMarkers.Remove(this);

        foreach (MonoGenView monoGenView in monoGenViews)
        {
            var markers = monoGenView.GetComponentsInChildren<IGenMarker>().Where(x => x.IsInclude).ToList();  
            markers.Remove(monoGenView);
            markers = markers.Where(x => allMarkers.Contains(x)).ToList();              

            FileGeneratorService fileGeneratorHelper = new(monoGenView.GetType(), markers.ToArray());

            fileGeneratorHelper.GenerateFile();
            fileGeneratorHelper.GenerateEvents();

            foreach (var marker in markers)
            {
                allMarkers.Remove(marker);
            }
        }

        AssetDatabase.Refresh();
        CompilationPipeline.RequestScriptCompilation();
    }
}
