using Godot;
using System;

public partial class GuardBee : StaticBody2D {
    private GlobalMaze _GlobalMaze;

    [Export] public float beeSpeed = 0f;
    [Export] private Vector2 currentVelocity;
    [Export] private AnimatedSprite2D guardBee;

    public float fadeDuration = 1f; // for tween function


    public override void _Ready() {
        _GlobalMaze = GetNode<GlobalMaze>("/root/GlobalMaze");
        //BodyEntered += BeeBodyEntered;

        _GlobalMaze.flowerCollected += OneFlowerCollected;

        if (_GlobalMaze.flowersCollected >= 1) {
            GD.Print("At least 1 flower has been collected. The Guard Bee is leaving now!");
            QueueFree(); // trying to get guard bee to disappear once flower is collected
        }
    }

    private void OneFlowerCollected() {
        var tween = GetTree().CreateTween();
        tween.TweenProperty(this, "modulate:a", 0, fadeDuration).SetEase(Tween.EaseType.Out);

        //SetCollisionMaskValue(5, false);
		
	}

    public override void _PhysicsProcess(double delta) {
        
    }
}
