using Godot;
using System;

public partial class Flowers : Area2D {
    private GlobalMaze _GlobalMaze;
    [Export] public BoxContainer flowerContainer; // bring existing container into script
    [Export] AnimatedSprite2D FlowerSprite = new AnimatedSprite2D();
    [Export] Texture2D FlowerTexture = new Texture2D();
    
    public override void _Ready() {
        _GlobalMaze = GetNode<GlobalMaze>("/root/GlobalMaze");
        BodyEntered += FlowerBodyEntered;
       
    }


    public void FlowerBodyEntered(Node2D body) {
         if (body.Name == "Player") {
            //GD.Print("Entered flower radius");
            QueueFree(); // removes flower node
            _GlobalMaze.EmitSignal(nameof(GlobalMaze.flowerCollected));
            
            _GlobalMaze.flowersCollected++;

            GD.Print("Flowers collected: " + _GlobalMaze.flowersCollected);

            // creates flower Texture2D & TextureRect
            TextureRect flowerRect = new TextureRect();
            // put texture in the textureRect
            flowerRect.Texture = FlowerTexture;
            flowerContainer.AddChild(flowerRect);
            
        }
    }

}
