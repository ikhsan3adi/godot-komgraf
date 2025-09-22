namespace Godot;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class Karya3 : Node2D
{
  private BentukDasar _bentukDasar = new BentukDasar();
  private TransformasiFast _transformasi = new TransformasiFast();

  struct GambarBentuk(string name, Action draw)
  {
    public string name = name;
    public Action draw = draw;
  }

  private int idx = 0;

  private readonly List<GambarBentuk> gambarBentuk = [];

  [Export]
  private Label _label;

  public override void _Ready()
  {
    ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils

    gambarBentuk.Add(new GambarBentuk("Candy", DrawCandy));
    gambarBentuk.Add(new GambarBentuk("Penguin", DrawPenguin));
    gambarBentuk.Add(new GambarBentuk("Plane", DrawPlane));

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

  public override void _Draw()
  {
    _label.Text = gambarBentuk[idx].name;

    gambarBentuk[idx].draw();
  }

  private void DrawCandy()
  {
    Vector2 pivot = new(0, 0);
    Matrix4x4 matrix = Matrix4x4.Identity;

    float angleRad = (float)(30 * Math.PI / 180);

    _transformasi.RotationClockwise(ref matrix, angleRad, pivot);

    // segitiga kiri
    List<Vector2> segitiga1 = _transformasi.GetTransformPoint(
      matrix,
      _bentukDasar.Polygon([
        new(-350, 67),
        new(-350, -67),
        new(-250, 0),
      ])
    );

    GraphicsUtils.PutPixelAll(
      this,
      segitiga1.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Orange
    );

    // diamond hijau
    List<Vector2> belahKetupat = _transformasi.GetTransformPoint(
      matrix,
      _bentukDasar.Polygon([
        new(-250, 0),
        new(-200, 100),
        new(-150, 0),
        new(-200, -100),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      belahKetupat.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Green
    );

    // segitiga atas
    List<Vector2> segitiga2 = _transformasi.GetTransformPoint(
      matrix,
      _bentukDasar.Polygon([
        new(-200, 100),
        new(-150, 0),
        new(-100, 100),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      segitiga2.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Orange
    );

    // segitiga bawah
    List<Vector2> segitiga3 = _transformasi.GetTransformPoint(
      matrix,
      _bentukDasar.Polygon([
        new(-200, -100),
        new(-150, 0),
        new(-100, -100),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      segitiga3.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Orange
    );

    // hexagon
    List<Vector2> hexagon = _transformasi.GetTransformPoint(
      matrix,
      _bentukDasar.Polygon([
        new(-100, 100),
        new(-150, 0),
        new(-100, -100),

        new(20, -100),
        new(70, 0),
        new(20, 100),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      hexagon.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Red
    );


    // segitiga kanan
    List<Vector2> segitiga4 = _transformasi.GetTransformPoint(
      matrix,
      _bentukDasar.Polygon([
        new(170, 67),
        new(170, -67),
        new(70, 0),
      ])
    );

    GraphicsUtils.PutPixelAll(
      this,
      segitiga4.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Orange
    );
  }

  private void DrawPenguin()
  {
    Vector2 titikAwal = new(-50, 0);
    Matrix4x4 matrix = Matrix4x4.Identity;

    _transformasi.ReflectionToX(ref matrix);
    _transformasi.Translation(ref matrix, 0, 150);

    // jajar genjang atas
    List<Vector2> jajarGenjang1 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(titikAwal, 100, 70, 40));
    GraphicsUtils.PutPixelAll(
      this,
      jajarGenjang1.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Green
    );

    // hexagon 1
    List<Vector2> hexagon1 = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X, titikAwal.Y),
        new (titikAwal.X + 100, titikAwal.Y),
        new (titikAwal.X + 140, titikAwal.Y + 70),
        new (titikAwal.X + 100, titikAwal.Y + 140),
        new (titikAwal.X, titikAwal.Y + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      hexagon1.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Red
    );

    // hexagon 2
    List<Vector2> hexagon2 = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X, titikAwal.Y + 140),
        new (titikAwal.X + 100, titikAwal.Y + 140),
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
        new (titikAwal.X + 100, titikAwal.Y + 140 + 140),
        new (titikAwal.X, titikAwal.Y + 140 + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      hexagon2.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Red
    );

    // diamond 1 (kiri-atas)
    List<Vector2> diamond1 = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X, titikAwal.Y + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70),
        new (titikAwal.X - 80, titikAwal.Y + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      diamond1.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Green
    );

    // diamond 2 (kananatas)
    List<Vector2> diamond2 = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X + 140, titikAwal.Y + 70),
        new (titikAwal.X + 100, titikAwal.Y + 140),
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
        new (titikAwal.X + 180, titikAwal.Y + 140),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      diamond2.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Green
    );

    // diamond 3 (kiri-bawah)
    List<Vector2> diamond3 = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X, titikAwal.Y + 140 + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
        new (titikAwal.X - 80, titikAwal.Y + 140 + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140 + 140),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      diamond3.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Green
    );

    // diamond 4 (kanan-bawah)
    List<Vector2> diamond4 = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
        new (titikAwal.X + 100, titikAwal.Y + 140 + 140),
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140 + 140),
        new (titikAwal.X + 180, titikAwal.Y + 140 + 140),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      diamond4.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.Green
    );

    // lengan kiri
    List<Vector2> leftHand = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X - 40, titikAwal.Y + 70),
        new (titikAwal.X - 80, titikAwal.Y + 140),
        new (titikAwal.X - 150, titikAwal.Y + 140 + 50),
        new (titikAwal.X - 110, titikAwal.Y + 110),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      leftHand.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.DarkCyan
    );

    // lengan kanan
    List<Vector2> rightHand = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
        new (titikAwal.X + 100 + 40, titikAwal.Y + 70),
        new (titikAwal.X + 100 + 80, titikAwal.Y + 140),
        new (titikAwal.X + 100 + 150, titikAwal.Y + 140 + 50),
        new (titikAwal.X + 100 + 110, titikAwal.Y + 110),
      ])
    );
    GraphicsUtils.PutPixelAll(
      this,
      rightHand.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(),
      GraphicsUtils.DrawStyle.DotDot,
      Colors.DarkCyan
    );
  }

  private void DrawPlane()
  {
    Vector2 titikAwal = new(-500, -50);

    Vector2 pivot = new(0, 0);
    Matrix4x4 matrix = Matrix4x4.Identity;

    float angleRad = (float)(30 * Math.PI / 180);

    _transformasi.ReflectionToX(ref matrix);
    _transformasi.RotationClockwise(ref matrix, angleRad, pivot);
    _transformasi.Scaling(ref matrix, 0.8f, 0.8f, pivot);
    _transformasi.Translation(ref matrix, 0, -100);

    // kepala
    List<Vector2> trapesium1 = _transformasi.GetTransformPoint(matrix, _bentukDasar.TrapesiumSamaKaki(titikAwal, 120, 200, 100));
    GraphicsUtils.PutPixelAll(this, trapesium1.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

    // jajargenjang tengah
    List<Vector2> jajarGenjang1 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(new(titikAwal.X + 200, titikAwal.Y), 120, 100, -40));
    GraphicsUtils.PutPixelAll(this, jajarGenjang1.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang2 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(new(titikAwal.X + 200 + 120, titikAwal.Y), 120, 100, -40));
    GraphicsUtils.PutPixelAll(this, jajarGenjang2.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang3 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(new(titikAwal.X + 200 + 240, titikAwal.Y), 120, 100, -40));
    GraphicsUtils.PutPixelAll(this, jajarGenjang3.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // ekor
    List<Vector2> ekor = _transformasi.GetTransformPoint(matrix, _bentukDasar.Polygon([
      new(titikAwal.X + 200 + 360, titikAwal.Y),
      new(titikAwal.X + 200 + 360 - 40, titikAwal.Y - 100),
      new(titikAwal.X + 200 + 360 + 60, titikAwal.Y - 240),
      new(titikAwal.X + 200 + 360 + 170, titikAwal.Y - 240),
    ]));
    GraphicsUtils.PutPixelAll(this, ekor.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);


    // trapesium atas
    List<Vector2> trapesium2 = _transformasi.GetTransformPoint(matrix, _bentukDasar.TrapesiumSamaKaki(new(titikAwal.X + 200 - 40, titikAwal.Y - 100), 120, 200, 100));
    GraphicsUtils.PutPixelAll(this, trapesium2.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);
    // trapesium bawah
    List<Vector2> trapesium3 = _transformasi.GetTransformPoint(matrix, _bentukDasar.TrapesiumSamaKaki(new(titikAwal.X + 200 - 40 + 40, titikAwal.Y + 100), 200, 120, 100));
    GraphicsUtils.PutPixelAll(this, trapesium3.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

    // jajargenjang atas
    List<Vector2> jajarGenjang4 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40, titikAwal.Y - 100 - 100), 120, 100, 40));
    GraphicsUtils.PutPixelAll(this, jajarGenjang4.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang5 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40 + 40, titikAwal.Y - 100 - 100 - 100), 120, 100, 40));
    GraphicsUtils.PutPixelAll(this, jajarGenjang5.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

    // jajargenjang bawah
    List<Vector2> jajarGenjang6 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40 + 40, titikAwal.Y + 100 + 100), 120, 100, -40));
    GraphicsUtils.PutPixelAll(this, jajarGenjang6.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    List<Vector2> jajarGenjang7 = _transformasi.GetTransformPoint(matrix, _bentukDasar.JajarGenjang(new(titikAwal.X + 200 - 40 + 40 + 40 + 40, titikAwal.Y + 100 + 100 + 100), 120, 100, -40));
    GraphicsUtils.PutPixelAll(this, jajarGenjang7.Select(p => ScreenUtils.ConvertToPixel(p.X, p.Y)).ToList(), GraphicsUtils.DrawStyle.DotDot, Colors.Green);
  }

  public override void _ExitTree()
  {
    NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
    NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
    base._ExitTree();
  }
}
