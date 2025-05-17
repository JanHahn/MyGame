using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MyGame;

public class BackGroundSlider: IPrintable{

    public Vector2 Position {get; set;}

    private List<Texture2D> backGroundLayers_;
    private List<float> backGroundSpeedFactors_;
    private Hero hero_;
    private Vector2 scale_;
    private int screenWidth_;
    private int screenHeight_;
    private int rightWrapPoint;
    private int leftWrapPoint;
    private int shift = 0;

    List<ICollidable> testList;
    public List<ICollidable> TestList { 
        get { return testList; }
        set { testList = value; }
     }

    public List<IPrintable> TestList2 { get; set; }
    
    
    public BackGroundSlider(List<Texture2D> backGroundLayers, List<float> backGroundSpeedFactors, Hero hero,int screenWidth, int screenHeight, Vector2 scale ){

        Position = new Vector2(0,0);

        backGroundLayers_ = backGroundLayers;
        backGroundSpeedFactors_ =  backGroundSpeedFactors;
        hero_ = hero;
        screenHeight_ = screenHeight;
        screenWidth_ = screenWidth;
        scale_ = scale;

        rightWrapPoint = (int)Math.Round(screenWidth_ * 0.90);
        leftWrapPoint = (int)(Math.Round(screenWidth_ * 0.10) - hero_.Width);
        
    }


    public void Draw(SpriteBatch spriteBatch){


        for (int layer = 0; layer < backGroundLayers_.Count; layer++ ){
            
            int localShift = (int)(shift*backGroundSpeedFactors_[layer]) % backGroundLayers_[layer].Width;
            //int localShift = shift % backGroundLayers_[layer].Width;

            Rectangle sourceRectangle = new Rectangle(localShift, 0, backGroundLayers_[layer].Width-localShift, backGroundLayers_[layer].Height);
            Rectangle sourceRectangle2 = new Rectangle(0, 0, localShift, backGroundLayers_[layer].Height);
            spriteBatch.Draw(backGroundLayers_[layer], new Vector2(0,0),sourceRectangle, Color.White, 0f, Vector2.Zero, scale_, SpriteEffects.None, 0f);
            spriteBatch.Draw(backGroundLayers_[layer], new Vector2(backGroundLayers_[layer].Width * scale_.X - localShift * scale_.X, 0),sourceRectangle2, Color.White, 0f, Vector2.Zero, scale_, SpriteEffects.None, 0f);
        }
    }

    
    public void Update(GameTime gameTime){
        //Console.WriteLine(leftWrapPoint);
        if (hero_.Position.X > rightWrapPoint)
        {
            //tutaj może być problem z zaokrągleniem
            int ds = (int)hero_.Position.X - rightWrapPoint;

            Vector2 pos = hero_.Position;
            pos.X = rightWrapPoint;
            hero_.Position = pos;

            shift += ds;

            // foreach (var obstacle in testList){
            //     if (obstacle == hero_){
            //         continue;
            //     }
            //     obstacle.Position = new Vector2(obstacle.Position.X - ds, obstacle.Position.Y); 
            // }
            foreach (var obstacle in TestList2)
            {
                if (obstacle == hero_)
                {
                    continue;
                }
                obstacle.Position = new Vector2(obstacle.Position.X - ds, obstacle.Position.Y);
            }
        }


        //nie działa :(
        if (hero_.Position.X < leftWrapPoint){
            //tutaj może być problem z zaokrągleniem
            int ds = leftWrapPoint - (int)hero_.Position.X;

            Vector2 pos = hero_.Position;
            pos.X = leftWrapPoint;
            hero_.Position = pos;

            shift -= ds;

            // foreach (var obstacle in testList){
            //     if (obstacle == hero_){
            //         continue;
            //     }
            //     obstacle.Position = new Vector2(obstacle.Position.X + ds, obstacle.Position.Y); 
            // }
            foreach (var obstacle in TestList2){
                if (obstacle == hero_){
                    continue;
                }
                obstacle.Position = new Vector2(obstacle.Position.X + ds, obstacle.Position.Y); 
            }

        }
    }
}