using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace MyGame;

public class SamuraiArcher: HeroSpriiteAnimator
{

    public SamuraiArcher(Texture2D activeTexture, GraphicsDevice graphicsDevice):base(activeTexture, graphicsDevice){
        string contentRoot = AppContext.BaseDirectory;
        string IdlePath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Idle.png");
        string RunPath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Run.png");
        string JumpPath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Jump.png");
        string Attack_1Path = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Arrow.png");

        jumpSpriteSheet_ = Texture2D.FromFile(graphicsDevice_, JumpPath);
        runSpriteSheet_ = Texture2D.FromFile(graphicsDevice_, RunPath);
        shotSpriteSheet_ = Texture2D.FromFile(graphicsDevice_, Attack_1Path);
        IdleSpriteSheet_ = Texture2D.FromFile(graphicsDevice_, IdlePath);

        activeFunction_ = Idle;
        activeTexture = IdleSpriteSheet_;
        totalFrames = 9;
        frameWidth = GetFrameWidth(totalFrames, IdleSpriteSheet_);
        frameHeight = activeTexture.Height;
        currentFrame = 0;
        timer = 0f;
        interval = 1;
    }

    public override void Idle(GameTime gameTime){
        ;
    }
    public override void Jump(GameTime gameTime){
        ;
    }
    public override void Run(GameTime gameTime){
        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        timer += deltaTime;
        if (timer > interval){
            timer = timer - interval;
            currentFrame++;
            activeTexture_ = ExtractSprite(runSpriteSheet_, new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight), graphicsDevice_);
        }
    }
    public override void Shot(GameTime gameTime){
        ;
    }



    Texture2D ExtractSprite(Texture2D source, Rectangle sourceRect, GraphicsDevice graphicsDevice)
        {
            Color[] data = new Color[sourceRect.Width * sourceRect.Height];
            source.GetData(0, sourceRect, data, 0, data.Length);

            Texture2D newTexture = new Texture2D(graphicsDevice, sourceRect.Width, sourceRect.Height);
            newTexture.SetData(data);

            return newTexture;
        }
}