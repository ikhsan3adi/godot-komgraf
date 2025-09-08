using Godot;
using System;
using System.Collections.Generic;

public partial class Karya8 : Node2D
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
    GambarPenguin();
  }

  private void GambarPenguin()
  {
    Vector2 titikAwal = ScreenUtils.ConvertToPixel(-50, 250);

    // jajar genjang atas
    List<Vector2> jajarGenjang1 = _bentukDasar.JajarGenjang(titikAwal, 100, 70, -40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang1, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // hexagon 1
    List<Vector2> hexagon1 = _bentukDasar.Polygon([
      new (titikAwal.X, titikAwal.Y),
      new (titikAwal.X + 100, titikAwal.Y),
      new (titikAwal.X + 140, titikAwal.Y + 70),
      new (titikAwal.X + 100, titikAwal.Y + 140),
      new (titikAwal.X, titikAwal.Y + 140),
      new (titikAwal.X - 40, titikAwal.Y + 70),
    ]);
    GraphicsUtils.PutPixelAll(this, hexagon1, GraphicsUtils.DrawStyle.DotDot, Colors.Red);

    // hexagon 2
    List<Vector2> hexagon2 = _bentukDasar.Polygon([
      new (titikAwal.X, titikAwal.Y + 140),
      new (titikAwal.X + 100, titikAwal.Y + 140),
      new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
      new (titikAwal.X + 100, titikAwal.Y + 140 + 140),
      new (titikAwal.X, titikAwal.Y + 140 + 140),
      new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
    ]);
    GraphicsUtils.PutPixelAll(this, hexagon2, GraphicsUtils.DrawStyle.DotDot, Colors.Red);

    // diamond 1 (kiri-atas)
    List<Vector2> diamond1 = _bentukDasar.Polygon([
      new (titikAwal.X, titikAwal.Y + 140),
      new (titikAwal.X - 40, titikAwal.Y + 70),
      new (titikAwal.X - 80, titikAwal.Y + 140),
      new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
    ]);
    GraphicsUtils.PutPixelAll(this, diamond1, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // diamond 2 (kananatas)
    List<Vector2> diamond2 = _bentukDasar.Polygon([
      new (titikAwal.X + 140, titikAwal.Y + 70),
      new (titikAwal.X + 100, titikAwal.Y + 140),
      new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
      new (titikAwal.X + 180, titikAwal.Y + 140),
    ]);
    GraphicsUtils.PutPixelAll(this, diamond2, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // diamond 3 (kiri-bawah)
    List<Vector2> diamond3 = _bentukDasar.Polygon([
      new (titikAwal.X, titikAwal.Y + 140 + 140),
      new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
      new (titikAwal.X - 80, titikAwal.Y + 140 + 140),
      new (titikAwal.X - 40, titikAwal.Y + 70 + 140 + 140),
    ]);
    GraphicsUtils.PutPixelAll(this, diamond3, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // diamond 4 (kanan-bawah)
    List<Vector2> diamond4 = _bentukDasar.Polygon([
      new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
      new (titikAwal.X + 100, titikAwal.Y + 140 + 140),
      new (titikAwal.X + 140, titikAwal.Y + 70 + 140 + 140),
      new (titikAwal.X + 180, titikAwal.Y + 140 + 140),
    ]);
    GraphicsUtils.PutPixelAll(this, diamond4, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // lengan kiri
    List<Vector2> leftHand = _bentukDasar.Polygon([
      new (titikAwal.X - 40, titikAwal.Y + 70),
      new (titikAwal.X - 80, titikAwal.Y + 140),
      new (titikAwal.X - 150, titikAwal.Y + 140 + 50),
      new (titikAwal.X - 110, titikAwal.Y + 110),
    ]);
    GraphicsUtils.PutPixelAll(this, leftHand, GraphicsUtils.DrawStyle.DotDot, Colors.DarkCyan);

    // lengan kanan
    List<Vector2> rightHand = _bentukDasar.Polygon([
      new (titikAwal.X + 100 + 40, titikAwal.Y + 70),
      new (titikAwal.X + 100 + 80, titikAwal.Y + 140),
      new (titikAwal.X + 100 + 150, titikAwal.Y + 140 + 50),
      new (titikAwal.X + 100 + 110, titikAwal.Y + 110),
    ]);
    GraphicsUtils.PutPixelAll(this, rightHand, GraphicsUtils.DrawStyle.DotDot, Colors.DarkCyan);
  }


  public void _on_next_btn_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya9.tscn");
  }

  private void MarginPixel()
  {
    var margin = _bentukDasar.Margin();
    GraphicsUtils.PutPixelAll(this, margin, color: Colors.White);
  }


}
