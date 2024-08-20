using Godot;
using System;

public partial class PauseMenu : Control
{
	Control optionsMenu;
	UserInterface parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		optionsMenu = GetNode<OptionsMenu>("OptionsMenu");
		optionsMenu.Visible = false;
		parent = GetParent<UserInterface>();
	}

	void _on_resume_game_pressed(){
		parent.PauseGame();
	}

	void _on_options_pressed(){
		optionsMenu.Visible = true;
	}

	void _on_exit_to_menu_pressed(){
		parent.PauseGame();
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}
}
