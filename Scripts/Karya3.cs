namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class Karya3 : Node2D
{
    private BentukDasar _bentukDasar = new BentukDasar();
    private TransformasiFast _transformasi = new TransformasiFast();
    private float _rotationAngle = 0f;
    private float _scaleFactor = 1f;
    private float _translationX = 0f;
    private Vector2 _initialPosition = new Vector2(100, 100); // Initial position of the square
    private float _ukuran = 50; // Size of the square

    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
        QueueRedraw();
    }

    public override void _Process(double delta)
    {
        // Update rotation angle
        _rotationAngle += (float)(Mathf.Pi / 2 * delta); // Rotate 90 degrees per second

        // Update scale factor (oscillate between 0.5 and 1.5)
        _scaleFactor += (float)(Mathf.Sin(_rotationAngle * 2) * 0.05f * delta); // Adjust the multiplier for oscillation speed
        _scaleFactor = Mathf.Clamp(_scaleFactor, 0.5f, 1.5f);

        // Update translation in x direction
        _translationX += (float)(100 * delta); // Move 100 pixels per second in x direction

        // If the square reaches the end of the screen width, reset its position
        if (_initialPosition.X + _translationX + _ukuran * _scaleFactor > ScreenUtils.ScreenWidth)
        {
            _translationX = 0;
        }

        QueueRedraw(); // Redraw the scene
    }

    public override void _Draw()
    {
        MyAnimatedPersegiTransform();
    }

    private void MyAnimatedPersegiTransform()
    {
        // 1. Define the original square's points
        List<Vector2> persegiPoints = new List<Vector2>()
        {
            _initialPosition,
            new Vector2(_initialPosition.X + _ukuran, _initialPosition.Y),
            new Vector2(_initialPosition.X + _ukuran, _initialPosition.Y + _ukuran),
            new Vector2(_initialPosition.X, _initialPosition.Y + _ukuran)
        };

        // 2. Create the transformation matrices
        Matrix4x4 translationMatrix = TransformasiFast.Identity();
        _transformasi.Translation(ref translationMatrix, _translationX, 0); // Translate in x direction

        Matrix4x4 scalingMatrix = TransformasiFast.Identity();
        _transformasi.Scaling(ref scalingMatrix, _scaleFactor, _scaleFactor, persegiPoints[0]); // Scale uniformly

        Matrix4x4 rotationMatrix = TransformasiFast.Identity();
        _transformasi.RotationClockwise(ref rotationMatrix, _rotationAngle, persegiPoints[0]); // Rotate clockwise

        // 3. Apply the transformations in the correct order (rotate, then scale, then translate)
        List<Vector2> rotatedPoints = _transformasi.GetTransformPoint(rotationMatrix, persegiPoints);
        List<Vector2> scaledPoints = _transformasi.GetTransformPoint(scalingMatrix, rotatedPoints);
        List<Vector2> transformedPoints = _transformasi.GetTransformPoint(translationMatrix, scaledPoints);

        // 4. Use the transformed points to create a new polygon using _bentukDasar.Polygon
        var polygonPoints = _bentukDasar.Polygon(transformedPoints);
        GraphicsUtils.PutPixelAll(this, polygonPoints, color: ColorUtils.ColorStorage(0));
    }

    public override void _ExitTree()
    {
        NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
        base._ExitTree();
    }
}
