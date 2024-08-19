using Godot;
using System;

public partial class AnimationController : Sprite2D
{
	AnimationPlayer child;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		child = GetNode<AnimationPlayer>("RocketAnimation");
		child.Play("spin");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
