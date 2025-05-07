using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;


public abstract class SpriteAnimator {

    protected private Texture2D activeTexture; //this is sprite sheet 

    protected int frameWidth;
    protected int frameHeight;
    protected int currentFrame;
    protected float timer; //stores how much time past from previous sprite frame refresh
    protected float interval;  // interval between each sprite frame
    protected int totalFrames;    // (in each action)

}