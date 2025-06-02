using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;


namespace MyGame;

public class GameServices
{
    public CollisionManager CollisionManager { get; set; }
    public List<IPrintable> GlobalDrawables { get; set; }
    public Gravitation gravitation { get; set; }
    public MapSlider MapSlider { get; set; }
    public CoinManager CoinManager { get; set; }
}

public class Game1 : Game
{
    private GraphicsDeviceManager graphics_;
    private SpriteBatch spriteBatch_;


    private PlayerInputController inputControll_;
    private PlayerInputController inputControll_2;


    //printable Things
    private Hero myHero_;
    private Hero myHero_2;
    private Hud hud_;


    private GameServices gameServices;
    private ChunkLoader chunkLoader;


    public Game1()
    {
        graphics_ = new GraphicsDeviceManager(this);
        graphics_.IsFullScreen = true;
        graphics_.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics_.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        graphics_.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }


    protected override void LoadContent()
    {
        spriteBatch_ = new SpriteBatch(GraphicsDevice);
        TextureManager.LoadAll(this.Content);


        HerosSprites herosSprites = new HerosSprites();
        herosSprites.hitBoxHeight = 73;
        herosSprites.hitBoxWidth = 40;

        herosSprites.idleSprite.spriteSheet = TextureManager.Get("SamuraiArcherIdle");
        herosSprites.idleSprite.framesQuantity = 9;
        herosSprites.idleSprite.timeInterval = 0.1f;
        herosSprites.idleSprite.X_OffSet = 9;
        herosSprites.idleSprite.Y_OffSet = 30;

        herosSprites.shotSprite.spriteSheet = TextureManager.Get("SamuraiArcherShot");
        herosSprites.shotSprite.framesQuantity = 9;
        herosSprites.shotSprite.timeInterval = 0.1f;
        herosSprites.shotSprite.X_OffSet = 26;
        herosSprites.shotSprite.Y_OffSet = 0;

        herosSprites.runSprite.spriteSheet = TextureManager.Get("SamuraiArcherRun");
        herosSprites.runSprite.framesQuantity = 8;
        herosSprites.runSprite.timeInterval = 0.1f;
        herosSprites.runSprite.X_OffSet = 26;
        herosSprites.runSprite.Y_OffSet = 0;

        herosSprites.jumpSprite.spriteSheet = TextureManager.Get("SamuraiArcherJump");
        herosSprites.jumpSprite.framesQuantity = 9;
        herosSprites.jumpSprite.timeInterval = 0.1f;
        herosSprites.jumpSprite.X_OffSet = 27;
        herosSprites.jumpSprite.Y_OffSet = 0;

        //Hero 1
        inputControll_ = new PlayerInputController();
        HeroSpriiteAnimator heroSpriiteAnimator = new HeroSpriiteAnimator(GraphicsDevice, herosSprites);
        myHero_ = new Hero(heroSpriiteAnimator);
        myHero_.Position = new Vector2(600, 0);


        //Hero 2
        HeroSpriiteAnimator hero2SpriiteAnimator = new HeroSpriiteAnimator(GraphicsDevice, herosSprites);
        myHero_2 = new Hero(hero2SpriiteAnimator);
        myHero_2.Position = new Vector2(300, 500);


        //Inputs Control for hero 2
        inputControll_2 = new PlayerInputController();
        inputControll_2.MoveLeft = Keys.Left;
        inputControll_2.MoveRight = Keys.Right;
        inputControll_2.Jump = Keys.Up;


        gameServices = new GameServices();
        gameServices.GlobalDrawables = [myHero_2];

        gameServices.CollisionManager = new CollisionManager();
        gameServices.CollisionManager.Add(myHero_);
        gameServices.CollisionManager.Add(myHero_2);

        gameServices.MapSlider = new MapSlider(myHero_, graphics_.PreferredBackBufferWidth, graphics_.PreferredBackBufferHeight, chunkLoader);
        gameServices.MapSlider.TestList = gameServices.CollisionManager.CollidableObjects;
        gameServices.MapSlider.MapWorldObjects = gameServices.GlobalDrawables;

        gameServices.gravitation = new Gravitation(gameServices.CollisionManager);



        //Sprite for coins
        CoinSprite coinSprite_1 = new CoinSprite();
        coinSprite_1.framesQuantity = 10;
        coinSprite_1.spriteSheet = TextureManager.Get("GoldCoin1");
        coinSprite_1.timeInterval = 0.05f;
        coinSprite_1.X_OffSet = 0;
        coinSprite_1.Y_OffSet = 0;

        CoinSpriteAnimator coinAnimator = new CoinSpriteAnimator(GraphicsDevice);
        coinAnimator.SetAnimation(coinSprite_1);

        gameServices.CoinManager = new CoinManager(coinAnimator);


        //Hud
        hud_ = new Hud(GraphicsDevice, gameServices);

        //THIS IS THE MOMENT WHERE WE LOAD WHOLE WORLD!!! 
        chunkLoader = new ChunkLoader(gameServices, GraphicsDevice);

        chunkLoader.WorldInit();
        gameServices.MapSlider.ChunkLoader = chunkLoader;
        
    }


    protected override void Update(GameTime gameTime)
    {
        gameServices.MapSlider.Update(gameTime);
        inputControll_.checkInput(myHero_, gameServices.gravitation, gameServices.CollisionManager);
        inputControll_2.checkInput(myHero_2, gameServices.gravitation, gameServices.CollisionManager);
        //GameWorld.update(); -> zrobić coś takiego rzeby było czytelniej tutaj
        gameServices.gravitation.Update(gameTime);
        myHero_.SpriiteAnimator.Update(gameTime);
        myHero_2.SpriiteAnimator.Update(gameTime);
        gameServices.CoinManager.updateCoinSprites(gameTime);
        gameServices.CoinManager.CheckIfIntersects(gameServices.CollisionManager, myHero_, gameServices.GlobalDrawables);
        hud_.GetHealthBar.TestUpdate(gameTime);
        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch_.Begin();
        

        gameServices.MapSlider.Draw(spriteBatch_);
        foreach (IPrintable gameObject in gameServices.GlobalDrawables)
        {
            gameObject.Draw(spriteBatch_);
        }
        myHero_.Draw(spriteBatch_);
        hud_.Draw(spriteBatch_);


        spriteBatch_.End();
        base.Draw(gameTime);
    }
}
