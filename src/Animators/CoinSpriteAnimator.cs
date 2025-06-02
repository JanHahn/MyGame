using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;



public struct CoinSprite
{
    public Texture2D spriteSheet;
    public int framesQuantity;
    public float timeInterval;
    public int X_OffSet;
    public int Y_OffSet;
    
}

public class CoinSpriteAnimator : SpriteAnimator
{

    public CoinSpriteAnimator(GraphicsDevice graphicsDevice) : base(graphicsDevice)
    {
        ResetParameters();
    }


    public void Update(GameTime gameTime)
    {
        NormalAnimation(gameTime);
    }

    public void SetAnimation(CoinSprite coinSprite)
    {
        activeSprite_ = coinSprite.spriteSheet;
        totalFrames = coinSprite.framesQuantity;
        interval = coinSprite.timeInterval;
        Active_Y_OffSet = coinSprite.Y_OffSet;
        Active_X_OffSet = coinSprite.X_OffSet;

        frameHeight = activeSprite_.Height;
        frameWidth = GetFrameWidth(totalFrames, activeSprite_);

        activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(currentFrame % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
    }

}