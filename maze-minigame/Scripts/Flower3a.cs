using Godot;
using System;

public partial class Flower3a : Area2D {
    public override void _Ready() {
        //base._Ready();
        BodyEntered += FlowerBodyEntered;
        //Connect(Area2D.SignalName.BodyEntered, Callable.From(FlowerBodyEntered));
    }
    public void FlowerBodyEntered(Node2D body) {
        GD.Print("Entered flower radius");
        
        if (body.Name == "Player") {
            QueueFree();
        }
    }
}
