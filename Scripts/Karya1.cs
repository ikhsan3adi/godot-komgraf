using System;
using System.Collections.Generic;

namespace Godot;

public partial class Karya1 : Node2D
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
  private bool cartesianEnabled = true;
  private Vector2 translation = new(150, 150);
  private float rotation = 30.0f;
  private float scale = 2.0f;

  private readonly List<GambarBentuk> gambarBentuk = [];

  [Export]
  private Label _label;

  [Export]
  private CheckButton _frameToggleCheckBtn;

  [Export]
  private CheckButton _cartesianToggleCheckBtn;

  [Export]
  private Slider _translateXSlider;

  [Export]
  private Slider _translateYSlider;

  [Export]
  private Slider _rotateSlider;

  [Export]
  private Slider _scaleSlider;

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
    _cartesianToggleCheckBtn.ButtonPressed = cartesianEnabled;
    _translateXSlider.Value = translation.X;
    _translateYSlider.Value = translation.Y;
    _rotateSlider.Value = rotation;
    _scaleSlider.Value = scale;

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

  public void _on_margin_button_toggled(bool enabled)
  {
    marginEnabled = enabled;
    QueueRedraw();
  }
  public void _on_cartesian_button_toggled(bool enabled)
  {
    cartesianEnabled = enabled;
    QueueRedraw();
  }
  public void _on_translate_x_changed(float value)
  {
    translation.X = value;
    QueueRedraw();
  }
  public void _on_translate_y_changed(float value)
  {
    translation.Y = value;
    QueueRedraw();
  }
  public void _on_rotate_changed(float value)
  {
    rotation = value;
    QueueRedraw();
  }
  public void _on_scale_changed(float value)
  {
    scale = value;
    QueueRedraw();
  }

  public override void _Draw()
  {
    if (marginEnabled) MarginPixel();
    if (cartesianEnabled) CartesianLine();

    _label.Text = gambarBentuk[idx].name;

    gambarBentuk[idx].draw();
  }

  private void MarginPixel()
  {
    var margin = _bentukDasar.Margin();
    GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
  }

  private void CartesianLine()
  {
    var xAxis = _primitif.LineBresenham(
      ScreenUtils.MarginLeft,
      ScreenUtils.ScreenHeight / 2,
      ScreenUtils.MarginRight,
      ScreenUtils.ScreenHeight / 2
    );
    var yAxis = _primitif.LineBresenham(
      ScreenUtils.ScreenWidth / 2,
      ScreenUtils.MarginTop,
      ScreenUtils.ScreenWidth / 2,
      ScreenUtils.MarginBottom
    );
    GraphicsUtils.PutPixelAll(this, xAxis, color: ColorUtils.ColorStorage(0));
    GraphicsUtils.PutPixelAll(this, yAxis, color: ColorUtils.ColorStorage(0));
  }

  private void GambarGaris()
  {
    // kuadran 1 (garis-garis)

    Vector2 titikAwal = new(0, 0); // kartesian
    Vector2 titikAkhir = new(100, 0); // kartesian

    float[,] matrixA = new float[3, 3] {
      { titikAwal.X, 0, 0 },
      { titikAwal.Y, 1, 0 },
      { 1, 0, 1 },
    };

    float[,] matrixB = new float[3, 3] {
      { titikAkhir.X, 0, 0 },
      { titikAkhir.Y, 1, 0 },
      { 1, 0, 1 },
    };

    // translate
    _transformasi.Translation(matrixA, translation.X, translation.Y, ref titikAwal);
    _transformasi.Translation(matrixB, translation.X, translation.Y, ref titikAkhir);

    Vector2 titikTengah = new((titikAwal.X + titikAkhir.X) / 2, (titikAwal.Y + titikAkhir.Y) / 2);

    // rotasi clockwise (pivot titikTengah)
    _transformasi.RotationClockwise(matrixA, rotation, titikTengah);
    _transformasi.RotationClockwise(matrixB, rotation, titikTengah);
    titikAwal.X = matrixA[0, 0];
    titikAwal.Y = matrixA[1, 0];
    titikAkhir.X = matrixB[0, 0];
    titikAkhir.Y = matrixB[1, 0];

    // scale (pivot titikTengah)
    _transformasi.Scaling(matrixA, scale, scale, titikTengah);
    _transformasi.Scaling(matrixB, scale, scale, titikTengah);
    titikAwal.X = matrixA[0, 0];
    titikAwal.Y = matrixA[1, 0];
    titikAkhir.X = matrixB[0, 0];
    titikAkhir.Y = matrixB[1, 0];

    var garis1 = _primitif.LineBresenham(titikAwal.X, titikAwal.Y, titikAkhir.X, titikAkhir.Y);
    GraphicsUtils.PutPixelAllCartesian(this, garis1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    // refleksi y kuadran 1
    _transformasi.ReflectionToY(matrixA, ref titikAwal);
    _transformasi.ReflectionToY(matrixB, ref titikAkhir);

    var garis2 = _primitif.LineBresenham(titikAwal.X, titikAwal.Y, titikAkhir.X, titikAkhir.Y);
    GraphicsUtils.PutPixelAllCartesian(this, garis2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    // refleksi x kuadran 2
    _transformasi.ReflectionToX(matrixA, ref titikAwal);
    _transformasi.ReflectionToX(matrixB, ref titikAkhir);

    var garis3 = _primitif.LineBresenham(titikAwal.X, titikAwal.Y, titikAkhir.X, titikAkhir.Y);
    GraphicsUtils.PutPixelAllCartesian(this, garis3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    // refleksi y kuadran 3
    _transformasi.ReflectionToY(matrixA, ref titikAwal);
    _transformasi.ReflectionToY(matrixB, ref titikAkhir);

    var garis4 = _primitif.LineBresenham(titikAwal.X, titikAwal.Y, titikAkhir.X, titikAkhir.Y);
    GraphicsUtils.PutPixelAllCartesian(this, garis4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarPersegi()
  {
    // kuadran 1 (garis-garis)

    // kartesian
    Vector2 titikAwal = new(0, 0);
    float panjang = 100;

    float[,] matrix = new float[3, 3] {
      { 1, 0, 0 },
      { 0, 1, 0 },
      { 0, 0, 1 },
    };

    var basePersegi = _bentukDasar.Persegi(titikAwal.X, titikAwal.Y, panjang);

    // translate
    _transformasi.Translation(matrix, translation.X, translation.Y, ref titikAwal);

    Vector2 titikTengah = new(titikAwal.X + panjang / 2, titikAwal.Y + panjang / 2);

    // rotate (pivot titikTengah / pusatPersegi)
    _transformasi.RotationClockwise(matrix, rotation, titikTengah);

    // scale (pivot titikTengah / pusatPersegi)
    _transformasi.Scaling(matrix, scale, scale, titikTengah);

    var persegi1 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var persegi2 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    _transformasi.ReflectionToX(matrix, ref titikTengah);
    var persegi3 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var persegi4 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarPersegiPanjang()
  {
    // kuadran 1 (garis-garis)

    // kartesian
    Vector2 titikAwal = new(0, 0);
    float panjang = 200;
    float lebar = 100;

    float[,] matrix = new float[3, 3] {
      { 1, 0, 0 },
      { 0, 1, 0 },
      { 0, 0, 1 },
    };

    var basePersegi = _bentukDasar.PersegiPanjang(titikAwal.X, titikAwal.Y, panjang, lebar);

    // translate
    _transformasi.Translation(matrix, translation.X, translation.Y, ref titikAwal);

    Vector2 titikTengah = new(titikAwal.X + panjang / 2, titikAwal.Y + lebar / 2);

    // rotate (pivot titikTengah / pusatPersegi)
    _transformasi.RotationClockwise(matrix, rotation, titikTengah);

    // scale (pivot titikTengah / pusatPersegi)
    _transformasi.Scaling(matrix, scale, scale, titikTengah);

    var persegi1 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var persegi2 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    _transformasi.ReflectionToX(matrix, ref titikTengah);
    var persegi3 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var persegi4 = _transformasi.GetTransformPoint(matrix, basePersegi);
    GraphicsUtils.PutPixelAllCartesian(this, persegi4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarSegitigaSikuSiku()
  {
    // kuadran 1 (garis-garis)

    // kartesian
    Vector2 titikAwal = new(0, 0);
    float alas = 100;
    float tinggi = 150;

    float[,] matrix = new float[3, 3] {
      { 1, 0, 0 },
      { 0, 1, 0 },
      { 0, 0, 1 },
    };

    var baseSegitiga = _bentukDasar.SegitigaSiku(titikAwal, (int)alas, (int)tinggi);

    // translate
    _transformasi.Translation(matrix, translation.X, translation.Y, ref titikAwal);

    Vector2 titikTengah = new(titikAwal.X + alas / 2, titikAwal.Y + tinggi / 2);

    // rotate (pivot titikTengah)
    _transformasi.RotationClockwise(matrix, rotation, titikTengah);

    // scale (pivot titikTengah)
    _transformasi.Scaling(matrix, scale, scale, titikTengah);

    var segitiga1 = _transformasi.GetTransformPoint(matrix, baseSegitiga);
    GraphicsUtils.PutPixelAllCartesian(this, segitiga1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var segitiga2 = _transformasi.GetTransformPoint(matrix, baseSegitiga);
    GraphicsUtils.PutPixelAllCartesian(this, segitiga2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    _transformasi.ReflectionToX(matrix, ref titikTengah);
    var segitiga3 = _transformasi.GetTransformPoint(matrix, baseSegitiga);
    GraphicsUtils.PutPixelAllCartesian(this, segitiga3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var segitiga4 = _transformasi.GetTransformPoint(matrix, baseSegitiga);
    GraphicsUtils.PutPixelAllCartesian(this, segitiga4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarTrapesiumSikuSiku()
  {
    // kuadran 1 (garis-garis)

    // kartesian
    Vector2 titikAwal = new(0, 0);
    float panjangAtas = 150;
    float panjangBawah = 200;
    float tinggi = 100;

    float[,] matrix = new float[3, 3] {
      { 1, 0, 0 },
      { 0, 1, 0 },
      { 0, 0, 1 },
    };

    var baseTrapesium = _bentukDasar.TrapesiumSiku(titikAwal, (int)panjangAtas, (int)panjangBawah, (int)tinggi);

    // translate
    _transformasi.Translation(matrix, translation.X, translation.Y, ref titikAwal);

    Vector2 titikTengah = new(titikAwal.X + panjangBawah / 2, titikAwal.Y + tinggi / 2);

    // rotate (pivot titikTengah)
    _transformasi.RotationClockwise(matrix, rotation, titikTengah);

    // scale (pivot titikTengah)
    _transformasi.Scaling(matrix, scale, scale, titikTengah);

    var trapesium1 = _transformasi.GetTransformPoint(matrix, baseTrapesium);
    GraphicsUtils.PutPixelAllCartesian(this, trapesium1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

    // Kuadran 2 (garis normal)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var trapesium2 = _transformasi.GetTransformPoint(matrix, baseTrapesium);
    GraphicsUtils.PutPixelAllCartesian(this, trapesium2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    _transformasi.ReflectionToX(matrix, ref titikTengah);
    var trapesium3 = _transformasi.GetTransformPoint(matrix, baseTrapesium);
    GraphicsUtils.PutPixelAllCartesian(this, trapesium3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    _transformasi.ReflectionToY(matrix, ref titikTengah);
    var trapesium4 = _transformasi.GetTransformPoint(matrix, baseTrapesium);
    GraphicsUtils.PutPixelAllCartesian(this, trapesium4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
  }

  private void GambarLingkaran()
  {
    // kuadran 1 (garis-garis)

    Vector2 titikPusat = new(0, 0);
    float radius = 100;

    float[,] matrix = new float[3, 3] {
      { 1, 0, 0 },
      { 0, 1, 0 },
      { 0, 0, 1 },
    };

    var baseLingkaran = _bentukDasar.Lingkaran(titikPusat, (int)radius);

    // translate
    _transformasi.Translation(matrix, translation.X, translation.Y, ref titikPusat);

    // rotate (pivot titikPusat)
    _transformasi.RotationClockwise(matrix, rotation, titikPusat);

    // scale (pivot titikPusat)
    _transformasi.Scaling(matrix, scale, scale, titikPusat);

    var lingkaran1 = _transformasi.GetTransformPoint(matrix, baseLingkaran);
    GraphicsUtils.PutPixelAllCartesian(this, lingkaran1, GraphicsUtils.DrawStyle.CircleDotStrip, Colors.Red, stripLength: 5, gap: 5);

    // Kuadran 2 (garis normal)
    _transformasi.ReflectionToY(matrix, ref titikPusat);
    var lingkaran2 = _transformasi.GetTransformPoint(matrix, baseLingkaran);
    GraphicsUtils.PutPixelAllCartesian(this, lingkaran2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    _transformasi.ReflectionToX(matrix, ref titikPusat);
    var lingkaran3 = _transformasi.GetTransformPoint(matrix, baseLingkaran);
    GraphicsUtils.PutPixelAllCartesian(this, lingkaran3, GraphicsUtils.DrawStyle.CircleStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    _transformasi.ReflectionToY(matrix, ref titikPusat);
    var lingkaran4 = _transformasi.GetTransformPoint(matrix, baseLingkaran);
    GraphicsUtils.PutPixelAllCartesian(this, lingkaran4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 20, gap: 5);
  }

  private void GambarElips()
  {
    // kuadran 1 (garis-garis)

    Vector2 titikPusat = new(0, 0);
    float radiusX = 100;
    float radiusY = 50;

    float[,] matrix = new float[3, 3] {
      { 1, 0, 0 },
      { 0, 1, 0 },
      { 0, 0, 1 },
    };

    var baseElips = _bentukDasar.Elips(titikPusat, (int)radiusX, (int)radiusY);

    // translate
    _transformasi.Translation(matrix, translation.X, translation.Y, ref titikPusat);

    // rotate (pivot titikPusat)
    _transformasi.RotationClockwise(matrix, rotation, titikPusat);

    // scale (pivot titikPusat)
    _transformasi.Scaling(matrix, scale, scale, titikPusat);

    var elips1 = _transformasi.GetTransformPoint(matrix, baseElips);
    GraphicsUtils.PutPixelAllCartesian(this, elips1, GraphicsUtils.DrawStyle.CircleDotStrip, Colors.Red, stripLength: 5, gap: 5);

    // Kuadran 2 (garis normal)
    _transformasi.ReflectionToY(matrix, ref titikPusat);
    var elips2 = _transformasi.GetTransformPoint(matrix, baseElips);
    GraphicsUtils.PutPixelAllCartesian(this, elips2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

    // Kuadran 3 (titik titik)
    _transformasi.ReflectionToX(matrix, ref titikPusat);
    var elips3 = _transformasi.GetTransformPoint(matrix, baseElips);
    GraphicsUtils.PutPixelAllCartesian(this, elips3, GraphicsUtils.DrawStyle.CircleStrip, Colors.Green, stripLength: 1, gap: 3);

    // Kuadran 4 (titik garis titik)
    _transformasi.ReflectionToY(matrix, ref titikPusat);
    var elips4 = _transformasi.GetTransformPoint(matrix, baseElips);
    GraphicsUtils.PutPixelAllCartesian(this, elips4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 20, gap: 5);
  }

  public override void _ExitTree()
  {
    NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
    NodeUtils.DisposeAndNull(_primitif, "_primitif");
    NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
    base._ExitTree();
  }
}
