using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class Obstacle : ICollidable
{
    public Texture2D Texture { get; set; }
    public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

    public Vector2 Position { get; set; }
    public int Width {get; set;}
    public int Height {get; set;} 

    public Obstacle(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        Width = Texture.Width;
        Height = Texture.Height;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
}