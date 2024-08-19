using Godot;
using System;

public partial class Rocket : Rescaler
{
	[Export] float speed;
	Vector2 direction = Vector2.Left;
	public override void _Ready()
	{
		base._Ready();	//inherit's Rescaler Setup
	}
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);	//inherit's Rescaler scaling logic

		MoveRocket((float)delta);
	}

	//detects collisions
	protected new void OnCollisionEnter(Node2D other){
		base.OnCollisionEnter(other);	//inherit's Rescaler collisions
		QueueFree();	//destroy's object on collision
	}

	//Moves Rocket
	void MoveRocket(float delta){
		ApplyCentralForce(direction * speed * delta * 100);
	}
}
