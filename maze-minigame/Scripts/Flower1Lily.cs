using Godot;
using System;

public partial class Flower1Lily : Area2D
{
    // Func _onfl1_body_entered(body)  { }

    public override void _Ready()
    {
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
