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
    Bunga bunga4Kelopak = new Bunga(25, 30, 40, 4, Colors.Yellow, Colors.Purple);
    Bunga bunga8Kelopak = new Bunga(25, 25, 15, 8, Colors.Pink, Colors.Cyan);

    bunga4Kelopak.Draw(this, new(-300, 150), isCartesian: true);
    bunga4Kelopak.Draw(this, new(-100, 150), isCartesian: true);
    bunga4Kelopak.Draw(this, new(100, 150), isCartesian: true);
    bunga4Kelopak.Draw(this, new(300, 150), isCartesian: true);

    bunga8Kelopak.Draw(this, new(-500, -150), isCartesian: true);
    bunga8Kelopak.Draw(this, new(-300, -150), isCartesian: true);
    bunga8Kelopak.Draw(this, new(-100, -150), isCartesian: true);
    bunga8Kelopak.Draw(this, new(100, -150), isCartesian: true);
    bunga8Kelopak.Draw(this, new(300, -150), isCartesian: true);
    bunga8Kelopak.Draw(this, new(500, -150), isCartesian: true);
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
