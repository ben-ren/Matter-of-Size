using Godot;
using System;

public partial class CannonAnimator : Sprite2D
{
	AnimationPlayer child;
	Timer timer;

	public override void _Ready()
	{
		child = GetNode<AnimationPlayer>("CannonAnimationPlayer");
		timer = GetNode<Timer>("Timer");
	}

	public override void _PhysicsProcess(double delta)
	{
		if(timer.TimeLeft < 1.0f){
			child.Play("shoot");
		}
	}
}
