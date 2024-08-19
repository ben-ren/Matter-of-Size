using Godot;
using System;

public partial class AnimationController : Sprite2D
{
	AnimationPlayer child;
	
	public override void _Ready()
	{
		child = GetNode<AnimationPlayer>("RocketAnimation");
		child.Play("spin");
	}

	public override void _Process(double delta)
	{
	}
}
