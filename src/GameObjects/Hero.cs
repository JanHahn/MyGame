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
        get { return 73; }
        }
    public int Height {
        get { return 73; }
        }

    public Hero(HeroSpriiteAnimator spriteAnimator)
    {
        spriteAnimator_ = spriteAnimator;
        fallingSpeed_ = 0;
        position_ = new Vector2(300, 1000);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        

        Rectangle src = new Rectangle(0, spriteAnimator_.ActiveTexture.Height - 73 - spriteAnimator_.Y_OffSet, spriteAnimator_.ActiveTexture.Width, 73 + spriteAnimator_.Y_OffSet);
        Vector2 position2 = new Vector2(this.position_.X, this.position_.Y - spriteAnimator_.Y_OffSet);
        if (spriteAnimator_.IsLeft)
        {
            spriteBatch.Draw(spriteAnimator_.ActiveTexture, position2, src, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        }
        else
        {
            spriteBatch.Draw(spriteAnimator_.ActiveTexture, position2, src, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}