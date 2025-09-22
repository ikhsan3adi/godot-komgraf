namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
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
    MyBunga();
  }

  private void MyBunga()
  {
    // tengah bunga
    Vector2 pusatLingkaran = new(0, 0);

    List<Vector2> lingkaran = _bentukDasar.Lingkaran(
      ScreenUtils.ConvertToPixel(pusatLingkaran.X, pusatLingkaran.Y),
      50
    );

    GraphicsUtils.PutPixelAll(this, lingkaran, color: Colors.Yellow);


    // kelopak
    Vector2 pusatElips = new(100, 0); // kartesian
    Matrix4x4 matrix = TransformasiFast.Identity();

    List<Vector2> elips = _bentukDasar.Elips(pusatElips, 50, 35);

    GraphicsUtils.PutPixelAll(
      this,
      elips.Select(e => ScreenUtils.ConvertToPixel(e.X, e.Y)).ToList(),
      color: Colors.Purple
    );

    float angle = (float)(45 * Math.PI / 180.0); // deg to radian

    _transformasi.RotationCounterClockwise(ref matrix, angle, pusatLingkaran); // pivot adalah pusatLingkaran

    for (int i = 0; i < 7; i++)
    {
      elips = _transformasi.GetTransformPoint(matrix, elips);
      GraphicsUtils.PutPixelAll(
        this,
        elips.Select(e => ScreenUtils.ConvertToPixel(e.X, e.Y)).ToList(), // cartesian to screen
        color: Colors.Purple
      );
    }
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
