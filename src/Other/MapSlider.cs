using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MyGame;


// class is responsible for moving every game object imitating players movement in the world, also drawing background in the paralax effect. 
// Stores shift that representing position of the player in the world.
public class MapSlider : IPrintable
{

    public Vector2 Position { get; set; }

    private List<Texture2D> backGroundLayers_;
    private List<float> backGroundSpeedFactors_;

    private Vector2 scale_;
    private int screenWidth_;
    private int screenHeight_;


    private Hero hero_;
    private int shift;
    private int rightWrapPoint;
    private int leftWrapPoint;


    public List<IPrintable> MapWorldObjects { get; set; }

    public ChunkLoader ChunkLoader { get; set; }


    public MapSlider(Hero hero, int screenWidth, int screenHeight, ChunkLoader chunkLoader)
    {

        Position = new Vector2(0, 0);
        shift = 0;

        this.hero_ = hero;
        this.screenHeight_ = screenHeight;
        this.screenWidth_ = screenWidth;
        this.ChunkLoader = chunkLoader;

        rightWrapPoint = (int)Math.Round(screenWidth_ * 0.60);
        leftWrapPoint = (int)(Math.Round(screenWidth_ * 0.05) - hero_.Width);

        backGroundLayers_ =
        [
            TextureManager.Get("BackGroundAnimated1"),
            TextureManager.Get("BackGroundAnimated2"),
            TextureManager.Get("BackGroundAnimated3"),
            TextureManager.Get("BackGroundAnimated4"),
        ];
        backGroundSpeedFactors_ =
        [
            1,
            0.05f,
            0.12f,
            0.35f
        ];

        scale_ = new Vector2(
           (float)screenWidth / backGroundLayers_[0].Width,
           (float)screenHeight / backGroundLayers_[0].Height
        );
    }


    public void Draw(SpriteBatch spriteBatch)
    {


        for (int layer = 0; layer < backGroundLayers_.Count; layer++)
        {

            //this shift is based on speedfactors of each background            
            int localShift = (int)(shift * backGroundSpeedFactors_[layer]) % backGroundLayers_[layer].Width;

            //animation of each background
            Rectangle sourceRectangle = new Rectangle(localShift, 0, backGroundLayers_[layer].Width - localShift, backGroundLayers_[layer].Height);
            Rectangle sourceRectangle2 = new Rectangle(0, 0, localShift, backGroundLayers_[layer].Height);
            spriteBatch.Draw(backGroundLayers_[layer], new Vector2(0, 0), sourceRectangle, Color.White, 0f, Vector2.Zero, scale_, SpriteEffects.None, 0f);
            spriteBatch.Draw(backGroundLayers_[layer], new Vector2(backGroundLayers_[layer].Width * scale_.X - localShift * scale_.X, 0), sourceRectangle2, Color.White, 0f, Vector2.Zero, scale_, SpriteEffects.None, 0f);
        }
    }


    public void Update(GameTime gameTime)
    {
        if (hero_.Position.X > rightWrapPoint)
        {
            int deltaPx = (int)hero_.Position.X - rightWrapPoint;

            Vector2 pos = hero_.Position;
            pos.X = rightWrapPoint;
            hero_.Position = pos;

            // Vector2 worldCoordinates = hero_.WorldCoordinates;
            // worldCoordinates.X += deltaPx;
            // hero_.WorldCoordinates = worldCoordinates;
            
            shift += deltaPx;
            ChunkLoader.CheckForChunkLoad((int)hero_.WorldCoordinates.X, shift);
 
            foreach (var obstacle in MapWorldObjects)
            {
                if (obstacle == hero_)
                {
                    continue;
                }
                obstacle.Position = new Vector2(obstacle.Position.X - deltaPx, obstacle.Position.Y);
            }
        }


        if (hero_.Position.X < leftWrapPoint)
        {
            int deltaPx = leftWrapPoint - (int)hero_.Position.X;

            Vector2 pos = hero_.Position;
            pos.X = leftWrapPoint;
            hero_.Position = pos;

            // Vector2 worldCoordinates = hero_.WorldCoordinates;
            // worldCoordinates.X -= deltaPx;
            // hero_.WorldCoordinates = worldCoordinates;

            if (shift - deltaPx > 0)
            {
                shift -= deltaPx;
                foreach (var obstacle in MapWorldObjects)
                {
                    if (obstacle == hero_)
                    {
                        continue;
                    }
                    obstacle.Position = new Vector2(obstacle.Position.X + deltaPx, obstacle.Position.Y);
                }

            }
        }
    }
}