using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] Control options;
	[Export] Control credits;

	public override void _Ready(){
		options.Visible = false;
		credits.Visible = false;
	}

	//Trigger's when Play Button pressed
	void _on_play_pressed(){
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}

	void _on_options_pressed(){
		options.Visible = true;
	}

	void _on_credits_pressed(){
		credits.Visible = !credits.Visible;
	}

	void _on_exit_game_pressed(){
		GetTree().Quit();
	}
}
