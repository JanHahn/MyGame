using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;


public class SpriteAnimator
{

    protected GraphicsDevice graphicsDevice_;
    public GraphicsDevice GraphicsDevice_
    {
        set { graphicsDevice_ = value; }
    }
    protected Texture2D activeTexture_; //this is one frame of the sprite sheet
    public Texture2D ActiveTexture
    {
        get { return activeTexture_; }
    }

    protected Texture2D activeSprite_ { get; set; }

    public SpriteAnimator(GraphicsDevice graphicsDevice)
    {
        graphicsDevice_ = graphicsDevice;
    }
    
    protected int frameWidth;
    protected int frameHeight;
    protected int currentFrame;
    protected double timer; //stores how much time past from previous sprite frame refresh
    protected double interval;  // interval between each sprite frame
    protected int totalFrames;    // (in each action)


    public int Active_Y_OffSet { get; set; }
    public int Active_X_OffSet { get; set; }

    public int GetFrameWidth(int FramesQuantity, Texture2D SpriteSheet)
    {
        return SpriteSheet.Width / FramesQuantity;
    }

    protected void ResetParameters()
    {
        currentFrame = 0;
        timer = 0;
    } 

    protected void NormalAnimation(GameTime gameTime)
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
    
    protected Texture2D ExtractSprite(Texture2D source, Rectangle sourceRect, GraphicsDevice graphicsDevice)
    {
        Color[] data = new Color[sourceRect.Width * sourceRect.Height];
        source.GetData(0, sourceRect, data, 0, data.Length);

        Texture2D newTexture = new Texture2D(graphicsDevice, sourceRect.Width, sourceRect.Height);
        newTexture.SetData(data);

        return newTexture;
    }
}