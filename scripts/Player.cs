using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Speed = 200;
	[Export] public int JumpForce = -400;
	[Export] public int Gravity = 900;
	[Export] public int MaxJumps = 2;

	private Vector2 velocity = Vector2.Zero;
	private AnimatedSprite2D animatedSprite;
	
	public int GemCount { get; private set; } = 0;

	private int jumpCount = 0;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		float deltaTime = (float)delta;

		// Aplica gravidade
		if (!IsOnFloor())
		{
			velocity.Y += Gravity * deltaTime;
		}
		else
		{
			velocity.Y = 0;
			jumpCount = 0; // Resetar contador de pulo ao tocar o chão
		}

		// Pulo ou pulo duplo
		if (Input.IsActionJustPressed("ui_accept") && jumpCount < MaxJumps)
		{
			velocity.Y = JumpForce;
			jumpCount++;
		}

		// Movimento horizontal
		velocity.X = 0;
		if (Input.IsActionPressed("ui_right"))
			velocity.X += Speed;
		if (Input.IsActionPressed("ui_left"))
			velocity.X -= Speed;

		// Definir animações
		if (!IsOnFloor())
		{
			animatedSprite.Play("Jump");
		}
		else if (velocity.X != 0)
		{
			animatedSprite.Play("Walk");
		}
		else
		{
			animatedSprite.Play("Idle");
		}

		// Virar sprite
		if (velocity.X != 0)
			animatedSprite.FlipH = velocity.X < 0;

		// Mover
		Velocity = velocity;
		MoveAndSlide();
	}

	public void AddGem()
{
	GemCount++;
	var label = GetNode<Label>("HUD/HBoxContainer/Label");
	label.Text = GemCount.ToString();
}

}
