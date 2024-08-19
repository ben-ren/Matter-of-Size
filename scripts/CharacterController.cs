using Godot;
using System;


public partial class CharacterController : CharacterBody2D
{
	[Export] public float Speed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;
	private RayCast2D ray;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        ray = GetNode<RayCast2D>("RayCast2D");
    }

    public override void _PhysicsProcess(double delta)
	{
		UpdateRaycastTarget();
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		ShrinkRay();
		QueueRedraw();
	}

	public void ShrinkRay(){
		if(Input.IsActionJustPressed("left_click") && ray.IsColliding()){
			GodotObject collidedObject = ray.GetCollider();
			if(collidedObject is Rescaler rescaler){
				rescaler.TriggerRescale();
			}
		}
	}

	public void UpdateRaycastTarget(){
		Vector2 mousePos = GetGlobalMousePosition();
		ray.TargetPosition = (mousePos - GlobalPosition)/2;
	}

	public override void _Draw()
	{
		DrawLine(ray.Position, ray.TargetPosition, new Color(1, 0, 0), 2); // Red line with width of 2
	}
}
