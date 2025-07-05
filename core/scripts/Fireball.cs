using Godot;
using System;

public partial class Fireball : Area2D
{
	[Export] public int Speed = 500;
	private Vector2 direction = Vector2.Right;

	public void SetDirection(Vector2 dir)
	{
		direction = dir.Normalized();
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += direction * Speed * (float)delta;

		if (Position.X > 2000 || Position.X < -2000)
			QueueFree();
	}

	private void _on_Fireball_body_entered(Node body)
	{
		// Lógica para quando colidir (ex: dano inimigo)
		QueueFree();
	}
}
