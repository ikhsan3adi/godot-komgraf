namespace Godot;

using Godot;
using System;

public partial class BackBtn : Button
{
	private void _on_BtnBack_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Welcome.tscn");
	}
}
