namespace Godot;

using Godot;
using System;
public partial class Welcome : Control
{
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
  }

  private void _on_BtnKarya1_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya1.tscn");
  }

  private void _on_BtnKarya2_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya2.tscn");
  }

  private void _on_BtnKarya3_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya3.tscn");
  }

  private void _on_BtnKarya4_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya4.tscn");
  }

  private void _on_BtnKarya5_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya5.tscn");
  }

  private void _on_BtnKarya6_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya6.tscn");
  }

  private void _on_BtnKarya7_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya7.tscn");
  }

  private void _on_BtnKarya8_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya8.tscn");
  }

  private void _on_BtnKarya9_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Karya9.tscn");
  }

  private void _on_BtnAbout_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/About.tscn");
  }

  private void _on_BtnGuide_pressed()
  {
    GetTree().ChangeSceneToFile("res://Scenes/Guide.tscn");
  }

  private void _on_BtnExit_pressed()
  {
    GetTree().Quit();
  }
}
