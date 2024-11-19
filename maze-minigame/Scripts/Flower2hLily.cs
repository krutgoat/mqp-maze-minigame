using Godot;
using System;

public partial class Flower2hLily : Area2D {

    [Export] public BoxContainer flowerContainer;
    public override void _Ready() {
       // when the body is entered, FlowerBodyEntered() is called
        BodyEntered += FlowerBodyEntered;
    }
    public void FlowerBodyEntered(Node2D body) {
         if (body.Name == "Player") {
            GD.Print("Entered flower radius");
            QueueFree(); // removes flower node

            // creates flower Texture2D & TextureRect
            Texture2D flower2HT = GD.Load<Texture2D>("res://Textures/Flowers/Flower2H.png");
            TextureRect flower2HR = new TextureRect();
            // put texture in the textureRect
            flower2HR.Texture = flower2HT;
            flowerContainer.AddChild(flower2HR);

            //Position = new Vector2(1500,1000);

            GD.Print("New flower position: " + Position);
            GD.Print("Player position: " + body.Position);
            //GD.Print("Player position: " + Player.Posi)
        }
    }

    //public void setPosition()
}
