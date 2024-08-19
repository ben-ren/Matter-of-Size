using Godot;

public partial class Rescaler : RigidBody2D
{
	[Export] public Vector2 originalScale;
	[Export] public Vector2 newScale;
	[Export] public float step = 1;
	[Export] public float impulsePower;
	private float t = 0.0f;
	private bool rescaling = false;
	private bool changeScale = false;
	Node2D child;
    public override void _Ready()
    {
		child = GetNode<Node2D>("CollisionShape2D");
		child.Scale = originalScale;
    }

    public override void _PhysicsProcess(double delta)
    {
		if(Input.IsActionJustPressed("shoot")){
			TriggerRescale();
		}
        Resize(newScale, delta);
    }

	public void Resize(Vector2 targetScale, double delta){
		t += (float)delta * step;								//Increment `t` based on the current step and delta
		t = Mathf.Clamp(t, 0, 1);				//Clamp `t` between 0 and 1
		if(changeScale){
			child.Scale = originalScale.Lerp(targetScale, t);
		}else{
			child.Scale = targetScale.Lerp(originalScale, t);
		}
		if(child.Scale == targetScale || child.Scale == originalScale){
			rescaling = false;
		}
	}

	public void TriggerRescale(){
		changeScale = !changeScale;
		t = 1-t;
		rescaling = true;
	}

	void AddForce(Node2D other){
        if (other is RigidBody2D rb2d)
        {
            Vector2 direction = other.Position - this.Position;
			rb2d.ApplyCentralImpulse(direction * impulsePower);
        }
		else if (other is CharacterBody2D character){
			// Calculate direction vector
			Vector2 direction = character.Position - this.Position;
			// Apply a force along the direction vector
			Vector2 forceDirection = direction.Normalized() * impulsePower * 100f;
			// Apply the combined force to the CharacterBody2D
			character.Velocity += forceDirection;
		}   
	}

	public void SetScales(Vector2 originalScale, Vector2 newScale){
		this.originalScale = originalScale;
		this.newScale = newScale;
	}

	//detects collisions
	public void OnCollisionEnter(Node2D other){
		if(rescaling){
			AddForce(other);
		}
	}
}
