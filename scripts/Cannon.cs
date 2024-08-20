using Godot;
using System;

public partial class Cannon : Rescaler
{
	Timer timer;
	PackedScene rocket;
	AudioStreamPlayer2D firingSFX;
	bool shoot;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("CollisionShape2D/Sprite2D/Timer");
		rocket = GD.Load<PackedScene>("res://subscenes(objects)/rocket.tscn");
		firingSFX = GetNode<AudioStreamPlayer2D>("FiringSFX");
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
			firingSFX.Playing = true;
		}
	}

	//Instantiate's a Rocket object and set's it's initial variables
	void ShootRocket(){
		if(rocket == null){
			return;
		}
		Rocket rocketInstance = (Rocket)rocket.Instantiate();
		rocketInstance.Position = this.GlobalPosition;
		rocketInstance.SetDirection(new Vector2(this.originalScale.X, 0).Normalized());
		rocketInstance.SetScales(this.originalScale/2, this.newScale/2);
		rocketInstance.step = 3;
		GetParent().AddChild(rocketInstance);

		GD.Print("rocket launched!");
	}
}
