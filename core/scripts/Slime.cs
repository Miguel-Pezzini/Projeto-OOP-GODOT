using Godot;
using System;

public partial class Slime : Node2D
{
	private const float SPEED = 60f;
	private int direction = 1;

	private RayCast2D rayCastRight;
	private RayCast2D rayCastLeft;
	private AnimatedSprite2D animatedSprite;

	public override void _Ready()
	{
		rayCastRight = GetNode<RayCast2D>("RayCastRight");
		rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
		if (rayCastRight.IsColliding())
		{
			direction = -1;
			animatedSprite.FlipH = true;
		}

		if (rayCastLeft.IsColliding())
		{
			direction = 1;
			animatedSprite.FlipH = false;
		}

		Position += new Vector2(direction * SPEED * (float)delta, 0);
	}
}
