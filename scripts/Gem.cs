using Godot;
using System;

public partial class Gem : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("player"))
		{
			GD.Print("Gema coletada!");

			// Aumenta contador se o jogador tiver um método ou variável pública
			if (body is Player player)
			{
				player.AddGem(); // ou player.GemCount++;
			}
			QueueFree(); // Remove a gema da cena

		}
	}
}
