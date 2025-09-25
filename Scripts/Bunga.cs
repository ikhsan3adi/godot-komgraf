using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace Godot;

public class Bunga
{
  private float RadiusTengah { get; set; }
  private float RadiusXKelopak { get; set; }
  private float RadiusYKelopak { get; set; }
  private int JumlahKelopak { get; set; }
  private Color Warna { get; set; }
  private Color WarnaKelopak { get; set; }

  private readonly BentukDasar _bentukDasar = new BentukDasar();
  private readonly TransformasiFast _transformasi = new TransformasiFast();

  public Bunga(
    float radiusTengah,
    float radiusXKelopak,
    float radiusYKelopak,
    int jumlahKelopak,
    Color? warna = null,
    Color? warnaKelopak = null)
  {
    this.RadiusTengah = radiusTengah;
    this.RadiusXKelopak = radiusXKelopak;
    this.RadiusYKelopak = radiusYKelopak;
    this.JumlahKelopak = jumlahKelopak;
    this.Warna = warna ?? Colors.Yellow;
    this.WarnaKelopak = warnaKelopak ?? Colors.Purple;
  }

  public void Draw(Node2D targetNode, Vector2 pusatBunga, bool isCartesian = false)
  {
    // tengah bunga
    Vector2 pusatLingkaran = isCartesian ? pusatBunga : ScreenUtils.ConvertToCartesian(pusatBunga.X, pusatBunga.Y);

    List<Vector2> lingkaran = _bentukDasar.Lingkaran(
      ScreenUtils.ConvertToPixel(pusatLingkaran.X, pusatLingkaran.Y),
      (int)RadiusTengah
    );

    GraphicsUtils.PutPixelAll(targetNode, lingkaran, color: Warna);

    // kelopak
    Vector2 pusatElips = new(pusatLingkaran.X + RadiusTengah + RadiusXKelopak, pusatLingkaran.Y); // kartesian

    Matrix4x4 matrix = TransformasiFast.Identity();

    List<Vector2> elips = _bentukDasar.Elips(pusatElips, (int)RadiusXKelopak, (int)RadiusYKelopak);

    GraphicsUtils.PutPixelAllCartesian(targetNode, elips, color: WarnaKelopak);

    float angle = (float)(360 / JumlahKelopak * Math.PI / 180.0);

    _transformasi.RotationCounterClockwise(ref matrix, angle, pusatLingkaran);

    for (int i = 1; i < JumlahKelopak; i++)
    {
      elips = _transformasi.GetTransformPoint(matrix, elips);
      GraphicsUtils.PutPixelAllCartesian(targetNode, elips, color: WarnaKelopak);
    }
  }
}