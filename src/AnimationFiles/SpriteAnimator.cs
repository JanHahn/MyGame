using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;


public abstract class SpriteAnimator {

    protected GraphicsDevice graphicsDevice_;
    public GraphicsDevice GraphicsDevice_{
        set { graphicsDevice_ = value; } 
    }
    protected Texture2D activeTexture_; //this is one frame of the sprite sheet

    public SpriteAnimator(Texture2D activeTexture, GraphicsDevice graphicsDevice){
        activeTexture_ = activeTexture;
        graphicsDevice_ = graphicsDevice;
    }
    protected int frameWidth;
    protected int frameHeight;
    protected int currentFrame;
    protected double timer; //stores how much time past from previous sprite frame refresh
    protected double interval;  // interval between each sprite frame
    protected int totalFrames;    // (in each action)

    public int GetFrameWidth(int FramesQuantity, Texture2D SpriteSheet){
        return SpriteSheet.Width/FramesQuantity;
    }

}