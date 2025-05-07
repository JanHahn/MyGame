using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace MyGame;

public class Engine {

    //rendering things
    private GraphicsDeviceManager graphics_;
    private SpriteBatch spriteBatch_;
    private GraphicsDevice graphicsDevice_;


    private PlayerInputControll inputControll_;
    private PlayerInputControll inputControll_2;


    //printable Things
    private Hero myHero_;
    private Hero myHero_2;
    private BackGroundSlider backGroundSlider_;
    private Texture2D obstacle_; //for tests


    //Stores things to be diplayed at the moment (they are in screen range)
    private List<IPrintable> printableInRange_;
    private List<IPrintable> printableAll_;


    // Environment tools
    Gravitation gravitation_;
    CollisionManager collisionManager_;


    public Engine(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice){

        graphics_ = graphics;
        spriteBatch_ = spriteBatch;
        graphicsDevice_ = graphicsDevice;


        //Hero 1
        inputControll_ = new PlayerInputControll();

        HeroGenerator heroGenerator = new HeroGenerator();
        heroGenerator.GraphicsDevice_ = graphicsDevice;
        myHero_ = heroGenerator.generateHero();
        myHero_.Position = new Vector2(600, 0);


        //Hero 2
        myHero_2 = heroGenerator.generateHero();
        myHero_2.Position = new Vector2(300, 500);
        string contentRoot = AppContext.BaseDirectory;
        //string RunPath = Path.Combine(contentRoot, "Content/HerosSprites/kalamarnica.png");
        //Texture2D IdleTexture = Texture2D.FromFile(graphicsDevice_, RunPath);
        //myHero_2.ActiveTexture = IdleTexture;




        inputControll_2 = new PlayerInputControll();
        inputControll_2.MoveLeft = Keys.Left;
        inputControll_2.MoveRight = Keys.Right;
        inputControll_2.Jump = Keys.Up;


        Texture2D backgroundTexture;
        Texture2D backgroundTexture2;
        Texture2D backgroundTexture3;
        Texture2D backgroundTexture4;
        //string contentRoot = AppContext.BaseDirectory;
        string backgroundPath1 = Path.Combine(contentRoot, "Content/BackGrounds/animated/1.png");
        string backgroundPath2 = Path.Combine(contentRoot, "Content/BackGrounds/animated/2.png");
        string backgroundPath3 = Path.Combine(contentRoot, "Content/BackGrounds/animated/3.png");
        string backgroundPath4 = Path.Combine(contentRoot, "Content/BackGrounds/animated/4.png");
        backgroundTexture = Texture2D.FromFile(graphicsDevice, backgroundPath1);
        backgroundTexture2 = Texture2D.FromFile(graphicsDevice, backgroundPath2);
        backgroundTexture3 = Texture2D.FromFile(graphicsDevice, backgroundPath3);
        backgroundTexture4 = Texture2D.FromFile(graphicsDevice, backgroundPath4);
        List<Texture2D> backGroundLayers = new List<Texture2D>();
        backGroundLayers.Add(backgroundTexture);
        backGroundLayers.Add(backgroundTexture2);
        backGroundLayers.Add(backgroundTexture3);
        backGroundLayers.Add(backgroundTexture4);
        List<float> speedFactors = new List<float>();
        speedFactors.Add(1);
        speedFactors.Add(0.05f);
        speedFactors.Add(0.12f);
        speedFactors.Add(0.5f);
        Vector2 backgroundScale = new Vector2(
        (float)graphics_.PreferredBackBufferWidth / backgroundTexture.Width,
        (float)graphics_.PreferredBackBufferHeight / backgroundTexture.Height);
        backGroundSlider_ = new BackGroundSlider(backGroundLayers, speedFactors, myHero_, graphics_.PreferredBackBufferWidth, graphics_.PreferredBackBufferHeight, backgroundScale);

        collisionManager_ = new CollisionManager();
        collisionManager_.Add(myHero_);
        collisionManager_.Add(myHero_2);

        gravitation_ = new Gravitation(collisionManager_);


        //printableInRange_.Add(myHero_);
    }


    public void Update(GameTime gameTime){
        backGroundSlider_.Update(gameTime);
        inputControll_.checkInput(myHero_, gameTime, gravitation_, collisionManager_);
        inputControll_2.checkInput(myHero_2, gameTime, gravitation_, collisionManager_);
        gravitation_.Update(gameTime);
    }


    public void Draw(GameTime gameTime){
        backGroundSlider_.Draw(spriteBatch_);
        // foreach (IPrintable gameObject in printableInRange_){
        //     gameObject.Draw(spriteBatch_);
        // }
        myHero_.Draw(spriteBatch_);
        myHero_2.Draw(spriteBatch_);
    }

}