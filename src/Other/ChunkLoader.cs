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

public class ChunkLoader
{

    private const int CHUNK_LOAD_NUMBER = 2;
    private const int MAP_CHUNK_QUANTITY = 7;
    private int playerCurrentChunk = 1;


    private List<Chunk> loadedChunks = new();

    private GraphicsDevice graphicsDevice;

    private int chunkWidth;
    public int ChunkWidth
    {
        get { return chunkWidth; }
        set { chunkWidth = value; }
    }

    private string pathToChunksInfoDir;


    //reference to managers in gameWorld
    private GameServices gameServices;


    public ChunkLoader(GameServices gameServices, GraphicsDevice graphicsDevice)
    {
        this.gameServices = gameServices;
        this.graphicsDevice = graphicsDevice;

        gameServices.CoinManager.LoadedChunkReference = this.loadedChunks;

        ChunkWidth = 960;
        pathToChunksInfoDir = "C:\\Users\\janek\\my_shit\\firstGame\\MyGame\\Content\\MapChunks";
    }


    private Texture2D GetTilesetAtlas(LayerJsonData layer)
    {
        Texture2D TileStruct = TextureManager.Get(layer.TileSetAtlas);
        return TileStruct;
    }


    private string GetChunkPath(int chunkNumber)
    {
        return pathToChunksInfoDir + "\\Chunk" + chunkNumber.ToString() + ".json";
    }


    private List<LayerJsonData> GetLayers(int chunkNumber)
    {
        string chunkPath = GetChunkPath(chunkNumber);

        string json = File.ReadAllText(chunkPath);
        JObject root = JObject.Parse(json);
        JArray layersArray = (JArray)root["layers"];

        // Deserializujemy warstwy kafelków
        return layersArray.ToObject<List<LayerJsonData>>();
    }


    void LoadChunk(int chunkNumber, int worldShift)
    {
        Console.WriteLine("ChunkLoaded" + chunkNumber);
        string chunkPath = GetChunkPath(chunkNumber);

        string json = File.ReadAllText(chunkPath);
        JObject root = JObject.Parse(json);

        Chunk newChunk = new Chunk(chunkNumber);


        if (root.ContainsKey("layers"))
        {
            List<LayerJsonData> layers = GetLayers(chunkNumber);
            foreach (var layer in layers)
            {
                int localTail = 0;
                foreach (int tailNumber in layer.Data)
                {
                    if (tailNumber == 0)
                    {
                        localTail++;
                        continue;
                    }
                    //Rectangle srcRect = new Rectangle(tailNumber % 4 * 48, tailNumber / 4 * 48, 48, 48);
                    Vector2 tileInitPosition = new Vector2((localTail % layer.Columns * 48 + (chunkNumber - 1) * chunkWidth) - worldShift, localTail / layer.Columns * 48 - 227);
                    Tile newTile = new Tile(tileInitPosition, layer.TileSetAtlas, tailNumber - 1);
                    if (layer.IsCollidable == true)
                    {
                        gameServices.CollisionManager.Add(newTile);
                    }
                    // dodaj do printable all
                    gameServices.GlobalDrawables.Add(newTile);
                    localTail++;
                }
            }
        }

        if (root.ContainsKey("coins"))
        {
            JObject coinsObject = (JObject)root["coins"];
            int coinId = coinsObject["coinBaseId"]?.Value<int>() ?? 0;

            JObject coinsEntities = (JObject)coinsObject["entities"];

            foreach (var prop in coinsEntities.Properties())
            {
                int coinValue = int.Parse(prop.Name); // "1", "5", "10" → 1, 5, 10

                JArray positionsArray = (JArray)prop.Value;

                foreach (var pos in positionsArray)
                {
                    int x = pos["x"].Value<int>();
                    int y = pos["y"].Value<int>();
                    Vector2 coinPosition = new Vector2(x, y);
                    
                    Console.WriteLine(coinPosition);
                    Console.WriteLine(coinId);
                    Console.WriteLine(coinValue);

                    Coin newCoin = new Coin(coinPosition, coinValue, coinId, gameServices.CoinManager.spriteAnimator);

                    newChunk.Coins.Add(newCoin);
                    gameServices.GlobalDrawables.Add(newCoin);
                    coinId++;
                }
            }
        }

        if (root["enemies"] is JArray enemiesArray)
        {
            foreach (JObject enemy in enemiesArray)
            {
                string type = enemy["type"].Value<string>();
                int x = enemy["x"].Value<int>();
                int y = enemy["y"].Value<int>();
            }
        }

        loadedChunks.Add(newChunk);
        gameServices.CoinManager?.UpdateCurrentInGameCoins();
    }


    public bool CheckForChunkLoad(int playerWorldCoordinate_X, int worldShift)
    {
        if (playerWorldCoordinate_X > playerCurrentChunk * chunkWidth && playerCurrentChunk <= MAP_CHUNK_QUANTITY - CHUNK_LOAD_NUMBER) 
        {
            Console.WriteLine("playerCurrentChunk:" + playerCurrentChunk);
            //game should load next chunk
            LoadChunk(playerCurrentChunk + CHUNK_LOAD_NUMBER, worldShift);
            playerCurrentChunk++;
            return true;
        }

        // this one should return true if player going left and some chunk were deloaded berfore
        if (playerWorldCoordinate_X - playerCurrentChunk * chunkWidth > playerCurrentChunk * chunkWidth && playerCurrentChunk <= MAP_CHUNK_QUANTITY - CHUNK_LOAD_NUMBER)
        {
            return true;
        }
        return false;
    }

    public void WorldInit()
    {
        for (int i = 1; i <= CHUNK_LOAD_NUMBER; i++)
        {
            LoadChunk(i, 0);
        }
    }
    

}