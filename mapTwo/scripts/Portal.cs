using Godot;
using System;

namespace MapTwo {
	public partial class Portal : Area2D
{
	[Export] public string ScenePath = "res://mapOne/scenes/mapOne.tscn";

	private CharacterBody2D playerInside = null;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("player"))
			playerInside = body as CharacterBody2D;
	}

	private void OnBodyExited(Node2D body)
	{
		if (body == playerInside)
			playerInside = null;
	}

	public override void _Process(double delta)
	{
		if (playerInside != null && Input.IsActionJustPressed("ui_up"))
		{
			Global.mapaAtual = "mapTwo";
			GetTree().ChangeSceneToFile(ScenePath);
		}
	}
}
}
