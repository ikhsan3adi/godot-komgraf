using System;
using System.Collections.Generic;

namespace Godot;

public partial class Karya6 : Node2D
{
  private BentukDasar _bentukDasar = new BentukDasar();
  private Primitif _primitif = new Primitif();
  private Transformasi _transformasi = new Transformasi();

  struct GambarBentuk(string name, Action draw)
  {
    public string name = name;
    public Action draw = draw;
  }

  private int idx = 0;
  private bool marginEnabled = true;

  private readonly List<GambarBentuk> gambarBentuk = [];

  [Export]
  private Label _label;

  [Export]
  private CheckButton _frameToggleCheckBtn;

  public override void _Ready()
  {
    ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils

    gambarBentuk.Add(new GambarBentuk("Garis", GambarGaris));
    gambarBentuk.Add(new GambarBentuk("Persegi", GambarPersegi));
    gambarBentuk.Add(new GambarBentuk("Persegi Panjang", GambarPersegiPanjang));
    gambarBentuk.Add(new GambarBentuk("Segitiga Siku-siku", GambarSegitigaSikuSiku));
    gambarBentuk.Add(new GambarBentuk("Trapesium Siku-siku", GambarTrapesiumSikuSiku));
    gambarBentuk.Add(new GambarBentuk("Lingkaran", GambarLingkaran));
    gambarBentuk.Add(new GambarBentuk("Elips", GambarElips));

    _frameToggleCheckBtn.ButtonPressed = marginEnabled;

    QueueRedraw();
  }

  public void _on_prev_btn_pressed()
  {
    if (idx == 0)
    {
      idx = gambarBentuk.Count - 1;
    }
    else
    {
      idx--;
    }
    QueueRedraw();
  }

  public void _on_next_btn_pressed()
  {
    idx++;
    idx %= gambarBentuk.Count;
    QueueRedraw();
  }

  public void _on_check_button_toggled(bool enabled)
  {
    marginEnabled = enabled;
    QueueRedraw();
  }

  public override void _Draw()
  {
    if (marginEnabled) MarginPixel();

    _label.Text = gambarBentuk[idx].name;

    gambarBentuk[idx].draw();
  }

  private void MarginPixel()
  {
    var margin = _bentukDasar.Margin();
    GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
  }

