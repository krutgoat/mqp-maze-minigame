using Godot;
using System;

[GlobalClass]
public partial class GlobalMaze : Node {
    internal void Connect(string v1, GameManager gameManager, string v2)
    {
        throw new NotImplementedException();
    }

    [Signal] public delegate void onTimerEndEventHandler();    
}
