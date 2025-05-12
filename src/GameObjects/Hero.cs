using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class Hero: IFallingObject, IPrintable, ICollidable
{

    private HeroSpriiteAnimator spriteAnimator_;
    public HeroSpriiteAnimator SpriiteAnimator {
        get { return spriteAnimator_; }
        set { spriteAnimator_ = value; }
    }

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

    public int Width { 
        get { return activeTexture_.Width; }
        }
    public int Height {
        get { return activeTexture_.Height; }
        }

    public Hero(HeroSpriiteAnimator spriteAnimator)
    {
        spriteAnimator_ = spriteAnimator;
        fallingSpeed_ = 0;
        position_ = new Vector2(300, 1000);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (spriteAnimator_.IsLeft){
            spriteBatch.Draw(activeTexture_, position_, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        }
        else{
            spriteBatch.Draw(activeTexture_, position_, Color.White);
        }
    }
}