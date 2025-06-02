using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;



namespace MyGame;

public class Coin : IPrintable, ICollidable
{

    SpriteAnimator spriteAnimator_;

    public int Worth { get; private set; }
    public int Id { get; private set; }

    public Vector2 Position { get; set; }
    public int Width { get; } = 30;
    public int Height { get; } = 30;

    public Coin(Vector2 Position, int Worth, int Id, SpriteAnimator spriteAnimator)
    {
        this.Worth = Worth;
        this.Id = Id;
        this.spriteAnimator_ = spriteAnimator;
        this.Position = Position;
    }
    

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteAnimator_.ActiveTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }
    

}
