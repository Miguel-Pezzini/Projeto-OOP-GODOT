using Godot;
using System;

public partial class TopSunnylandSpike : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("player"))
		{
			GD.Print("entrou");
			GetTree().ReloadCurrentScene();
		}
	}
}
