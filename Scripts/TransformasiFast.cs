namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class TransformasiFast : RefCounted
{
    // Use Matrix4x4 from System.Numerics
    public static Matrix4x4 Identity()
    {
        return Matrix4x4.Identity;
    }

    public static Matrix4x4 CreateTranslation(float x, float y, float z = 0f)
    {
        Matrix4x4 matrix = Matrix4x4.Identity;
        matrix.M14 = x; // Translation in x direction
        matrix.M24 = y; // Translation in y direction
        return matrix;
    }

    public static Matrix4x4 CreateScale(float x, float y)
    {
        Matrix4x4 matrix = Matrix4x4.Identity;
        matrix.M11 = x; // Scaling in x direction
        matrix.M22 = y; // Scaling in y direction
        return matrix;
    }

    public static Matrix4x4 CreateRotationZ(float radians)
    {
        Matrix4x4 matrix = Matrix4x4.Identity;
        matrix.M11 = MathF.Cos(radians);
        matrix.M12 = MathF.Sin(radians);
        matrix.M21 = -MathF.Sin(radians);
        matrix.M22 = MathF.Cos(radians);
        return matrix;
    }

    public List<Vector2> GetTransformPoint(Matrix4x4 m, List<Vector2> res)
    {
        List<Vector2> transformedPoints = new List<Vector2>();
        foreach (Vector2 point in res)
        {
            // Convert Vector2 to Vector3 with W=1
            Vector3 tempPoint = new Vector3(point.X, point.Y, 1);

            // PrintUtils.PrintMatrix(m, "Matrix before multiplication:"); // Debug the matrix

            // Manually perform matrix-vector multiplication
            Vector3 transformedPoint3D = new Vector3(
                m.M11 * tempPoint.X + m.M12 * tempPoint.Y + m.M13 * tempPoint.Z + m.M14,
                m.M21 * tempPoint.X + m.M22 * tempPoint.Y + m.M23 * tempPoint.Z + m.M24,
                m.M31 * tempPoint.X + m.M32 * tempPoint.Y + m.M33 * tempPoint.Z + m.M34
            );

            // PrintUtils.PrintVector3(transformedPoint3D, "Transformed Point 3D");

            // Convert back to Vector2
            Vector2 transformedPoint = new Vector2(transformedPoint3D.X, transformedPoint3D.Y);

            transformedPoints.Add(transformedPoint);
        }
        return transformedPoints;
    }

    public void Translation(ref Matrix4x4 matrix, float x, float y)
    {
        matrix = CreateTranslation(x, y) * matrix; // Pre-multiplication
    }

    public void Scaling(ref Matrix4x4 matrix, float x, float y, Vector2 coord)
    {
        if (coord.X != 0 && coord.Y != 0)
        {
            Translation(ref matrix, -coord.X, -coord.Y);
            matrix = CreateScale(x, y) * matrix; // Pre-multiplication
            Translation(ref matrix, coord.X, coord.Y);
        }
        else
        {
            matrix = CreateScale(x, y) * matrix; // Pre-multiplication
        }
    }

    public void RotationClockwise(ref Matrix4x4 matrix, float radians, Vector2 coord) // Use radians directly
    {
        if (coord.X != 0 && coord.Y != 0)
        {
            Translation(ref matrix, -coord.X, -coord.Y);
            matrix = CreateRotationZ(radians) * matrix; // Pre-multiplication
            Translation(ref matrix, coord.X, coord.Y);
        }
        else
        {
            matrix = CreateRotationZ(radians) * matrix; // Pre-multiplication
        }
    }

    public void RotationCounterClockwise(ref Matrix4x4 matrix, float radians, Vector2 coord) // Use radians directly
    {
        if (coord.X != 0 && coord.Y != 0)
        {
            Translation(ref matrix, -coord.X, -coord.Y);
            matrix = CreateRotationZ(-radians) * matrix; // Pre-multiplication
            Translation(ref matrix, coord.X, coord.Y);
        }
        else
        {
            matrix = CreateRotationZ(-radians) * matrix; // Pre-multiplication
        }
    }

    public void Shearing(ref Matrix4x4 matrix, float x, float y, Vector2 coord)
    {
        Matrix4x4 shearingMatrix = Matrix4x4.Identity;
        shearingMatrix.M12 = x;
        shearingMatrix.M21 = y;

        if (coord.X != 0 && coord.Y != 0)
        {
            Translation(ref matrix, -coord.X, -coord.Y);
            matrix = shearingMatrix * matrix; // Pre-multiplication
            Translation(ref matrix, coord.X, coord.Y);
        }
        else
        {
            matrix = shearingMatrix * matrix; // Pre-multiplication
        }
    }

    public void ReflectionToX(ref Matrix4x4 matrix) // Remove ref Vector2 coord
    {
        Matrix4x4 reflectionMatrix = Matrix4x4.Identity;
        reflectionMatrix.M22 = -1; // Reflection along x-axis
        matrix = reflectionMatrix * matrix; // Pre-multiplication
        // Remove: coord.Y = -coord.Y;
    }

    public void ReflectionToY(ref Matrix4x4 matrix)  // Remove ref Vector2 coord
    {
        Matrix4x4 reflectionMatrix = Matrix4x4.Identity;
        reflectionMatrix.M11 = -1; // Reflection along y-axis
        matrix = reflectionMatrix * matrix; // Pre-multiplication
        // Remove: coord.X = -coord.X; 
    }

    public void ReflectionToOrigin(ref Matrix4x4 matrix) // Remove ref Vector2 coord
    {
        Matrix4x4 reflectionMatrix = Matrix4x4.Identity;
        reflectionMatrix.M11 = -1; // Reflection through origin
        reflectionMatrix.M22 = -1;
        matrix = reflectionMatrix * matrix; // Pre-multiplication
        // Remove: coord.X = -coord.X; 
        // Remove: coord.Y = -coord.Y;
    }
}