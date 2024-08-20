using Godot;
using System;

public partial class ExitPoint : Area2D
{
	[Export] Node2D spawnPoint;
	private int score;

	public override void _Ready(){
		score = 0;
	}

	void OnBodyEntered(Node2D other){
		other.Position = spawnPoint.Position;
		score++;
	}

	public int GetScore(){
		return score;
	}
}
