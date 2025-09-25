namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class Transformasi : RefCounted
{
  // Matrix Operations (Make these static utility methods)

  // Identity Matrix
  public static void Matrix3x3Identity(float[,] a)
  {
    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        a[i, j] = (i == j) ? 1 : 0;
      }
    }
  }

  // Matrix Addition (Modifies 'b' in place)
  public static void Matrix3x3Summation(float[,] a, float[,] b)
  {
    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        b[i, j] = a[i, j] + b[i, j];
      }
    }
  }

  // Matrix Subtraction (Modifies 'b' in place)
  public static void Matrix3x3Subtraction(float[,] a, float[,] b)
  {
    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        b[i, j] = a[i, j] - b[i, j];
      }
    }
  }

  // Matrix Multiplication (Modifies 'b' in place)
  public static void Matrix3x3Multiplication(float[,] a, float[,] b)
  {
    float[,] c = new float[3, 3]; // Temporary matrix
    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        for (int k = 0; k < 3; k++)
        {
          c[i, j] += a[i, k] * b[k, j];
        }
      }
    }
    // Copy result back to 'b'
    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        b[i, j] = c[i, j];
      }
    }
  }

  //####################################################################################################
  // Geometric Transformations

  // Get Transformed Points (Modifies 'res' in place)
  public List<Vector2> GetTransformPoint(float[,] m, List<Vector2> res)
  {
    //create new list to save temp result
    List<Vector2> transformedPoints = new List<Vector2>();
    foreach (Vector2 point in res)
    {
      float tempX = point.X * m[0, 0] + point.Y * m[0, 1] + m[0, 2];
      float tempY = point.X * m[1, 0] + point.Y * m[1, 1] + m[1, 2];

      transformedPoints.Add(new Vector2((int)MathF.Floor(tempX), (int)MathF.Floor(tempY)));
    }

    return transformedPoints;
  }

  // Translation
  public void Translation(float[,] matrix, float x, float y, ref Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);
    identity[0, 2] = x;
    identity[1, 2] = y;
    Matrix3x3Multiplication(identity, matrix);

    coord.X += x;
    coord.Y += y;
  }

  // Scaling
  public void Scaling(float[,] matrix, float x, float y, Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);
    identity[0, 0] = x;
    identity[1, 1] = y;

    if (coord.X != 0 || coord.Y != 0)
    {
      // Create a copy of coord for modification within the translation
      Vector2 tempCoord = coord;
      Translation(matrix, -coord.X, -coord.Y, ref tempCoord);
      Matrix3x3Multiplication(identity, matrix);
      tempCoord = coord; // Reset tempCoord to original coord values before the second translation
      Translation(matrix, coord.X, coord.Y, ref tempCoord);

    }
    else
    {
      Matrix3x3Multiplication(identity, matrix);
    }
  }


  // Rotation Clockwise
  public void RotationClockwise(float[,] matrix, float angle, Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);

    // Convert angle to radians
    angle = (float)(angle * Math.PI / 180.0);

    identity[0, 0] = MathF.Cos(angle);
    identity[0, 1] = MathF.Sin(angle);
    identity[1, 0] = -MathF.Sin(angle);
    identity[1, 1] = MathF.Cos(angle);

    if (coord.X != 0 || coord.Y != 0)
    {
      // Create a copy of coord for modification within the translation
      Vector2 tempCoord = coord;
      Translation(matrix, -coord.X, -coord.Y, ref tempCoord);
      Matrix3x3Multiplication(identity, matrix);
      tempCoord = coord; // Reset tempCoord to original coord values
      Translation(matrix, coord.X, coord.Y, ref tempCoord);
    }
    else
    {
      Matrix3x3Multiplication(identity, matrix);
    }
  }
  // Rotation CounterClockwise
  public void RotationCounterClockwise(float[,] matrix, float angle, Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);

    angle = (float)(angle * Math.PI / 180.0);

    identity[0, 0] = MathF.Cos(angle);
    identity[0, 1] = -MathF.Sin(angle);
    identity[1, 0] = MathF.Sin(angle);
    identity[1, 1] = MathF.Cos(angle);

    if (coord.X != 0 || coord.Y != 0)
    {
      // Create a copy of coord for modification within the translation
      Vector2 tempCoord = coord;
      Translation(matrix, -coord.X, -coord.Y, ref tempCoord);
      Matrix3x3Multiplication(identity, matrix);
      tempCoord = coord; // Reset tempCoord to original coord values
      Translation(matrix, coord.X, coord.Y, ref tempCoord);
    }
    else
    {
      Matrix3x3Multiplication(identity, matrix);
    }
  }


  // Shearing
  public void Shearing(float[,] matrix, float x, float y, Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);
    identity[0, 1] = x;
    identity[1, 0] = y;

    if (coord.X != 0 || coord.Y != 0)
    {
      // Create a copy of coord for modification within the translation
      Vector2 tempCoord = coord;
      Translation(matrix, -coord.X, -coord.Y, ref tempCoord);
      Matrix3x3Multiplication(identity, matrix);
      tempCoord = coord;  // Reset tempCoord
      Translation(matrix, coord.X, coord.Y, ref tempCoord);
    }
    else
    {
      Matrix3x3Multiplication(identity, matrix);
    }
  }

  // Reflection to x-axis
  public void ReflectionToX(float[,] matrix, ref Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);
    identity[1, 1] = -1;
    Matrix3x3Multiplication(identity, matrix);

    coord.Y = -coord.Y;
  }

  // Reflection to y-axis
  public void ReflectionToY(float[,] matrix, ref Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);
    identity[0, 0] = -1;
    Matrix3x3Multiplication(identity, matrix);

    coord.X = -coord.X;
  }

  // Reflection to origin
  public void ReflectionToOrigin(float[,] matrix, ref Vector2 coord)
  {
    float[,] identity = new float[3, 3];
    Matrix3x3Identity(identity);
    identity[0, 0] = -1;
    identity[1, 1] = -1;
    Matrix3x3Multiplication(identity, matrix);

    coord.X = -coord.X;
    coord.Y = -coord.Y;
  }



}
