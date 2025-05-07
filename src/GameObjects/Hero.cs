using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class Hero: IFallingObject, IPrintable, ICollidable
{

    private SpriteAnimator spriteAnimator_;

    private Texture2D activeTexture_;
    public Texture2D ActiveTexture{
        set { activeTexture_ = value; }
        get { return activeTexture_; }
    }


    public bool IsFalling { get; set; }
    private float fallingSpeed_;
    public float FallingSpeed { 
        get { return fallingSpeed_; }
        set { fallingSpeed_ = value; }
    }

    private Vector2 position_;
    public Vector2 Position {
        get { return position_; }
        set { position_ = value; }
    }

    public int Width { get; set; }
    public int Height { get; set; }

    public Hero(SpriteAnimator spriteAnimator)
    {
        spriteAnimator_ = spriteAnimator;
        fallingSpeed_ = 0;
        position_ = new Vector2(300, 1000);
        Width = 20;
        Height = 20;
        //Width = activeTexture_.Width;
        //Height = activeTexture_.Height;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(activeTexture_, position_, Color.White);
    }
}