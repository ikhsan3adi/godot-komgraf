using System;

namespace Godot;

public partial class Karya3 : Node2D
{
  private Primitif _primitif = new Primitif();
  private BentukDasar _bentukDasar = new BentukDasar();

  public override void _Ready()
  {
    ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
    QueueRedraw();
  }

  public override void _Draw()
  {
    MarginPixel();
    MyGaris();
  }

  private void MyGaris()
  {
    float panjang = 200.0f;

    Vector2 titikAwal1 = ScreenUtils.ConvertToPixel(20, 200);
    var dda1 = _primitif.LineDDA(titikAwal1.X, titikAwal1.Y, titikAwal1.X + panjang, titikAwal1.Y);
    GraphicsUtils.PutPixelAll(this, dda1, GraphicsUtils.DrawStyle.DotDot, Colors.Yellow, 1, 0);

    Vector2 titikAwal2 = ScreenUtils.ConvertToPixel(300, 200);
    var dda2 = _primitif.LineDDA(titikAwal2.X, titikAwal2.Y, titikAwal2.X + panjang, titikAwal2.Y + panjang);
    GraphicsUtils.PutPixelAll(this, dda2, GraphicsUtils.DrawStyle.DotDot, Colors.Yellow, 1, 0);

    Vector2 titikAwal3 = ScreenUtils.ConvertToPixel(20, 50);
    var bresenham1 = _primitif.LineBresenham(titikAwal3.X, titikAwal3.Y, titikAwal3.X + panjang, titikAwal3.Y);
    GraphicsUtils.PutPixelAll(this, bresenham1, GraphicsUtils.DrawStyle.DotDot, Colors.Cyan, 1, 0);

    Vector2 titikAwal4 = ScreenUtils.ConvertToPixel(300, 50);
    var bresenham2 = _primitif.LineBresenham(titikAwal4.X, titikAwal4.Y, titikAwal4.X + panjang, titikAwal4.Y + panjang);
    GraphicsUtils.PutPixelAll(this, bresenham2, GraphicsUtils.DrawStyle.DotDot, Colors.Cyan, 1, 0);
  }

  private void MarginPixel()
  {
    var margin = _bentukDasar.Margin();
    GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
  }

  public override void _ExitTree()
  {
    NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
    base._ExitTree();
  }
}
