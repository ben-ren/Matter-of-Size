using Godot;
using System;

public partial class AnimationController : Sprite2D
{
	AnimationPlayer child;
	Rocket parent;
	
	public override void _Ready()
	{
		child = GetNode<AnimationPlayer>("RocketAnimation");
		CollisionShape2D col2d = GetParent<CollisionShape2D>();
		parent = col2d.GetParent<Rocket>();
		child.Play("spin");
	}

	public override void _Process(double delta)
	{
		if(parent.GetDirection().X < 0){
			FlipH = true;
		}else{
			FlipH = false;
		}
	}
}
