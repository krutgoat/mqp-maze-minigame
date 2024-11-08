using Godot;
using System;

public partial class Player : CharacterBody2D {
	
	[Export]
	private int speed = 200;
	private Vector2 currentVelocity;
	
    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
		handleInput(); // get currentVelocity
		Velocity = currentVelocity; // update Velocity
		MoveAndSlide();
    }

	// update velocity based on player input
	private void handleInput() {
		 currentVelocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//  if Input.IsActionPressed("ui_left") {
		// 	currentVelocity.X = -1;
		//  }
		 currentVelocity *= speed;
	}

}
