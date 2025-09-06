namespace Godot;

public partial class Karya4 : Node2D
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
  }

  private void MyPersegi()
  {
    // Persegi
    // Kuadran 1 
    Vector2 titikAwalPersegi1 = ScreenUtils.ConvertToPixel(100, 100, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegi1 = _bentukDasar.Persegi(titikAwalPersegi1.X, titikAwalPersegi1.Y, 50);
    GraphicsUtils.PutPixelAll(this, persegi1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4), 1, 0);

    // Kuadran 2 
    Vector2 titikAwalPersegi2 = ScreenUtils.ConvertToPixel(-150, 100, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegi2 = _bentukDasar.Persegi(titikAwalPersegi2.X, titikAwalPersegi2.Y, 50);
    GraphicsUtils.PutPixelAll(this, persegi2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4), 1, 0);

    // Kuadran 3 
    Vector2 titikAwalPersegi3 = ScreenUtils.ConvertToPixel(-150, -150, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegi3 = _bentukDasar.Persegi(titikAwalPersegi3.X, titikAwalPersegi3.Y, 50);
    GraphicsUtils.PutPixelAll(this, persegi3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4), 1, 0);

    // Kuadran 4 
    Vector2 titikAwalPersegi4 = ScreenUtils.ConvertToPixel(100, -150, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegi4 = _bentukDasar.Persegi(titikAwalPersegi4.X, titikAwalPersegi4.Y, 50);
    GraphicsUtils.PutPixelAll(this, persegi4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4), 1, 0);

    // Persegi Panjang
    // Kuadran 1 
    Vector2 titikAwalPersegiPanjang1 = ScreenUtils.ConvertToPixel(200, 150, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegipanjang1 = _bentukDasar.PersegiPanjang(titikAwalPersegiPanjang1.X, titikAwalPersegiPanjang1.Y, 70, 40);
    GraphicsUtils.PutPixelAll(this, persegipanjang1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3), 1, 0);

    // Kuadran 2 
    Vector2 titikAwalPersegiPanjang2 = ScreenUtils.ConvertToPixel(-270, 150, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegipanjang2 = _bentukDasar.PersegiPanjang(titikAwalPersegiPanjang2.X, titikAwalPersegiPanjang2.Y, 70, 40);
    GraphicsUtils.PutPixelAll(this, persegipanjang2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3), 1, 0);

    // Kuadran 3 
    Vector2 titikAwalPersegiPanjang3 = ScreenUtils.ConvertToPixel(-270, -190, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegipanjang3 = _bentukDasar.PersegiPanjang(titikAwalPersegiPanjang3.X, titikAwalPersegiPanjang3.Y, 70, 40);
    GraphicsUtils.PutPixelAll(this, persegipanjang3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3), 1, 0);

    // Kuadran 4 
    Vector2 titikAwalPersegiPanjang4 = ScreenUtils.ConvertToPixel(200, -190, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var persegipanjang4 = _bentukDasar.PersegiPanjang(titikAwalPersegiPanjang4.X, titikAwalPersegiPanjang4.Y, 70, 40);
    GraphicsUtils.PutPixelAll(this, persegipanjang4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3), 1, 0);
  }

  private void MySegitiga()
  {
    // Segitiga Siku-siku
    // Kuadran 1 
    Vector2 titikAwalSegitiga1 = ScreenUtils.ConvertToPixel(30, 30, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitiga1 = _bentukDasar.SegitigaSiku(titikAwalSegitiga1, 60, 50);
    GraphicsUtils.PutPixelAll(this, segitiga1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1), 2, 1);

    // Kuadran 2 
    Vector2 titikAwalSegitiga2 = ScreenUtils.ConvertToPixel(-90, 30, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitiga2 = _bentukDasar.SegitigaSiku(titikAwalSegitiga2, 60, 50);
    GraphicsUtils.PutPixelAll(this, segitiga2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1), 2, 1);

    // Kuadran 3 
    Vector2 titikAwalSegitiga3 = ScreenUtils.ConvertToPixel(-90, -80, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitiga3 = _bentukDasar.SegitigaSiku(titikAwalSegitiga3, 60, 50);
    GraphicsUtils.PutPixelAll(this, segitiga3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1), 2, 1);

    // Kuadran 4 
    Vector2 titikAwalSegitiga4 = ScreenUtils.ConvertToPixel(30, -80, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitiga4 = _bentukDasar.SegitigaSiku(titikAwalSegitiga4, 60, 50);
    GraphicsUtils.PutPixelAll(this, segitiga4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1), 2, 1);

    // Segitiga Sama Kaki menggunakan method SegitigaSamaKaki
    // Kuadran 1
    Vector2 titikAwalSegitigaSamaKaki1 = ScreenUtils.ConvertToPixel(300, 50, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitigaSamaKaki1 = _bentukDasar.SegitigaSamaKaki(titikAwalSegitigaSamaKaki1, 70, 60);
    GraphicsUtils.PutPixelAll(this, segitigaSamaKaki1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2), 2, 1);

    // Kuadran 2 - Segitiga Sama Kaki
    Vector2 titikAwalSegitigaSamaKaki2 = ScreenUtils.ConvertToPixel(-370, 50, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitigaSamaKaki2 = _bentukDasar.SegitigaSamaKaki(titikAwalSegitigaSamaKaki2, 70, 60);
    GraphicsUtils.PutPixelAll(this, segitigaSamaKaki2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2), 2, 1);

    // Kuadran 3 - Segitiga Sama Kaki
    Vector2 titikAwalSegitigaSamaKaki3 = ScreenUtils.ConvertToPixel(-370, -110, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitigaSamaKaki3 = _bentukDasar.SegitigaSamaKaki(titikAwalSegitigaSamaKaki3, 70, 60);
    GraphicsUtils.PutPixelAll(this, segitigaSamaKaki3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2), 2, 1);

    // Kuadran 4 - Segitiga Sama Kaki
    Vector2 titikAwalSegitigaSamaKaki4 = ScreenUtils.ConvertToPixel(300, -110, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var segitigaSamaKaki4 = _bentukDasar.SegitigaSamaKaki(titikAwalSegitigaSamaKaki4, 70, 60);
    GraphicsUtils.PutPixelAll(this, segitigaSamaKaki4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2), 2, 1);
  }

  private void MyTrapesium()
  {
    // Trapesium Siku-siku
    // Kuadran 1 
    Vector2 titikAwalTrapesium1 = ScreenUtils.ConvertToPixel(150, 200, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesium1 = _bentukDasar.TrapesiumSiku(titikAwalTrapesium1, 40, 70, 50);
    GraphicsUtils.PutPixelAll(this, trapesium1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5), 1, 0);

    // Kuadran 2 
    Vector2 titikAwalTrapesium2 = ScreenUtils.ConvertToPixel(-220, 200, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesium2 = _bentukDasar.TrapesiumSiku(titikAwalTrapesium2, 40, 70, 50);
    GraphicsUtils.PutPixelAll(this, trapesium2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5), 1, 0);

    // Kuadran 3 
    Vector2 titikAwalTrapesium3 = ScreenUtils.ConvertToPixel(-220, -290, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesium3 = _bentukDasar.TrapesiumSiku(titikAwalTrapesium3, 40, 70, 50);
    GraphicsUtils.PutPixelAll(this, trapesium3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5), 1, 0);

    // Kuadran 4 
    Vector2 titikAwalTrapesium4 = ScreenUtils.ConvertToPixel(150, -290, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesium4 = _bentukDasar.TrapesiumSiku(titikAwalTrapesium4, 40, 70, 50);
    GraphicsUtils.PutPixelAll(this, trapesium4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5), 1, 0);

    // Trapesium Sama Kaki
    // Kuadran 1 
    Vector2 titikAwalTrapesiumSamaKaki1 = ScreenUtils.ConvertToPixel(280, 200, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesiumSamaKaki1 = _bentukDasar.TrapesiumSamaKaki(titikAwalTrapesiumSamaKaki1, 30, 60, 45);
    GraphicsUtils.PutPixelAll(this, trapesiumSamaKaki1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7), 2, 1);

    // Kuadran 2 
    Vector2 titikAwalTrapesiumSamaKaki2 = ScreenUtils.ConvertToPixel(-340, 200, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesiumSamaKaki2 = _bentukDasar.TrapesiumSamaKaki(titikAwalTrapesiumSamaKaki2, 30, 60, 45);
    GraphicsUtils.PutPixelAll(this, trapesiumSamaKaki2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7), 2, 1);

    // Kuadran 3 
    Vector2 titikAwalTrapesiumSamaKaki3 = ScreenUtils.ConvertToPixel(-340, -265, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesiumSamaKaki3 = _bentukDasar.TrapesiumSamaKaki(titikAwalTrapesiumSamaKaki3, 30, 60, 45);
    GraphicsUtils.PutPixelAll(this, trapesiumSamaKaki3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7), 2, 1);

    // Kuadran 4 
    Vector2 titikAwalTrapesiumSamaKaki4 = ScreenUtils.ConvertToPixel(280, -265, ScreenUtils.MarginLeft, ScreenUtils.MarginTop);
    var trapesiumSamaKaki4 = _bentukDasar.TrapesiumSamaKaki(titikAwalTrapesiumSamaKaki4, 30, 60, 45);
    GraphicsUtils.PutPixelAll(this, trapesiumSamaKaki4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7), 2, 1);
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
