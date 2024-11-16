using Godot;
using System;

public partial class Flower2hLily : Area2D {
    public override void _Ready() {
        //base._Ready();
        BodyEntered += FlowerBodyEntered;
        //Connect(Area2D.SignalName.BodyEntered, Callable.From(FlowerBodyEntered));
    }
    public void FlowerBodyEntered(Node2D body) {
         if (body.Name == "Player") {
            GD.Print("Entered flower radius");
            QueueFree();
        }
    }
}
