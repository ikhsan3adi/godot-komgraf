namespace Godot;

using Godot;
using System.Collections.Generic;

public static class NodeUtils
{
    public static void DisposeAndNull(RefCounted obj, string objName)
    {
        GD.Print($"{objName} is null: {obj == null}");
        obj?.Dispose();
        obj = null;
        GD.Print($"{objName} is null after dispose and null: {obj == null}");
    }

    public static List<Vector2> CheckPrimitif(Primitif primitif)
    {
        if (primitif == null)
        {
            GD.PrintErr("Node Primitif belum di-assign!");
            return new List<Vector2>();
        }
        return null; // Indicate that primitif is not null
    }
}