using Godot;
using System;

public partial class ExitPoint : Area2D
{
	[Export] Node2D spawnPoint;
	AudioStreamPlayer scoreGain;
	private int score;

	public override void _Ready(){
		scoreGain = GetNode<AudioStreamPlayer>("scoreSFX");
		score = 0;
	}

	void OnBodyEntered(Node2D other){
		other.Position = spawnPoint.Position;
		score++;
		scoreGain.Playing = true;
	}

	public int GetScore(){
		return score;
	}
}
