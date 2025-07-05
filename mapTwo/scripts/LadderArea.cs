using Godot;
using System;

public partial class LadderArea : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("player"))
		{
			var player = (Player)body;
			player.SetIsOnLadder(true);
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body.IsInGroup("player"))
		{
			var player = (Player)body;
			player.SetIsOnLadder(false);
		}
	}
}
