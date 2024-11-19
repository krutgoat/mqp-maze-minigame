using Godot;
using System;

public partial class GameManager : Node2D {
    private GlobalMaze _GlobalMaze;
    public void TimerEnded() {
        GD.Print("The timer has ended."); // emits global signal 
    }
     public override void _Ready() {
        _GlobalMaze = GetNode<GlobalMaze>("res://GlobalMaze");
        GD.Print(_GlobalMaze);
        _GlobalMaze.onTimerEnd += TimerEnded;
        //GlobalMaze.Connect("onTimerEnded", this, "TimerEnded");
    }
}
