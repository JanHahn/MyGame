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
    
}

public struct HerosSprites
{
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

    protected Action<GameTime> activeFunction_;

    private HerosSprites herosSprites_;
    public HerosSprites HeroSprites
    {
        set { herosSprites_ = value; }
    }


    public bool IsLeft { get; set; }


    public HeroSpriiteAnimator(GraphicsDevice graphicsDevice, HerosSprites heroSprites) : base(graphicsDevice)
    {
        Y_OffSet = 0;
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
                Y_OffSet = 20;
                activeFunction_ = NormalAnimation;
                SetAnimation(herosSprites_.idleSprite);
                Console.WriteLine(activeTexture_.Height);
                break;

            case HeroActions.Run:
                ResetParameters();
                Y_OffSet = 0;
                activeFunction_ = NormalAnimation;
                SetAnimation(herosSprites_.runSprite);
                Console.WriteLine(activeTexture_.Height);
                break;

            case HeroActions.Jump:
                ResetParameters();
                activeFunction_ = NormalAnimation;
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

        frameHeight = activeSprite_.Height;
        frameWidth = GetFrameWidth(totalFrames, activeSprite_);

        activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(currentFrame % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
    }

    private void NormalAnimation(GameTime gameTime)
    {

        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        timer += deltaTime;
        if (timer > interval)
        {
            timer = timer - interval;
            currentFrame++;
            activeTexture_ = ExtractSprite(activeSprite_, new Rectangle(currentFrame % totalFrames * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
    }

}