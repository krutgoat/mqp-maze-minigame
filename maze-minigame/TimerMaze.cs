using Godot;
using System;
//using System.Threading;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public partial class TimerMaze : Label {
    [Export] private Timer mazeTimer;
    private GlobalMaze _GlobalMaze;

    private void TimerEnd() {
        _GlobalMaze.EmitSignal(nameof(GlobalMaze.onTimerEnd)); // emits global signal 
        GD.Print("Signal Emitted");
    }
    public override void _Ready() {
        _GlobalMaze = GetNode<GlobalMaze>("res://GlobalMaze");

        //AddChild(mazeTimer);
       //mazeTimer.AutoStart;
        mazeTimer.Timeout += TimerEnd;
        
        //EmitSignal(GlobalMaze.SignalName.onTimerEnd);
        //GlobalMaze.EmitSignal(SignalName.onTimerEnd);
        //var timerVar = GlobalMaze.onTimerEnd;
    }

    public override void _Process(double delta) {
        int seconds = (int)Math.Round(mazeTimer.TimeLeft);
        Text = seconds.ToString();
    }

}
