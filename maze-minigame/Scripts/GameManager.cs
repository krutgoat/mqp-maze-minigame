using Godot;
using System;

public partial class GameManager : Node2D {
    private GlobalMaze _GlobalMaze;

    [Export] private Godot.Collections.Array<AnimatedSprite2D> flowerSprites = new Godot.Collections.Array<AnimatedSprite2D>();

    public void TimerEnded() {
        GD.Print("The timer has ended."); // emits global signal 
    }

    [Export] private Area2D endBody;

     public override void _Ready() {
        _GlobalMaze = GetNode<GlobalMaze>("/root/GlobalMaze");
        _GlobalMaze.onTimerEnd += TimerEnded;

        //endBody.OnMazeEndBodyEntered += EndingBodyEntered;
        //endBody.BodyEntered += EndingBodyEntered

        foreach (AnimatedSprite2D flower in flowerSprites) {
            flower.Play();
        }
    }

    public void OnMazeEndBodyEntered(Node2D body) {

        GD.Print("Maze ended!");
    }
}
