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
    private int amountOfMoney;
    public int AmountOfMoney
    {
        get { return amountOfMoney; }
    }

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



    public CoinManager(CoinSpriteAnimator spriteAnimator)
    {
        amountOfMoney = 0;
        this.spriteAnimator = spriteAnimator;
    }


    //take all coins from chunk that are loaded currently and substract those coins that palyer already collected 
    public void UpdateCurrentInGameCoins()
    {
        CurrentInGameCoins.Clear();
        foreach (var chunk in loadedChunksReference)
        {
            foreach (var coin in chunk.Coins)
            {
                if (!collectedCoinsById.Contains(coin.Id))
                {
                    CurrentInGameCoins.Add(coin);
                }
            }
        }
    }

    public void CheckIfIntersects(CollisionManager collisionManager, Hero player, List<IPrintable> allDrawable)
{
    for (int i = CurrentInGameCoins.Count - 1; i >= 0; i--)
    {
        var coin = CurrentInGameCoins[i];
        if (collisionManager.CheckSingleIntersection(player, coin))
        {
            amountOfMoney += CollectCoin(coin, allDrawable);
            Console.WriteLine(AmountOfMoney);
        }
    }
}

    //returns worth value of the coin
    private int CollectCoin(Coin coin, List<IPrintable> allDrawable)
    {
        CurrentInGameCoins.Remove(coin);
        allDrawable.Remove(coin);
        collectedCoinsById.Add(coin.Id);
        return coin.Worth;
    }

    public void updateCoinSprites(GameTime gameTime)
    {
        spriteAnimator.Update(gameTime);
    }

}
