using NaughtyAttributes;
using UnityEngine;

public abstract class GenMarker : MonoBehaviour, IGenMarker
{
    public bool isInclude = true;

    [ShowIf("isInclude")]
    public bool isOverrideName;
    [ShowIf("isOverrideName")]
    public string overrideName;

    public bool IsInclude { get { return isInclude && enabled; } }
    public string Name { get { return isOverrideName ? overrideName : name; } }
    public abstract Object GetNativeObject();

    public bool IsHaveParentMonoGenView { get { return GetComponentInParent<MonoGenView>() != null; } }

    [Button, ShowIf("IsHaveParentMonoGenView")]
    public void Generate()
    {
        GetComponentInParent<MonoGenView>()!.RequestGenerate();
    }
}
