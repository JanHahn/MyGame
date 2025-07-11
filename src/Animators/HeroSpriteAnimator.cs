using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;


public enum HeroActions
{
    Idle,
    Run,
    Jump,
    Shot
}

public struct HeroSprite
{
    public Texture2D spriteSheet;
    public int framesQuantity;
    public float timeInterval;
    public int X_OffSet;
    public int Y_OffSet;
    
}

public struct HerosSprites
{
    public int hitBoxWidth;
    public int hitBoxHeight;
    public HeroSprite jumpSprite;
    public HeroSprite runSprite;
    public HeroSprite shotSprite;
    public HeroSprite idleSprite;
    
}

// to ta klasa powinna być odpowiedzialna za wyświetlanie ona powinna mieć draw
// zmienic wtedy w Hero.Width = SpriteAnimator.ActiveTexture.Width
// updatujemy wtedy wszystkie spritey naraz
public class HeroSpriiteAnimator : SpriteAnimator
{

    private Action<GameTime> activeFunction_;

    private HerosSprites herosSprites_;
    public HerosSprites HeroSprites
    {
        set { herosSprites_ = value; }
        get { return herosSprites_; }
    }


    public Hero ConnectedHero { get; set; }
    public bool IsLeft { get; set; }


    public HeroSpriiteAnimator(GraphicsDevice graphicsDevice, HerosSprites heroSprites) : base(graphicsDevice)
    {
        ConnectedHero = new Hero();
        herosSprites_ = heroSprites;

        ResetParameters();
        activeFunction_ = NormalAnimation;
        SetAnimation(herosSprites_.idleSprite);

    }

    public void ChangeState(HeroActions newAction)
    {
        switch (newAction)
        {
            case HeroActions.Idle:
                ResetParameters();
                activeFunction_ = NormalAnimation;
                SetAnimation(herosSprites_.idleSprite);
                break;

            case HeroActions.Run:
                ResetParameters();
                activeFunction_ = NormalAnimation;
                SetAnimation(herosSprites_.runSprite);
                break;

            case HeroActions.Jump:
                ResetParameters();
                activeFunction_ = JumpAnimation;
                SetAnimation(herosSprites_.jumpSprite);
                break;

            case HeroActions.Shot:
                ResetParameters();
                activeFunction_ = NormalAnimation;
                SetAnimation(herosSprites_.shotSprite);
                break;

            default:
                Console.WriteLine("Nieznany stan animacji");
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        activeFunction_(gameTime);
    }

    private void SetAnimation(HeroSprite herosSprite)
    {
        activeSprite_ = herosSprite.spriteSheet;
        totalFrames = herosSprite.framesQuantity;
        interval = herosSprite.timeInterval;
        Active_Y_OffSet = herosSprite.Y_OffSet;
        Active_X_OffSet = herosSprite.X_OffSet;

        frameHeight = activeSprite_.Height;
        frameWidth = GetFrameWidth(totalFrames, activeSprite_);

        activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(currentFrame % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
    }

    private void JumpAnimation(GameTime gameTime)
    {
        //Console.WriteLine(ConnectedHero.FallingSpeed);
        if (ConnectedHero.FallingSpeed < -600)
        {
            activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(2 % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
        else if (ConnectedHero.FallingSpeed < 600)
        {
            activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(3 % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
        else if (ConnectedHero.FallingSpeed < 1200)
        {
            activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(4 % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
        else if (ConnectedHero.FallingSpeed < 1400)
        {
            activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(6 % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
        else if (ConnectedHero.FallingSpeed < 1800)
        {
            activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(7 % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
        else
        {
            activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(8 % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
    }

}