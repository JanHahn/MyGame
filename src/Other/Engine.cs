using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace MyGame;

public class Engine
{

    //rendering things
    private GraphicsDeviceManager graphics_;
    private SpriteBatch spriteBatch_;
    private GraphicsDevice graphicsDevice_;


    private PlayerInputController inputControll_;
    private PlayerInputController inputControll_2;


    //printable Things
    private Hero myHero_;
    private Hero myHero_2;
    private BackGroundSlider backGroundSlider_;
    private Hud hud_;


    //Stores things to be diplayed at the moment (they are in screen range)
    private List<IPrintable> printableInRange_;
    private List<IPrintable> printableAll_;


    // Environment tools
    Gravitation gravitation_;
    CollisionManager collisionManager_;


    public Engine(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {

        graphics_ = graphics;
        spriteBatch_ = spriteBatch;
        graphicsDevice_ = graphicsDevice;


        //List things to be printed
        printableAll_ = new List<IPrintable>();

        //Heros Paths
        string contentRoot = AppContext.BaseDirectory;
        string IdlePath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Idle.png");
        string RunPath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Run.png");
        string JumpPath = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Jump.png");
        string Attack_1Path = Path.Combine(contentRoot, "Content/HerosSprites/SamuraiArcher/Arrow.png");

        HerosSprites herosSprites = new HerosSprites();
        herosSprites.hitBoxHeight = 73;
        herosSprites.hitBoxWidth = 40;

        herosSprites.idleSprite.spriteSheet = Texture2D.FromFile(graphicsDevice_, IdlePath);
        herosSprites.idleSprite.framesQuantity = 9;
        herosSprites.idleSprite.timeInterval = 0.1f;
        herosSprites.idleSprite.X_OffSet = 9;
        herosSprites.idleSprite.Y_OffSet = 30;

        herosSprites.shotSprite.spriteSheet = Texture2D.FromFile(graphicsDevice_, Attack_1Path);
        herosSprites.shotSprite.framesQuantity = 9;
        herosSprites.shotSprite.timeInterval = 0.1f;
        herosSprites.shotSprite.X_OffSet = 26;
        herosSprites.shotSprite.Y_OffSet = 0;

        herosSprites.runSprite.spriteSheet = Texture2D.FromFile(graphicsDevice_, RunPath);
        herosSprites.runSprite.framesQuantity = 8;
        herosSprites.runSprite.timeInterval = 0.1f;
        herosSprites.runSprite.X_OffSet = 26;
        herosSprites.runSprite.Y_OffSet = 0;

        herosSprites.jumpSprite.spriteSheet = Texture2D.FromFile(graphicsDevice_, JumpPath);
        herosSprites.jumpSprite.framesQuantity = 9;
        herosSprites.jumpSprite.timeInterval = 0.1f;
        herosSprites.jumpSprite.X_OffSet = 27;
        herosSprites.jumpSprite.Y_OffSet = 0;


        //Hero 1
        inputControll_ = new PlayerInputController();
        HeroSpriiteAnimator heroSpriiteAnimator = new HeroSpriiteAnimator(graphicsDevice_, herosSprites);
        myHero_ = new Hero(heroSpriiteAnimator);
        myHero_.Position = new Vector2(600, 0);


        //Hero 2
        HeroSpriiteAnimator hero2SpriiteAnimator = new HeroSpriiteAnimator(graphicsDevice_, herosSprites);
        myHero_2 = new Hero(hero2SpriiteAnimator);
        myHero_2.Position = new Vector2(300, 500);


        //Inputs Control for hero 2
        inputControll_2 = new PlayerInputController();
        inputControll_2.MoveLeft = Keys.Left;
        inputControll_2.MoveRight = Keys.Right;
        inputControll_2.Jump = Keys.Up;



        //Background slider
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
        List<float> speedFactors = [1, 0.05f, 0.12f, 0.35f];
        Vector2 backgroundScale = new Vector2(
        (float)graphics_.PreferredBackBufferWidth / backgroundTexture.Width,
        (float)graphics_.PreferredBackBufferHeight / backgroundTexture.Height);
        backGroundSlider_ = new BackGroundSlider(backGroundLayers, speedFactors, myHero_, graphics_.PreferredBackBufferWidth, graphics_.PreferredBackBufferHeight, backgroundScale);


        collisionManager_ = new CollisionManager();
        collisionManager_.Add(myHero_);
        collisionManager_.Add(myHero_2);

        backGroundSlider_.TestList = collisionManager_.CollidableObjects;
        backGroundSlider_.TestList2 = printableAll_;

        gravitation_ = new Gravitation(collisionManager_);


        //Castle
        Castle myCastle = new Castle(collisionManager_, printableAll_, graphicsDevice_);

        hud_ = new Hud(100, graphicsDevice);


        //printableInRange_.Add(myCastle);
    }


    public void Update(GameTime gameTime)
    {
        backGroundSlider_.Update(gameTime);
        inputControll_.checkInput(myHero_, gravitation_, collisionManager_);
        inputControll_2.checkInput(myHero_2, gravitation_, collisionManager_);
        //GameWorld.update(); -> zrobić coś takiego rzeby było czytelniej tutaj
        gravitation_.Update(gameTime);
        myHero_.SpriiteAnimator.Update(gameTime);
        myHero_2.SpriiteAnimator.Update(gameTime);
        hud_.TestUpdate(gameTime);
    }


    public void Draw(GameTime gameTime)
    {
        backGroundSlider_.Draw(spriteBatch_);
        foreach (IPrintable gameObject in printableAll_)
        {
            gameObject.Draw(spriteBatch_);
        }
        myHero_.Draw(spriteBatch_);
        myHero_2.Draw(spriteBatch_);
        hud_.Draw(spriteBatch_);
    }
    
}