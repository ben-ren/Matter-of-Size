using Godot;
using System;

public partial class CameraController : Camera2D
{
	[Export] Node2D focusTarget;
	[Export] float X_Min;
	[Export] float X_Max;
	[Export] float Y_Min;
	[Export] float Y_Max;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		defineBoundaries();
	}

	void defineBoundaries(){
		float Xbound = Mathf.Clamp(focusTarget.Position.X, X_Min, X_Max);
		float Ybound = Mathf.Clamp(focusTarget.Position.Y, Y_Max, Y_Min);
		this.Position = new Vector2(Xbound, Ybound);
	}
}
