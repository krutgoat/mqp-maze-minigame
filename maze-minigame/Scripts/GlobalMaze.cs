using Godot;
using System;

[GlobalClass]
public partial class GlobalMaze : Node {
    internal void Connect(string v1, GameManager gameManager, string v2)
    {
        throw new NotImplementedException();
    }

    [Signal] public delegate void onTimerEndEventHandler();
    [Signal] public delegate void damagePlayerEventHandler();
    [Signal] public delegate void flowerCollectedEventHandler();
    //[Signal] public delegate void allflowerscollectedEventHandler();
    [Signal] public delegate void gameOverEventHandler();
    //[Signal] public delegate void gameOverEventHandler(bool gameOver);



    public int flowersCollected = 0;
    public int heartsRemaining = 3;
    public bool isGameOver = false;
    internal static object _GlobalMaze;
}
