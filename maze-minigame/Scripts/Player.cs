using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D {
	private GlobalMaze _GlobalMaze; // custom signals singleton
	[Export] public BoxContainer heartContainer; // bring existing heart container into script
	[Export] private AnimationPlayer _pauseAnimator;

 
	[Export] public float speed = 200.0f;
	public bool canMove = true;

	private Vector2 currentVelocity;
	public Vector2 last_direction;

    public override void _Ready() {
        _GlobalMaze = GetNode<GlobalMaze>("/root/GlobalMaze"); // get the custom signals singleton
		//heartContainer = GetNode<HBoxContainer>("Hearts"); // get the hearts container

		_GlobalMaze.damagePlayer += HandleDamagePlayer; // connect to damagePlayer signal; this will run HandleDamagePlayer()
		_GlobalMaze.flowerCollected += OneFlowerCollected;
		
		//_GlobalMaze.gameOver +=

    }

	private void OneFlowerCollected() {
		SetCollisionMaskValue(5, false); // player will be able to walk thru (now invisible) guard bee
	}
	
	private void HandleDamagePlayer() {
		
		if (_GlobalMaze.heartsRemaining > 0) {
			GD.Print("Ouch! A bee stung you!");
			_GlobalMaze.heartsRemaining -= 1; // subtract 1 heart
			GD.Print("Hearts remaining: ", _GlobalMaze.heartsRemaining);

			heartContainer.GetChild(-1).QueueFree(); // removes last child from container
			
			GD.Print("Now playing damage animation..");
			
			canMove = false;
			SetCollisionMaskValue(4, false); // not colliding with bees anymore
			DamageAnimation();

		}

		if (_GlobalMaze.heartsRemaining == 0) { // gameOver check
			_GlobalMaze.isGameOver = true;
			GD.Print("The game is now over.");
	        _pauseAnimator.Play("pause");
			GetTree().Paused = true;

		}

		// flashing red tween call
	}

	public float fadeDuration = 0.2f; // for tween function
	private void DamageAnimation() { // blinking red damage
		var tween = GetTree().CreateTween();
		
		for (int i = 0; i < 3; i++) {
			tween.TweenProperty(this, "modulate:r", 1.5, fadeDuration);
			tween.Parallel().TweenProperty(this, "modulate:g", 0.8, fadeDuration);
			tween.Parallel().TweenProperty(this, "modulate:b", 0.8, fadeDuration);
			
			tween.TweenProperty(this, "modulate:r", 1, fadeDuration);
			tween.Parallel().TweenProperty(this, "modulate:g", 1, fadeDuration);
			tween.Parallel().TweenProperty(this, "modulate:b", 1, fadeDuration);
		}  

		//tween.TweenCallback waits for tween to finish before calling a function
		tween.TweenCallback(Callable.From(() => canMove = true)); //this waits for tween to finish before changing image.
		tween.TweenCallback(Callable.From(() => SetCollisionMaskValue(4, true))); // able to take damage from bees again
	}

	// public void afterDamage() {
	// 	canMove = true;
	// }
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
		
		for (int i = 0; i < GetSlideCollisionCount(); i++) { // check for player collision
            var collision = GetSlideCollision(i);
            var collidingBody = ((Node)collision.GetCollider()).Name; // get name of colliding body
            
            //GD.Print("Bee collided with ", collidingBody);
            if (collidingBody == "GuardBee") {
                //_GlobalMaze.EmitSignal("damagePlayer");
                GD.Print("You can't leave yet! Collect at least 1 flower to exit.");
                //_GlobalMaze.EmitSignal(nameof(GlobalMaze.damagePlayer)); // emits global signal 

            }
        }
    }

	// update velocity based on player input
	private void handleInput() {	

		if (canMove == true) {	
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
		}
		else {
			currentVelocity = new Vector2(0,0);
		}
		currentVelocity *= speed;
	}

}
