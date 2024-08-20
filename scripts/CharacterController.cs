using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	[Export] public float Speed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;
	[Export] public Node2D spawnPoint;
	AudioStreamPlayer jumpSFX;
	AudioStreamPlayer shrinkRaySFX;
	private Vector2 direction;
	private RayCast2D ray;
	private int animationState;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        ray = GetNode<RayCast2D>("RayCast2D");
		jumpSFX = GetNode<AudioStreamPlayer>("JumpSFX");
		shrinkRaySFX = GetNode<AudioStreamPlayer>("ShrinkRaySFX");
		FallPitReset();
    }

    public override void _PhysicsProcess(double delta)
	{
		UpdateRaycastTarget();
		PlayerMovement(delta);
		if(Position.Y >= 1000){
			FallPitReset();
		}
		MoveAndSlide();
		ShrinkRay();
		QueueRedraw();
	}

	private void PlayerMovement(double delta){
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor()){
			jumpSFX.Playing = true;
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		SetAnimationState(velocity);

		Velocity = velocity;
	}

	public void ShrinkRay(){
		if(Input.IsActionJustPressed("left_click") && ray.IsColliding()){
			GodotObject collidedObject = ray.GetCollider();
			if(collidedObject is Rescaler rescaler){
				shrinkRaySFX.Playing = true;
				rescaler.TriggerRescale();
			}
		}
	}

	void FallPitReset(){
		this.Position = spawnPoint.Position;
	}

	public void UpdateRaycastTarget(){
		Vector2 mousePos = GetGlobalMousePosition();
		ray.TargetPosition = (mousePos - GlobalPosition)/2;
	}

	public int GetAnimationState(){
		return animationState;
	}

	public Vector2 GetDirection(){
		return direction;
	}

	void SetAnimationState(Vector2 velocity){
		if(velocity != Vector2.Zero && IsOnFloor()){
			animationState = 1;	//running animation
			return;
		}else if(!IsOnFloor()){
			animationState = 2;	//jumping animation
			return;
		}
		animationState = 0;	//idling animation
	}

	public override void _Draw()
	{
		DrawLine(ray.Position, ray.TargetPosition, new Color(1, 0, 0), 2); // Red line with width of 2
	}
}
