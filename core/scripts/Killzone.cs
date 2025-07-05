using Godot;
using System;

public partial class Killzone : Area2D
{
	private Timer timer;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		BodyEntered += OnBodyEntered;
		timer.Timeout += OnTimerTimeout;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("player"))
		{
			GD.Print("Jogador entrou na killzone!");
			timer.Start();
		}
	}

	private void OnTimerTimeout()
	{
		// Reinicia a cena atual
		GetTree().ReloadCurrentScene();
	}
}
