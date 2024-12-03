using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D {
	private GlobalMaze _GlobalMaze; // custom signals singleton
	[Export] public BoxContainer heartContainer; // bring existing heart container into script
	
 
	[Export] public float speed = 200.0f;
	private Vector2 currentVelocity;
	public Vector2 last_direction;

    public override void _Ready() {
        _GlobalMaze = GetNode<GlobalMaze>("/root/GlobalMaze"); // get the custom signals singleton
		//heartContainer = GetNode<HBoxContainer>("Hearts"); // get the hearts container

		_GlobalMaze.damagePlayer += HandleDamagePlayer; // connect to damagePlayer signal; this will run HandleDamagePlayer()
		_GlobalMaze.flowerCollected += OneFlowerCollected;

    }

	private void OneFlowerCollected() {
		SetCollisionMaskValue(5, false);

	}

	private void HandleDamagePlayer() {
		
		if (_GlobalMaze.heartsRemaining > 0) {
			GD.Print("Ouch! A bee stung you!");
			_GlobalMaze.heartsRemaining -= 1; // subtract 1 heart
			GD.Print("Hearts remaining: ", _GlobalMaze.heartsRemaining);

			//heartContainer.RemoveChild(heartContainer.GetChild(-1)); // remove last child
			//GD.Print("heart container: ", heartContainer);
			//GD.Print("heart container children: ", heartContainer.GetChildren());

			heartContainer.GetChild(-1).QueueFree();
		
		}
		if (_GlobalMaze.heartsRemaining == 0) { // gameOver check
			_GlobalMaze.isGameOver = true;
			GD.Print("The game is now over.");
		}
		
		
		
	}



    public override void _Input(InputEvent @event) {
		handleInput();
    }

    public override void _PhysicsProcess(double delta) {
		Velocity = currentVelocity; // update Velocity
		//MoveAndSlide();
		MoveAndCollide(Velocity * (float)delta); // detect collision; MoveAndCollide returns KinematicCollision2D
		
		// if (collision != null) { // detect collision playerside
        //     GD.Print("Player collided with: ", ((Node)collision.GetCollider()).Name);
        // }
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