  private void GambarGaris()
  {
    // kuadran 1 (garis-garis)

    Vector2 titikAwal = new(0, 0); // kartesian
    Vector2 titikAkhir = new(100, 0); // kartesian

    float[,] matrixA = new float[3, 3] {
      { titikAwal.X, 0, 0 },
      { titikAwal.Y, 0, 0 },
      { 1, 0, 0 },
    };

    float[,] matrixB = new float[3, 3] {
      { titikAkhir.X, 0, 0 },
      { titikAkhir.Y, 0, 0 },
      { 1, 0, 0 },
    };

    // translate ke kuadran 1
    _transformasi.Translation(matrixA, 100, 250, ref titikAwal);
    _transformasi.Translation(matrixB, 200, 250, ref titikAkhir);

    // rotasi clockwise 30deg (pivot titikAwal)
    _transformasi.RotationClockwise(matrixB, 30, titikAwal);
    titikAkhir.X = matrixB[0, 0];
    titikAkhir.Y = matrixB[1, 0];

    // scale 2.0x (pivot titikAwal)
    _transformasi.Scaling(matrixB, 2.0f, 2.0f, titikAwal);
    titikAkhir.X = matrixB[0, 0];
    titikAkhir.Y = matrixB[1, 0];

    Vector2 titikAwalPixel = ScreenUtils.ConvertToPixel(titikAwal.X, titikAwal.Y); // screen
    Vector2 titikAkhirPixel = ScreenUtils.ConvertToPixel(titikAkhir.X, titikAkhir.Y); // screen

    var garis1 = _primitif.LineBresenham(titikAwalPixel.X, titikAwalPixel.Y, titikAkhirPixel.X, titikAkhirPixel.Y);
    GraphicsUtils.PutPixelAll(this, garis1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    // refleksi y kuadran 1
    _transformasi.ReflectionToY(matrixA, ref titikAwal);
    _transformasi.ReflectionToY(matrixB, ref titikAkhir);

    Vector2 titikAwalK2 = titikAwal;
    Vector2 titikAkhirK2 = titikAkhir;

    titikAwalPixel = ScreenUtils.ConvertToPixel(titikAwal.X, titikAwal.Y);
    titikAkhirPixel = ScreenUtils.ConvertToPixel(titikAkhir.X, titikAkhir.Y);

    var garis2 = _primitif.LineBresenham(titikAwalPixel.X, titikAwalPixel.Y, titikAkhirPixel.X, titikAkhirPixel.Y);
    GraphicsUtils.PutPixelAll(this, garis2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    // refleksi x kuadran 2
    _transformasi.ReflectionToX(matrixA, ref titikAwal);
    _transformasi.ReflectionToX(matrixB, ref titikAkhir);

    titikAwalPixel = ScreenUtils.ConvertToPixel(titikAwal.X, titikAwal.Y);
    titikAkhirPixel = ScreenUtils.ConvertToPixel(titikAkhir.X, titikAkhir.Y);

    var garis3 = _primitif.LineBresenham(titikAwalPixel.X, titikAwalPixel.Y, titikAkhirPixel.X, titikAkhirPixel.Y);
    GraphicsUtils.PutPixelAll(this, garis3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    // refleksi origin (0, 0) kuadran 2
    // Reset matrix
    _transformasi.ReflectionToY(matrixA, ref titikAwalK2); // to K1
    _transformasi.ReflectionToY(matrixB, ref titikAkhirK2);
    _transformasi.ReflectionToY(matrixA, ref titikAwalK2); // to K2
    _transformasi.ReflectionToY(matrixB, ref titikAkhirK2);

    _transformasi.ReflectionToOrigin(matrixA, ref titikAwalK2);
    _transformasi.ReflectionToOrigin(matrixB, ref titikAkhirK2);

    titikAwalPixel = ScreenUtils.ConvertToPixel(titikAwalK2.X, titikAwalK2.Y);
    titikAkhirPixel = ScreenUtils.ConvertToPixel(titikAkhirK2.X, titikAkhirK2.Y);

    var garis4 = _primitif.LineBresenham(titikAwalPixel.X, titikAwalPixel.Y, titikAkhirPixel.X, titikAkhirPixel.Y);
    GraphicsUtils.PutPixelAll(this, garis4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarPersegi()
  {
    // kuadran 1 (garis-garis)
    Vector2 taw1 = ScreenUtils.ConvertToPixel(200, 200);
    var persegi1 = _bentukDasar.Persegi(taw1.X, taw1.Y, 100);
    GraphicsUtils.PutPixelAll(this, persegi1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    Vector2 taw2 = ScreenUtils.ConvertToPixel(-300, 200);
    var persegi2 = _bentukDasar.Persegi(taw2.X, taw2.Y, 100);
    GraphicsUtils.PutPixelAll(this, persegi2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    Vector2 taw3 = ScreenUtils.ConvertToPixel(-300, -100);
    var persegi3 = _bentukDasar.Persegi(taw3.X, taw3.Y, 100);
    GraphicsUtils.PutPixelAll(this, persegi3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    Vector2 taw4 = ScreenUtils.ConvertToPixel(200, -100);
    var persegi4 = _bentukDasar.Persegi(taw4.X, taw4.Y, 100);
    GraphicsUtils.PutPixelAll(this, persegi4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarPersegiPanjang()
  {
    // kuadran 1 (garis-garis)
    Vector2 taw1 = ScreenUtils.ConvertToPixel(200, 200);
    var persegiPanjang1 = _bentukDasar.PersegiPanjang(taw1.X, taw1.Y, 200, 100);
    GraphicsUtils.PutPixelAll(this, persegiPanjang1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    Vector2 taw2 = ScreenUtils.ConvertToPixel(-400, 200);
    var persegiPanjang2 = _bentukDasar.PersegiPanjang(taw2.X, taw2.Y, 200, 100);
    GraphicsUtils.PutPixelAll(this, persegiPanjang2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    Vector2 taw3 = ScreenUtils.ConvertToPixel(-400, -100);
    var persegiPanjang3 = _bentukDasar.PersegiPanjang(taw3.X, taw3.Y, 200, 100);
    GraphicsUtils.PutPixelAll(this, persegiPanjang3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    Vector2 taw4 = ScreenUtils.ConvertToPixel(200, -100);
    var persegiPanjang4 = _bentukDasar.PersegiPanjang(taw4.X, taw4.Y, 200, 100);
    GraphicsUtils.PutPixelAll(this, persegiPanjang4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarSegitigaSikuSiku()
  {
    // kuadran 1 (garis-garis)
    Vector2 taw1 = ScreenUtils.ConvertToPixel(200, 100);
    var segitiga1 = _bentukDasar.SegitigaSiku(taw1, 200, 100);
    GraphicsUtils.PutPixelAll(this, segitiga1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    Vector2 taw2 = ScreenUtils.ConvertToPixel(-400, 100);
    var segitiga2 = _bentukDasar.SegitigaSiku(taw2, 200, 100);
    GraphicsUtils.PutPixelAll(this, segitiga2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    Vector2 taw3 = ScreenUtils.ConvertToPixel(-400, -200);
    var segitiga3 = _bentukDasar.SegitigaSiku(taw3, 200, 100);
    GraphicsUtils.PutPixelAll(this, segitiga3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    Vector2 taw4 = ScreenUtils.ConvertToPixel(200, -200);
    var segitiga4 = _bentukDasar.SegitigaSiku(taw4, 200, 100);
    GraphicsUtils.PutPixelAll(this, segitiga4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarTrapesiumSikuSiku()
  {
    // kuadran 1 (garis-garis)
    Vector2 taw1 = ScreenUtils.ConvertToPixel(200, 100);
    var trapesium1 = _bentukDasar.TrapesiumSiku(taw1, 150, 200, 100);
    GraphicsUtils.PutPixelAll(this, trapesium1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    Vector2 taw2 = ScreenUtils.ConvertToPixel(-400, 100);
    var trapesium2 = _bentukDasar.TrapesiumSiku(taw2, 150, 200, 100);
    GraphicsUtils.PutPixelAll(this, trapesium2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    Vector2 taw3 = ScreenUtils.ConvertToPixel(-400, -200);
    var trapesium3 = _bentukDasar.TrapesiumSiku(taw3, 200, 150, 100);
    GraphicsUtils.PutPixelAll(this, trapesium3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    Vector2 taw4 = ScreenUtils.ConvertToPixel(200, -200);
    var trapesium4 = _bentukDasar.TrapesiumSiku(taw4, 200, 150, 100);
    GraphicsUtils.PutPixelAll(this, trapesium4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarLingkaran()
  {
    // kuadran 1 (garis-garis)
    Vector2 tt1 = ScreenUtils.ConvertToPixel(200, 150);
    var lingkaran1 = _bentukDasar.Lingkaran(tt1, 100);
    GraphicsUtils.PutPixelAll(this, lingkaran1, GraphicsUtils.DrawStyle.CircleDotStrip, Colors.Red, stripLength: 5, gap: 5);

    // Kuadran 2 (garis normal)
    Vector2 tt2 = ScreenUtils.ConvertToPixel(-200, 150);
    var lingkaran2 = _bentukDasar.Lingkaran(tt2, 100);
    GraphicsUtils.PutPixelAll(this, lingkaran2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    Vector2 tt3 = ScreenUtils.ConvertToPixel(-200, -150);
    var lingkaran3 = _bentukDasar.Lingkaran(tt3, 100);
    GraphicsUtils.PutPixelAll(this, lingkaran3, GraphicsUtils.DrawStyle.CircleStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    Vector2 tt4 = ScreenUtils.ConvertToPixel(200, -150);
    var lingkaran4 = _bentukDasar.Lingkaran(tt4, 100);
    GraphicsUtils.PutPixelAll(this, lingkaran4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 20, gap: 5);
  }

  private void GambarElips()
  {
    // kuadran 1 (garis-garis)
    Vector2 tt1 = ScreenUtils.ConvertToPixel(200, 150);
    var elips1 = _bentukDasar.Elips(tt1, 100, 50);
    GraphicsUtils.PutPixelAll(this, elips1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 2);

    // Kuadran 2 (garis normal)
    Vector2 tt2 = ScreenUtils.ConvertToPixel(-200, 150);
    var elips2 = _bentukDasar.Elips(tt2, 100, 50);
    GraphicsUtils.PutPixelAll(this, elips2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    Vector2 tt3 = ScreenUtils.ConvertToPixel(-200, -150);
    var elips3 = _bentukDasar.Elips(tt3, 100, 50);
    GraphicsUtils.PutPixelAll(this, elips3, GraphicsUtils.DrawStyle.EllipseStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    Vector2 tt4 = ScreenUtils.ConvertToPixel(200, -150);
    var elips4 = _bentukDasar.Elips(tt4, 100, 50);
    GraphicsUtils.PutPixelAll(this, elips4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  public override void _ExitTree()
  {
    NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
    base._ExitTree();
  }
}
