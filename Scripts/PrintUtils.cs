namespace Godot;

using Godot;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4

public static class PrintUtils
{
    public static void PrintVector2List(List<Vector2> vectorList, string listName = "Vector2 List")
    {
        if (vectorList == null)
        {
            GD.Print($"{listName} is null.");
            return;
        }

        GD.Print($"{listName}:");
        foreach (Vector2 vector in vectorList)
        {
            GD.Print($"  ({vector.X}, {vector.Y})"); // Indentation for readability
        }
    }

    public static void PrintMatrix(Matrix4x4 matrix, string label = "Matrix")
    {
        GD.Print($"{label}:");
        GD.Print($"| {matrix.M11,6:F2} {matrix.M12,6:F2} {matrix.M13,6:F2} {matrix.M14,6:F2} |");
        GD.Print($"| {matrix.M21,6:F2} {matrix.M22,6:F2} {matrix.M23,6:F2} {matrix.M24,6:F2} |");
        GD.Print($"| {matrix.M31,6:F2} {matrix.M32,6:F2} {matrix.M33,6:F2} {matrix.M34,6:F2} |");
        GD.Print($"| {matrix.M41,6:F2} {matrix.M42,6:F2} {matrix.M43,6:F2} {matrix.M44,6:F2} |");
    }

    public static void PrintVector3(Vector3 vector, string label = "Vector3")
    {
        GD.Print($"{label}: ({vector.X:F2}, {vector.Y:F2}, {vector.Z:F2})");
    }
}