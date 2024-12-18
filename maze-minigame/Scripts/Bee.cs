using Godot;
using System;

public partial class Bee : CharacterBody2D {
    private GlobalMaze _GlobalMaze; // custom signals singleton
    
	[Export] public float beeSpeed = 200.0f;
    public bool canMove = true;

    [Export] private Vector2 currentVelocity;
    [Export] private AnimatedSprite2D bee;

    //private bool isFlipped = false;
    private Vector2 direction = new Vector2(-1,0);

    public override void _Ready() { // only called once; used for initilization
        _GlobalMaze = GetNode<GlobalMaze>("/root/GlobalMaze");
        
        //HeartSetup();
        
        //BodyEntered += BeeBodyEntered;
    }

    public void BeeBodyEntered(Node2D body) { // check for player collision
        if (body.Name == "player") {

        }
    }

    public override void _PhysicsProcess(double delta) {
        
        // if (canMove == true) {	
        //     currentVelocity = beeSpeed * direction; // set currentVelocity 
        // }
        // else {
		// 	currentVelocity = new Vector2(0,0);
		// }

        if (IsOnWall()) { // check if bee hit something
            direction *= -1; // flip direction
            bee.SetFlipH(!bee.IsFlippedH());
        }
        
        currentVelocity = beeSpeed * direction; // set currentVelocity 
        Velocity = currentVelocity;
        MoveAndSlide();
        
        for (int i = 0; i < GetSlideCollisionCount(); i++) { // check for player collision
            var collision = GetSlideCollision(i);
            var collidingBody = ((Node)collision.GetCollider()).Name; // get name of colliding body
            
            //GD.Print("Bee collided with ", collidingBody);
            if (collidingBody == "Player") {

                _GlobalMaze.EmitSignal(nameof(GlobalMaze.damagePlayer)); // emits global signal 
                //canMove = false;
                //direction *= -1; // flip direction
                //bee.SetFlipH(!bee.IsFlippedH());

            }
        }
		//MoveAndCollide(Velocity * (float)delta);
    }


}
