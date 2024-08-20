using Godot;
using System;

public partial class MusicController : Node
{
	AudioStreamPlayer music;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		music.Play();
	}

	void _on_audio_stream_player_finished(){
		music.Play();
	}
}
