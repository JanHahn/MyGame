using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;


public enum HeroActions {
    Idle,
    Run,
    Jump,
    Shot
}


// to ta klasa powinna być odpowiedzialna za wyświetlanie ona powinna mieć draw
// zmienic wtedy w Hero.Width = SpriteAnimator.ActiveTexture.Width
// updatujemy wtedy wszystkie spritey naraz
public abstract class HeroSpriiteAnimator: SpriteAnimator {

    protected Action<GameTime> activeFunction_;

    protected Texture2D jumpSpriteSheet_;
    protected Texture2D runSpriteSheet_;
    protected Texture2D shotSpriteSheet_;
    protected Texture2D IdleSpriteSheet_;

    public bool IsLeft { get; set; }

    public HeroSpriiteAnimator(Texture2D activeTexture, GraphicsDevice graphicsDevice):base(activeTexture,graphicsDevice){}

    public void ChangeState(HeroActions newAction){
        switch(newAction){
            case HeroActions.Idle:
                activeFunction_ = Idle;
                timer = 0;
                currentFrame = 0;
                break;

            case HeroActions.Run:
                activeFunction_ = Run;
                timer = 0;
                currentFrame = 0;
                break;

            case HeroActions.Jump:
                activeFunction_ = Jump;
                timer = 0;
                currentFrame = 0;
                break;

            case HeroActions.Shot:
                activeFunction_ = Shot;
                timer = 0;
                currentFrame = 0;
                break;

            default:
                Console.WriteLine("Nieznany stan animacji");
                break;
        }
    }

    public void Update(GameTime gameTime){
        activeFunction_(gameTime);
    }

    virtual public void Idle(GameTime gameTime){}
    virtual public void Jump(GameTime gameTime){}
    virtual public void Run(GameTime gameTime){}
    virtual public void Shot(GameTime gameTime){}
}