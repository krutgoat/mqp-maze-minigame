using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D {
	
	[Export] public float speed = 200.0f;
	[Export] private Vector2 currentVelocity;
	public Vector2 last_direction;

    public override void _Input(InputEvent @event) {
		handleInput();
    }

    public override void _PhysicsProcess(double delta) {
		//Vector2 velocity = Velocity;
        //base._PhysicsProcess(delta);
		//handleInput(); // get currentVelocity
		Velocity = currentVelocity; // update Velocity
		MoveAndSlide();
    }

	// update velocity based on player input
	private void handleInput() {		
		currentVelocity = Input.GetVector("left", "right", "up", "down");

		if (Math.Abs(currentVelocity.X) > 0 && Math.Abs(currentVelocity.Y) > 0) { // if moving
			if (Input.IsActionJustPressed("left") || Input.IsActionJustPressed("right")) { // L & R
				currentVelocity = new Vector2(currentVelocity.X, 0);
			}
			else if (Input.IsActionJustPressed("up") || Input.IsActionJustPressed("down")) {
				currentVelocity = new Vector2(0, currentVelocity.Y);
			}
			else {
				currentVelocity = last_direction;
			}
		}
		last_direction = currentVelocity;
		currentVelocity *= speed;
	}

}
