using Godot;
using System;

public partial class Bee : CharacterBody2D {
	[Export] public float beeSpeed = 200.0f;
    [Export] private Vector2 currentVelocity;
    [Export] private AnimatedSprite2D bee;
    //private bool isFlipped = false;
    private Vector2 direction = new Vector2(-1,0);

    // public override void _Ready() {
    //     BodyEntered += BeeBodyEntered;
    // }

    // public void BeeBodyEntered(Node2D body) {
    //     if (body.Name == "Player") {
    //         GD.Print("Entered flower radius");
    //         QueueFree();
    //     }
    // }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        if (IsOnWall()) { // check if bee hit the wall
            direction *= -1; // flip direction
            //bee.SetFrameAndProgress(1, 0); // flip sprite
            bee.SetFlipH(!bee.IsFlippedH());
        }

        currentVelocity = beeSpeed * direction; // set currentVelocity
        Velocity = currentVelocity;
        MoveAndSlide();
    }


}
