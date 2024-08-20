using Godot;
using System;

public partial class PlayerAnimationController : Sprite2D
{
	CharacterController parent;
	AnimationPlayer child;
	public override void _Ready()
	{
		child = GetNode<AnimationPlayer>("AnimationPlayer");
		parent = GetParent<CharacterController>();
	}

	public override void _Process(double delta)
	{
		GD.Print(parent.GetAnimationState());
		RunAnimation(parent.GetAnimationState());
		FlipStateCheck();
	}

	void FlipStateCheck(){
		if(parent.GetDirection().X < 0){
			FlipH = true;
		} else if(parent.GetDirection().X > 0){
			FlipH = false;
		}
	}

	void RunAnimation(int animationState){
		switch(animationState){
			default:
				child.Play("idle");
				break;
			case 1:
				child.Play("run");
				break;
			case 2:
				child.Play("jump");
				break;
		}
	}
}
