using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;


public abstract class HeroSpriiteAnimator: SpriteAnimator {

    private Texture2D jump_;
    private Texture2D run_;
    private Texture2D shot_;
    private Texture2D Idle_;


    public HeroSpriiteAnimator() {
        frameWidth = 1;
        frameHeight = 1;
        currentFrame = 0;
        timer = 0f;
        interval = 0.1f;
        totalFrames = 1;
    }


    void Idle(){
        

    }

    void Jump(){


    }

    void Run(){


    }

    void Shot(){


    }




}