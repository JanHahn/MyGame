using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class Hero: IPrintable
{
    private Vector2 position_;
    private Dictionary<string, (Texture2D, int)> actions_;

    public Dictionary<string, (Texture2D, int)> Actions { 
        get { return actions_; }
        set { actions_ = value; } 
    }
    public Vector2 Position { 
        get { return position_; }
        set { position_ = value; }
    }

    private Texture2D activeTexture;
    float acceleration = 8000;
    float speed = 0f; // Piksele na sekundę

    
    int frameWidth = 1;         // Szerokość jednej klatki
    int frameHeight = 1;        // Wysokość jednej klatki
    int currentFrame = 0;   // Aktualna klatka
    float timer = 0f;       // Licznik czasu
    float interval = 0.05f;  // Ile sekund pokazujemy jedną klatkę
    int totalFrames = 6;    // Ile wszystkich klatek masz w pliku


    bool isLeft = false;
    bool RunningLeft = false;
    bool RunningRight = false;
    bool idleNow = true;
    bool flying = false;


    const float BOTTOM_LEVEL = 850f; // <- dodajemy tę stałą

    public Hero(Dictionary<string, (Texture2D, int)> actions, Vector2 position)
    {
        actions_ = actions;
        position_ = position;
        activeTexture = actions["Idle"].Item1;
        totalFrames = actions["Idle"].Item2;
        frameWidth = activeTexture.Width/totalFrames;
        frameHeight = activeTexture.Height;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);
        if (isLeft){
            spriteBatch.Draw(activeTexture, Position, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);    
        }
        else{
            spriteBatch.Draw(activeTexture, Position, sourceRectangle, Color.White);
        }
        
    }

    public void Update(GameTime gameTime){
        
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer >= interval)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            timer = 0f;
        }

        var keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Left)){
            if (!RunningRight){
                activeTexture = actions_["Run"].Item1;
                totalFrames = actions_["Run"].Item2;
                isLeft = false;
                RunningRight = true;
                idleNow = false;
            }
            position_.X += 10;
        } 
        else if (keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right)){
            if (!RunningLeft){
                activeTexture = actions_["Run"].Item1;
                totalFrames = actions_["Run"].Item2;
                isLeft = true;
                RunningLeft = true;
                idleNow = false;
            }
            position_.X -= 10;
        }
        else {
            if (!idleNow){
                activeTexture = actions_["Idle"].Item1;
                totalFrames = actions_["Idle"].Item2;
                RunningLeft = false;
                RunningRight = false;
                idleNow = true;
            }
        }


        if (keyboard.IsKeyDown(Keys.Up) && speed == 0){
            flying = true; 
            speed = 2000;
        }

        if (flying){
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            speed -= deltaTime * acceleration;

            position_.Y -= deltaTime * speed;

            if (position_.Y >= BOTTOM_LEVEL)
            {
                position_.Y = BOTTOM_LEVEL;
                speed = 0;
            }
            }
        }
}