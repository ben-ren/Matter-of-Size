using Godot;
using System;

public partial class Cannon : Rescaler
{
	Timer timer;
	PackedScene rocket;
	bool shoot;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("CollisionShape2D/Sprite2D/Timer");
		rocket = GD.Load<PackedScene>("res://subscenes(objects)/rocket.tscn");
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		TimerTriggerSingleton();
		if(shoot){
			ShootRocket();
			shoot = false;
		}
			
		
		base._PhysicsProcess(delta);
	}

	protected new void OnCollisionEnter(Node2D other){
		base.OnCollisionEnter(other);	//inherit's Rescaler collisions
	}

	void TimerTriggerSingleton(){
		if(timer.TimeLeft <= 0.01){
			shoot = true;
		}
	}

	//Instantiate's a Rocket object and set's it's initial variables
	void ShootRocket(){
		if(rocket == null){
			return;
		}
		Rocket rocketInstance = (Rocket)rocket.Instantiate();
		rocketInstance.Position = this.GlobalPosition;
		rocketInstance.SetScales(new(2, 2), new(4, 4));
		rocketInstance.step = 1;
		GetParent().AddChild(rocketInstance);

		GD.Print("rocket launched!");
	}
}
