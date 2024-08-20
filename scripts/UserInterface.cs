using Godot;
using System;

public partial class UserInterface : Control
{
	[Export] ExitPoint exit;
	Control pauseMenu;
	RichTextLabel textBox;
	bool gamePause = false; 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pauseMenu = GetNode<PauseMenu>("PauseMenu");
		pauseMenu.Visible = gamePause;
		ProcessMode = ProcessModeEnum.Always;
		textBox = GetNode<RichTextLabel>("VBoxContainer/RichTextLabel");
		textBox.Text = "Score: " + exit.GetScore().ToString();
	}

    public override void _Process(double delta)
    {
        textBox.Text = "Score: " + exit.GetScore().ToString();
    }

    public override void _UnhandledInput(InputEvent @event){
		if(@event.IsActionPressed("pause")){
			PauseGame();
		}
	}

	public void PauseGame(){
		gamePause = !gamePause;
		GetTree().Paused = gamePause;
		pauseMenu.Visible = gamePause;
	}
}
