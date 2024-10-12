# Unity View Generator

I love the idea of wpf or winforms auto generetaed view classes. I implemented for Unity.

## How To 

You should create view class 

```csharp
public partial class ...View : MonoGenView 
{
    private void Awake()
    {
        InitializeComponent();
    }
}
```

You can Mark in unity hierarchy and access the references. 
