using Godot;

public partial class Rescaler : Node2D
{
	[Export] public Vector2 originalScale;
	[Export] public Vector2 newScale;
	[Export] public float step = 1;
	private float t = 0.0f;
	private bool rescaling = false;
	Node2D child;
    public override void _Ready()
    {
		child = GetNode<Node2D>("CollisionShape2D");
		child.Scale = originalScale;
    }

    public override void _PhysicsProcess(double delta)
    {
		if(Input.IsActionJustPressed("shoot")){
			rescaling = !rescaling;
			t = 1-t;
		}
        Resize(newScale, delta);
    }

	public void Resize(Vector2 targetScale, double delta){
		t += (float)delta * step;								//Increment `t` based on the current step and delta
		t = Mathf.Clamp(t, 0, 1);				//Clamp `t` between 0 and 1
		if(rescaling){
			child.Scale = originalScale.Lerp(targetScale, t);
		}else{
			child.Scale = targetScale.Lerp(originalScale, t);
		}
		//GD.Print(Scale);
	}
}
