using Godot;
using System;
using System.Collections.Generic;

public partial class Karya9 : Node2D
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
    GambarPesawat();
  }

  private void GambarPesawat()
  {
    Vector2 titikAwal = ScreenUtils.ConvertToPixel(-500, -50);

    // kepala
    List<Vector2> trapesium1 = _bentukDasar.TrapesiumSamaKaki(titikAwal, 120, 200, 100);
    GraphicsUtils.PutPixelAll(this, trapesium1, GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

    // jajargenjang tengah
    List<Vector2> jajarGenjang1 = _bentukDasar.JajarGenjang(new(titikAwal.X + 200, titikAwal.Y), 120, 100, -40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang1, GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang2 = _bentukDasar.JajarGenjang(new(titikAwal.X + 200 + 120, titikAwal.Y), 120, 100, -40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang2, GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang3 = _bentukDasar.JajarGenjang(new(titikAwal.X + 200 + 240, titikAwal.Y), 120, 100, -40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang3, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // ekor
    List<Vector2> ekor = _bentukDasar.Polygon([
      new(titikAwal.X + 200 + 360, titikAwal.Y),
      new(titikAwal.X + 200 + 360 - 40, titikAwal.Y - 100),
      new(titikAwal.X + 200 + 360 + 60, titikAwal.Y - 240),
      new(titikAwal.X + 200 + 360 + 170, titikAwal.Y - 240),
    ]);
    GraphicsUtils.PutPixelAll(this, ekor, GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);


    // trapesium atas
    List<Vector2> trapesium2 = _bentukDasar.TrapesiumSamaKaki(new(titikAwal.X + 200 - 40, titikAwal.Y - 100), 120, 200, 100);
    GraphicsUtils.PutPixelAll(this, trapesium2, GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);
    // trapesium bawah
    List<Vector2> trapesium3 = _bentukDasar.TrapesiumSamaKaki(new(titikAwal.X + 200 - 40 + 40, titikAwal.Y + 100), 200, 120, 100);
    GraphicsUtils.PutPixelAll(this, trapesium3, GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

    // jajargenjang atas
    List<Vector2> jajarGenjang4 = _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40, titikAwal.Y - 100 - 100), 120, 100, 40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang4, GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang5 = _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40 + 40, titikAwal.Y - 100 - 100 - 100), 120, 100, 40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang5, GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // jajargenjang bawah
    List<Vector2> jajarGenjang6 = _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40 + 40, titikAwal.Y + 100 + 100), 120, 100, -40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang6, GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang7 = _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40 + 40 + 40, titikAwal.Y + 100 + 100 + 100), 120, 100, -40);
    GraphicsUtils.PutPixelAll(this, jajarGenjang7, GraphicsUtils.DrawStyle.DotDot, Colors.Green);
  }


  private void MarginPixel()
  {
    var margin = _bentukDasar.Margin();
    GraphicsUtils.PutPixelAll(this, margin, color: Colors.White);
  }


}
