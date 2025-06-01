using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;



namespace MyGame;

public class CoinManager
{
    public int AmountOfMoney { get; set; }

    private List<Chunk> loadedChunksReference;
    public List<Chunk> LoadedChunkReference
    {
        set { loadedChunksReference = value; }
    }


    /* CurrentInGameCoins variable is updated form ChunkLoader evry time chunk is loaded. 
    It includes coins that has been collected by the palyer */
    public List<Coin> CurrentInGameCoins { get; set; } = new();
    private List<int> collectedCoinsById = new();


    // In the future could be changed to List<CoinSpriteAnimator> in case we would want to saparate animation for each coin 
    public CoinSpriteAnimator spriteAnimator { get; set; }

    private Hud hudReference;


    public CoinManager(CoinSpriteAnimator spriteAnimator)
    {
        AmountOfMoney = 0;
        this.spriteAnimator = spriteAnimator;
    }


    //take all coins from chunk that are loaded currently and substract those coins that palyer already collected 
    public void UpdateCurrentInGameCoins()
    {
        foreach (var chunk in loadedChunksReference)
        {
            foreach (var coin in chunk.Coins)
            {
                if (!collectedCoinsById.Contains(coin.Id)) {
                    CurrentInGameCoins.Add(coin);
                }
            }
        }
    }

    // bool CheckIfIntersects(CollisionManager collisionManager,  Hero player)
    // {

    // }

    private void CollectCoin()
    {

    }

    public void updateCoinSprites(GameTime gameTime)
    {
        spriteAnimator.Update(gameTime);
    }

}
