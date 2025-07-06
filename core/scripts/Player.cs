using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public PackedScene FireballScene;
	[Export] public int WalkSpeed = 120;
	[Export] public int RunSpeed = 250;
	[Export] public int JumpForce = -300;
	[Export] public int DoubleJumpForce = -300;
	[Export] public int Gravity = 900;
	[Export] public int MaxJumps = 2;
	[Export] public int WallSlideSpeed = 100;
	[Export] public int ClimbSpeed = 100;
	[Export] public float ShootDuration = 0.3f;

	private Vector2 velocity = Vector2.Zero;
	private AnimatedSprite2D animatedSprite;

	private int jumpCount = 0;
	private bool isOnLadder = false;
	
	private bool justJumped = false;

	private float shootTimer = 0f;

	private enum PlayerState
	{
		Idle,
		Running,
		Jumping,
		DoubleJumping,
		WallSliding,
		Climbing,
		Shooting
	}

	private PlayerState currentState = PlayerState.Idle;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		var label = GetNode<Label>("HUD/HBoxContainer/Label");
		label.Text = Global.GetGemasDoMapaAtual().ToString();
	}

	public override void _PhysicsProcess(double delta)
	{
		float deltaTime = (float)delta;

		if (currentState == PlayerState.Shooting)
		{
			shootTimer -= deltaTime;
			if (shootTimer <= 0)
				currentState = IsOnFloor() ? PlayerState.Idle : PlayerState.Jumping;
		}

		HandleInput(deltaTime);
		ApplyPhysics(deltaTime);
		UpdateAnimation();
		MoveAndSlide();
	}

	private void HandleInput(float delta)
	{
		// Se estiver atirando, ignora outros comandos
		if (currentState == PlayerState.Shooting)
			return;

		if (Input.IsActionJustPressed("shoot"))
		{
			currentState = PlayerState.Shooting;
			shootTimer = ShootDuration;
			Shoot();
			return;
		}

		float inputX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		int speed = Input.IsActionPressed("ui_shift") ? RunSpeed : WalkSpeed;
		velocity.X = inputX * speed;

		// ESCADA
		if (isOnLadder)
		{
			bool isPressingUp = Input.IsActionPressed("ui_up");
			bool isPressingDown = Input.IsActionPressed("ui_down");

			if (isPressingUp)
			{
				velocity.Y = -ClimbSpeed;
				currentState = PlayerState.Climbing;
				return;
			}
			else if (currentState == PlayerState.Climbing && (isPressingDown || velocity.Y != 0))
			{
				velocity.Y = ClimbSpeed;
				return;
			}
			else if (currentState == PlayerState.Climbing)
			{
				if (!IsOnFloor())
					currentState = PlayerState.Jumping;
				else
					currentState = velocity.X == 0 ? PlayerState.Idle : PlayerState.Running;
				return;
			}
		}

		// GRAVIDADE
		if (!IsOnFloor())
			velocity.Y += Gravity * delta;
		else
		{
			velocity.Y = 0;
			jumpCount = 0;
		}

		// PULO
	if (Input.IsActionJustPressed("ui_accept")) {
		if (IsOnFloor()) {
			velocity.Y = JumpForce;
			currentState = PlayerState.Jumping;
			jumpCount = 1;
			justJumped = true;
		}
		else if (IsOnWall() && velocity.Y > 0) {
		velocity.Y = JumpForce;
		velocity.X = GetWallNormal().X * speed;
		currentState = PlayerState.Jumping;
		justJumped = true;
		}
		else if (jumpCount < MaxJumps) {
			velocity.Y = DoubleJumpForce;
			currentState = PlayerState.DoubleJumping;
			jumpCount++;
			justJumped = true;
		}
}

		// DESLIZANDO PAREDE
		if (!IsOnFloor() && IsOnWall() && velocity.Y > 0)
		{
			velocity.Y = Mathf.Min(velocity.Y, WallSlideSpeed);
			currentState = PlayerState.WallSliding;
		}
		else if (IsOnFloor() && !justJumped) {
			currentState = velocity.X == 0 ? PlayerState.Idle : PlayerState.Running;
		}	
		justJumped = false;
	}

	private void ApplyPhysics(float delta)
	{
		if (velocity.X != 0)
			animatedSprite.FlipH = velocity.X < 0;

		Velocity = velocity;
	}

	private void UpdateAnimation()
	{
		switch (currentState)
		{
			case PlayerState.Idle:
				animatedSprite.Play("Idle");
				break;
			case PlayerState.Running:
				animatedSprite.Play(Input.IsActionPressed("ui_shift") ? "Run" : "Walk");
				break;
			case PlayerState.Jumping:
			case PlayerState.DoubleJumping:
				animatedSprite.Play("Jump");
				break;
			case PlayerState.WallSliding:
				animatedSprite.Play("WallSlide");
				break;
			case PlayerState.Climbing:
				animatedSprite.Play("Climb");
				break;
			case PlayerState.Shooting:
				animatedSprite.Play("Shot");
				break;
		}
	}

	private void Shoot()
	{
		if (FireballScene == null)
			return;

		var fireball = (Fireball)FireballScene.Instantiate();
		fireball.Position = Position + new Vector2(animatedSprite.FlipH ? -20 : 20, 0);
		fireball.SetDirection(animatedSprite.FlipH ? Vector2.Left : Vector2.Right);
		GetParent().AddChild(fireball);
	}

	public void AddGem() {
		Global.AddGema();
		var label = GetNode<Label>("HUD/HBoxContainer/Label");
		label.Text = Global.GetGemasDoMapaAtual().ToString();
	}

	public void ResetHUD() {
		var label = GetNode<Label>("HUD/HBoxContainer/Label");
		label.Text = Global.GetGemasDoMapaAtual().ToString();
	}

	public void SetIsOnLadder(bool value)
	{
		isOnLadder = value;
	}
}
