using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;


namespace MyGame;

public static class TextureManager
{
    private static Dictionary<string, Texture2D> textures_ = new();

    public static void LoadAll(ContentManager content)
    {

        textures_["SamuraiArcherIdle"] = content.Load<Texture2D>("HerosSprites/SamuraiArcher/Idle");
        textures_["SamuraiArcherShot"] = content.Load<Texture2D>("HerosSprites/SamuraiArcher/Shot");
        textures_["SamuraiArcherRun"] = content.Load<Texture2D>("HerosSprites/SamuraiArcher/Run");
        textures_["SamuraiArcherJump"] = content.Load<Texture2D>("HerosSprites/SamuraiArcher/Jump");

        textures_["HealthBar"] = content.Load<Texture2D>("HUD/HealthBar");
        textures_["InventoryBar"] = content.Load<Texture2D>("HUD/InventoryBar");
        textures_["CoinIcon"] = content.Load<Texture2D>("HUD/CoinIcon");

        textures_["CastleTiles"] = content.Load<Texture2D>("GameWorld/CastleTiles");
        textures_["GoldCoin1"] = content.Load<Texture2D>("GameWorld/GoldCoin_1");

        textures_["BackGroundAnimated1"] = content.Load<Texture2D>("BackGrounds/animated/1");
        textures_["BackGroundAnimated2"] = content.Load<Texture2D>("BackGrounds/animated/2");
        textures_["BackGroundAnimated3"] = content.Load<Texture2D>("BackGrounds/animated/3");
        textures_["BackGroundAnimated4"] = content.Load<Texture2D>("BackGrounds/animated/4");



    }

    public static Texture2D Get(string key)
    {
        return textures_[key];
    }

    public static void Unload()
    {
        textures_.Clear();
    }
}