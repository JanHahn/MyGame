using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;


public class HeroGenerator
{

    private GraphicsDevice graphicsDevice_;
    public GraphicsDevice GraphicsDevice_{
        set { graphicsDevice_ = value; } 
    }
    public Hero generateHero(){

        Dictionary<string, (Texture2D, int)> acitons = new Dictionary<string, (Texture2D, int)>();;
        acitons.Add("Idle", (Texture2D.FromFile(graphicsDevice_, "/home/jh/my_shit/game_csharp/game/MyGame/Content/HerosSprites/Samurai/Idle.png"), 6));
        acitons.Add("Run", (Texture2D.FromFile(graphicsDevice_, "/home/jh/my_shit/game_csharp/game/MyGame/Content/HerosSprites/Samurai/Run.png"), 8));
        acitons.Add("Jump", (Texture2D.FromFile(graphicsDevice_, "/home/jh/my_shit/game_csharp/game/MyGame/Content/HerosSprites/Samurai/Jump.png"), 9));
        acitons.Add("Attack", (Texture2D.FromFile(graphicsDevice_, "/home/jh/my_shit/game_csharp/game/MyGame/Content/HerosSprites/Samurai/Attack_1.png"), 4));
        
        Hero hero = new Hero(acitons, new Vector2(500, 1080 - 400)); 
        return hero;
    }
        
}
