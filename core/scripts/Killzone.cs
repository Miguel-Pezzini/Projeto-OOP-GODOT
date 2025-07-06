using Godot;
using System;

public partial class Killzone : Area2D
{
	private Timer timer;
	private CharacterBody2D player;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		BodyEntered += OnBodyEntered;
		timer.Timeout += OnTimerTimeout;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("player")) {
			player = body as CharacterBody2D;
			if (player != null) {
				var playerScript = player as Player;
				if (playerScript != null) {
					Global.ResetGemasMapaAtual();
					playerScript.ResetHUD();
				}
				player.Velocity = Vector2.Zero;
				player.SetPhysicsProcess(false);
				
				var anim = player.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
				if (anim != null)
					anim.Play("Dead");
				timer.Start();
			}
		}
	}

	private void OnTimerTimeout()
	{
		GetTree().ReloadCurrentScene();
	}
}
