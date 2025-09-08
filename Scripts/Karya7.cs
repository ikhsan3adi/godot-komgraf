using Godot;
using System;
using System.Collections.Generic;

public partial class Karya7 : Node2D
{
  private BentukDasar _bentukDasar = new BentukDasar();

  private Primitif _primitif = new Primitif();

  public override void _Ready()
  {
    base._Ready();

    ScreenUtils.Initialize(GetViewport());

    QueueRedraw();
  }

  public override void _Draw()
  {
    GambarCandy();
  }

  private void GambarCandy()
  {
    // segitiga kiri
    List<Vector2> segitiga1 = _bentukDasar.Polygon([
      ScreenUtils.ConvertToPixel(-350, 67),
      ScreenUtils.ConvertToPixel(-350, -67),
      ScreenUtils.ConvertToPixel(-250, 0),
    ]);
    GraphicsUtils.PutPixelAll(this, segitiga1, GraphicsUtils.DrawStyle.DotDot, Colors.Orange);


    // diamond hijau
    List<Vector2> belahKetupat = _bentukDasar.Polygon([
      ScreenUtils.ConvertToPixel(-250, 0),
      ScreenUtils.ConvertToPixel(-200, 100),
      ScreenUtils.ConvertToPixel(-150, 0),
      ScreenUtils.ConvertToPixel(-200, -100),
    ]);
    GraphicsUtils.PutPixelAll(this, belahKetupat, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // segitiga atas
    List<Vector2> segitiga2 = _bentukDasar.Polygon([
      ScreenUtils.ConvertToPixel(-200, 100),
      ScreenUtils.ConvertToPixel(-150, 0),
      ScreenUtils.ConvertToPixel(-100, 100),
    ]);
    GraphicsUtils.PutPixelAll(this, segitiga2, GraphicsUtils.DrawStyle.DotDot, Colors.Orange);

    // segitiga bawah
    List<Vector2> segitiga3 = _bentukDasar.Polygon([
      ScreenUtils.ConvertToPixel(-200, -100),
      ScreenUtils.ConvertToPixel(-150, 0),
      ScreenUtils.ConvertToPixel(-100, -100),
    ]);
    GraphicsUtils.PutPixelAll(this, segitiga3, GraphicsUtils.DrawStyle.DotDot, Colors.Orange);

    // hexagon
    List<Vector2> hexagon = _bentukDasar.Polygon([
      ScreenUtils.ConvertToPixel(-100, 100),
      ScreenUtils.ConvertToPixel(-150, 0),
      ScreenUtils.ConvertToPixel(-100, -100),

      ScreenUtils.ConvertToPixel(20, -100),
      ScreenUtils.ConvertToPixel(70, 0),
      ScreenUtils.ConvertToPixel(20, 100),
    ]);
    GraphicsUtils.PutPixelAll(this, hexagon, GraphicsUtils.DrawStyle.DotDot, Colors.Red);


    // segitiga kanan
    List<Vector2> segitiga4 = _bentukDasar.Polygon([
      ScreenUtils.ConvertToPixel(170, 67),
      ScreenUtils.ConvertToPixel(170, -67),
      ScreenUtils.ConvertToPixel(70, 0),
    ]);

    GraphicsUtils.PutPixelAll(this, segitiga4, GraphicsUtils.DrawStyle.DotDot, Colors.Orange);
  }

  public void _on_next_btn_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya8.tscn");
  }

  private void MarginPixel()
  {
    var margin = _bentukDasar.Margin();
    GraphicsUtils.PutPixelAll(this, margin, color: Colors.White);
  }


}
