namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class Karya2 : Node2D
{
	private BentukDasar _bentukDasar = new BentukDasar();
	private TransformasiFast _transformasi = new TransformasiFast();

	public override void _Ready()
	{
		ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
		QueueRedraw();
	}

	public override void _Draw()
	{
		MyPersegi();
		MyPersegiTranslation();
		MyPersegiTranslationAndScaling();
		MyPersegiTransform();
	}

	private void MyTransformation()
	{
		Vector2 myPoint = new Vector2(50, 50);
		Matrix4x4 myMatrix = TransformasiFast.Identity();
		_transformasi.Translation(ref myMatrix, 10, 20); // Example usage
		List<Vector2> points = new List<Vector2>() { myPoint };
		PrintUtils.PrintVector2List(points, "Koordinat Awal");
		var transformedPoints = _transformasi.GetTransformPoint(myMatrix, points);
		PrintUtils.PrintVector2List(transformedPoints, "Koordinat Transformasi");
		// PrintUtils.PrintMatrix(myMatrix, "Transformation Matrix");
	}

	private void MyPersegi()
	{
		var persegi1 = _bentukDasar.Persegi(100, 100, 50); // Gambar persegi di posisi (100, 100) dengan ukuran 50
		GraphicsUtils.PutPixelAll(this, persegi1, color: ColorUtils.ColorStorage(3));
	}

	private void MyPersegiTranslation()
	{
		// 1. Define the original square's points
		float x = 100;
		float y = 100;
		float ukuran = 50;
		List<Vector2> persegiPoints = new List<Vector2>()
		{
			new Vector2(x, y),
			new Vector2(x + ukuran, y),
			new Vector2(x + ukuran, y + ukuran),
			new Vector2(x, y + ukuran)
		};
		PrintUtils.PrintVector2List(persegiPoints, "Sebelum Translasi");

		// 2. Create the transformation matrix
		Matrix4x4 translationMatrix = TransformasiFast.Identity();
		_transformasi.Translation(ref translationMatrix, 200, 0); // Translate by (200, 100)

		// 3. Apply the transformation using GetTransformPoint
		List<Vector2> translatedPoints = _transformasi.GetTransformPoint(translationMatrix, persegiPoints);
		PrintUtils.PrintVector2List(translatedPoints, "Setelah Translasi");

		// 4. Use the translated point to create a new square using _bentukDasar.Persegi
		var persegiTranslated = _bentukDasar.Persegi(translatedPoints[0].X, translatedPoints[0].Y, ukuran); // Use translatedPoints[0] as the starting point
		GraphicsUtils.PutPixelAll(this, persegiTranslated, color: ColorUtils.ColorStorage(2));
	}

	private void MyPersegiTranslationAndScaling()
	{
		// 1. Define the original square's points
		float x = 100;
		float y = 100;
		float ukuran = 50;
		List<Vector2> persegiPoints = new List<Vector2>()
		{
			new Vector2(x, y),
			new Vector2(x + ukuran, y),
			new Vector2(x + ukuran, y + ukuran),
			new Vector2(x, y + ukuran)
		};
		PrintUtils.PrintVector2List(persegiPoints, "Sebelum Translasi dan Scaling");

		// 2. Create the transformation matrix for translation
		Matrix4x4 translationMatrix = TransformasiFast.Identity();
		_transformasi.Translation(ref translationMatrix, 200, 100); // Translate by (200, 100)

		// 3. Apply translation using GetTransformPoint
		List<Vector2> translatedPoints = _transformasi.GetTransformPoint(translationMatrix, persegiPoints);

		// 4. Create the transformation matrix for scaling
		Matrix4x4 scalingMatrix = TransformasiFast.Identity();
		_transformasi.Scaling(ref scalingMatrix, 2, 1.5f, translatedPoints[0]); // Scale by 2 in x and 1.5 in y, using the translated point as the center

		// 5. Apply scaling using GetTransformPoint on the translated points
		List<Vector2> scaledPoints = _transformasi.GetTransformPoint(scalingMatrix, translatedPoints);
		PrintUtils.PrintVector2List(scaledPoints, "Setelah Translasi dan Scaling");

		// 6. Use the scaled point to create a new square using _bentukDasar.Persegi
		float newUkuranX = scaledPoints[1].X - scaledPoints[0].X; // Calculate new ukuran in x
		float newUkuranY = scaledPoints[3].Y - scaledPoints[0].Y; // Calculate new ukuran in y
		var persegiTranslatedAndScaled = _bentukDasar.PersegiPanjang(scaledPoints[0].X, scaledPoints[0].Y, newUkuranX, newUkuranY); // Use PersegiPanjang to accommodate different x and y scales
		GraphicsUtils.PutPixelAll(this, persegiTranslatedAndScaled, color: ColorUtils.ColorStorage(1));
	}

	private void MyPersegiTransform()
	{
		// 1. Define the original square's points
		float x = 100;
		float y = 100;
		float ukuran = 50;
		List<Vector2> persegiPoints = new List<Vector2>()
		{
			new Vector2(x, y),
			new Vector2(x + ukuran, y),
			new Vector2(x + ukuran, y + ukuran),
			new Vector2(x, y + ukuran)
		};
		PrintUtils.PrintVector2List(persegiPoints, "Sebelum Transformasi");

		// 2. Create the transformation matrices
		Matrix4x4 translationMatrix = TransformasiFast.Identity();
		_transformasi.Translation(ref translationMatrix, 400, 0); // Translate by (400, 0)

		Matrix4x4 scalingMatrix = TransformasiFast.Identity();
		_transformasi.Scaling(ref scalingMatrix, 2, 1.5f, persegiPoints[0]); // Scale by 2 in x and 1.5 in y, using the original starting point

		Matrix4x4 rotationMatrix = TransformasiFast.Identity();
		_transformasi.RotationClockwise(ref rotationMatrix, Mathf.DegToRad(30), persegiPoints[0]); // Rotate 90 degrees clockwise, using the original starting point


		// 3. Apply the transformations in the correct order (rotate, then scale, then translate)
		List<Vector2> rotatedPoints = _transformasi.GetTransformPoint(rotationMatrix, persegiPoints);
		List<Vector2> scaledPoints = _transformasi.GetTransformPoint(scalingMatrix, rotatedPoints);
		List<Vector2> transformedPoints = _transformasi.GetTransformPoint(translationMatrix, scaledPoints);
		PrintUtils.PrintVector2List(transformedPoints, "Setelah Transformasi");

		// 4. Use the transformed points to create a new polygon using _bentukDasar.Polygon
		Godot.Color color1 = new Godot.Color("#00008B");
		var polygonPoints = _bentukDasar.Polygon(transformedPoints);
		GraphicsUtils.PutPixelAll(this, polygonPoints, color: ColorUtils.ColorStorage(0));
	}

	private void MarginPixel()
	{
		var margin = _bentukDasar.Margin();
		GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
	}

	public override void _ExitTree()
	{
		NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
		NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
		base._ExitTree();
	}
}
