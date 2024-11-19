using Godot;
using System;

public partial class Flower3a : Area2D {
    public override void _Ready() {
        // when the body is entered, FlowerBodyEntered() is called
        BodyEntered += FlowerBodyEntered;
    }
    public void FlowerBodyEntered(Node2D body) {
         if (body.Name == "Player") {
            GD.Print("Entered flower radius");
            //QueueFree();
            //Position = new Vector2(500,500);
            GD.Print("Flower position: " + Position);
            GD.Print("Player position: " + body.Position);
        }
    }
}
