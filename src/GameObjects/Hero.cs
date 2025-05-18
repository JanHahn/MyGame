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

    public int Width { get; set; }
    public int Height { get; set; }

    public Hero(HeroSpriiteAnimator spriteAnimator)
    {
        spriteAnimator_ = spriteAnimator;
        fallingSpeed_ = 0;
        position_ = new Vector2(300, 1000);

        Width = spriteAnimator.HeroSprites.hitBoxWidth;
        Height = spriteAnimator.HeroSprites.hitBoxHeight;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        //a lot of calculating! Maybe slow!
        //Rectangle src = new Rectangle(0, spriteAnimator_.ActiveTexture.Height - this.Height - spriteAnimator_.Active_Y_OffSet, spriteAnimator_.ActiveTexture.Width, this.Height + spriteAnimator_.Active_Y_OffSet);
        if (spriteAnimator_.IsLeft)
        {
            Vector2 texturePosition = new Vector2(this.position_.X - (spriteAnimator_.ActiveTexture.Width - this.Width - spriteAnimator_.Active_X_OffSet) , this.position_.Y - spriteAnimator_.Active_Y_OffSet);
            spriteBatch.Draw(spriteAnimator_.ActiveTexture, texturePosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        }
        else
        {
            
            Vector2 texturePosition = new Vector2(this.position_.X - spriteAnimator_.Active_X_OffSet, this.position_.Y - spriteAnimator_.Active_Y_OffSet);
            spriteBatch.Draw(spriteAnimator_.ActiveTexture, texturePosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}