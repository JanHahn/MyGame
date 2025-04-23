using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace MyGame;


public class HeroGenerator
{

    private GraphicsDevice graphicsDevice_;
    public GraphicsDevice GraphicsDevice_{
        set { graphicsDevice_ = value; } 
    }
    public Hero generateHero(){

        Dictionary<string, (Texture2D, int)> acitons = new Dictionary<string, (Texture2D, int)>();;
        string contentRoot = AppContext.BaseDirectory;
        string IdlePath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Idle.png");
        string RunPath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Run.png");
        string JumpPath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Jump.png");
        string Attack_1Path = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Attack_1.png");
        acitons.Add("Idle", (Texture2D.FromFile(graphicsDevice_, IdlePath), 9));
        acitons.Add("Run", (Texture2D.FromFile(graphicsDevice_, RunPath), 8));
        acitons.Add("Jump", (Texture2D.FromFile(graphicsDevice_, JumpPath), 9));
        acitons.Add("Attack", (Texture2D.FromFile(graphicsDevice_, Attack_1Path), 4));
        
        Hero hero = new Hero(acitons, new Vector2(500, 1080 - 400)); 
        return hero;
    }
        
}
