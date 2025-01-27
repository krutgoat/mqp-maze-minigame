using Godot;
using System;

public partial class GameManager : Node2D {
    private GlobalMaze _GlobalMaze;

    [Export] private AnimationPlayer _pauseAnimator;
    [Export] private Button _continue;

    [Export] private Godot.Collections.Array<AnimatedSprite2D> flowerSprites = new Godot.Collections.Array<AnimatedSprite2D>();

    public void TimerEnded() {
        GD.Print("The timer has ended."); // emits global signal 
        _pauseAnimator.Play("pause");
		GetTree().Paused = true;

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
        _continue.Pressed += () => OnButtonPressed();
        _pauseAnimator.Play("RESET");
    }

    public void OnButtonPressed()
        {
            //Change the scene to the Office Scene
            GetTree().Paused = false;
            GD.Print("Button pressed");
            //GetTree().ChangeSceneToFile("res://Main Scenes/Office/office_scene.tscn");
        }

    public void OnMazeEndBodyEntered(Node2D body) {
        GD.Print("Maze ended!");
        _pauseAnimator.Play("pause");
		GetTree().Paused = true;
    }
}
