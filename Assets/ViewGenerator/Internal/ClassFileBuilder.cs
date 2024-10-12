using System.Collections.Generic;
using System.Text;

internal class ClassFileBuilder
{
    StringBuilder classBuilder;
    int indentCount;
    int indentIndex = 4;
    string indent = "";

    Stack<ScopeGenerator> scopeGeneratorStack;

    internal ClassFileBuilder()
    {
        classBuilder = new();
        scopeGeneratorStack = new();

        for (int i = 0; i < indentIndex; i++)
        {
            indent += ' ';           
        }
    }

    internal ClassFileBuilder(int indentIndex, int indentCount = 4)
    {
        classBuilder = new();
        scopeGeneratorStack = new();
        this.indentCount = indentCount;
        this.indentIndex = indentIndex;

        for (int i = 0; i < indentIndex; i++)
        {
            indent += ' ';
        }
    }

    internal void Append(IGeneratorable generatorable)
    {
        for (int i = 0; i < indentCount; i++) 
        {
            classBuilder.Append(indent);    
        }

        classBuilder.AppendLine(generatorable.Generate());

        if (generatorable is IScopeGenerator scopeGeneratorObject)
        {
            var scopeGenerator = new ScopeGenerator();
            scopeGeneratorObject.DisposeEvent += AppendScope;

            Append(scopeGenerator);

            scopeGeneratorStack.Push(scopeGenerator);
            indentCount++;
        }
    }

    internal void Append(string line)
    {
        for (int i = 0; i < indentCount; i++)
        {
            classBuilder.Append(indent);
        }

        classBuilder.AppendLine(line);
    }

    internal void AppendEmpty()
    {
        classBuilder.AppendLine();
    }

    private void AppendScope()
    {
        var scopeGenerator = scopeGeneratorStack.Pop();
        indentCount--;
        Append(scopeGenerator);
    }

    public override string ToString()
    {
        return classBuilder.ToString();
    }
}
