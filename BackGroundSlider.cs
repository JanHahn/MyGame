using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MyGame;

public class BackGroundSlider: IPrintable{

    private List<Texture2D> backGroundLayers_;
    private List<float> backGroundSpeedFactors_;
    private Vector2 playerPosition_;

    public BackGroundSlider(List<Texture2D> backGroundLayers, List<float> backGroundSpeedFactors, Vector2 playerPosition){
        backGroundLayers_ = backGroundLayers;
        backGroundSpeedFactors_ =  backGroundSpeedFactors;
        playerPosition_ = playerPosition;

        for (int i = 0; i < backGroundLayers.Count; i++){
            SlideRingPoint.Add(0);
        }
    }

    //indicates the place where the background wraps
    private List<int> SlideRingPoint;

    public void Draw(SpriteBatch spriteBatch){
        ;
    }
    public void Update(GameTime gameTime){
        ;
    }



}