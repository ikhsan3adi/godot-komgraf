using System;

namespace Godot;

public partial class Karya5 : Node2D
{
  private BentukDasar _bentukDasar = new BentukDasar();

  public override void _Ready()
  {
    ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
    QueueRedraw();
  }

  public override void _Draw()
  {
    MarginPixel();
    MyPersegi();
    MySegitiga();
    MyTrapesium();
    MyLingkaran();
    MyElips();
  }

  private void MyPersegi()
  {
    // Persegi
    Vector2 titikAwalPersegi2 = ScreenUtils.ConvertToPixel(-150, 100);
    var persegi2 = _bentukDasar.Persegi(titikAwalPersegi2.X, titikAwalPersegi2.Y, 50);
    GraphicsUtils.PutPixelAll(this, persegi2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4), 1, 0);

    // Persegi Panjang
    Vector2 titikAwalPersegiPanjang2 = ScreenUtils.ConvertToPixel(-270, 150);
    var persegipanjang2 = _bentukDasar.PersegiPanjang(titikAwalPersegiPanjang2.X, titikAwalPersegiPanjang2.Y, 70, 40);
    GraphicsUtils.PutPixelAll(this, persegipanjang2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3), 1, 0);
  }

  private void MySegitiga()
  {
    // Segitiga Siku-siku    
    Vector2 titikAwalSegitiga1 = ScreenUtils.ConvertToPixel(30, 30);
    var segitiga1 = _bentukDasar.SegitigaSiku(titikAwalSegitiga1, 60, 50);
    GraphicsUtils.PutPixelAll(this, segitiga1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1), 2, 1);

    // Segitiga Sama Kaki menggunakan method SegitigaSamaKaki
    Vector2 titikAwalSegitigaSamaKaki1 = ScreenUtils.ConvertToPixel(300, 50);
    var segitigaSamaKaki1 = _bentukDasar.SegitigaSamaKaki(titikAwalSegitigaSamaKaki1, 70, 60);
    GraphicsUtils.PutPixelAll(this, segitigaSamaKaki1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2), 2, 1);
  }

  private void MyTrapesium()
  {
    // Trapesium Siku-siku
    Vector2 titikAwalTrapesium3 = ScreenUtils.ConvertToPixel(-220, -220);
    var trapesium3 = _bentukDasar.TrapesiumSiku(titikAwalTrapesium3, 40, 70, 50);
    GraphicsUtils.PutPixelAll(this, trapesium3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5), 1, 0);

    // Trapesium Sama Kaki
    Vector2 titikAwalTrapesiumSamaKaki3 = ScreenUtils.ConvertToPixel(-340, -220);
    var trapesiumSamaKaki3 = _bentukDasar.TrapesiumSamaKaki(titikAwalTrapesiumSamaKaki3, 30, 60, 45);
    GraphicsUtils.PutPixelAll(this, trapesiumSamaKaki3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7), 2, 1);
  }

  private void MyLingkaran()
  {
    // kartesian to screen/pixel
    Vector2 titikTengah = ScreenUtils.ConvertToPixel(150, -150);
    var lingkaran = _bentukDasar.Lingkaran(titikTengah, 50);
    GraphicsUtils.PutPixelAll(this, lingkaran, color: Colors.Green);
  }

  private void MyElips()
  {
    // kartesian to screen/pixel
    Vector2 titikTengah = ScreenUtils.ConvertToPixel(350, -150);
    var elips = _bentukDasar.Elips(titikTengah, 80, 50);
    GraphicsUtils.PutPixelAll(this, elips, color: Colors.Purple);
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
