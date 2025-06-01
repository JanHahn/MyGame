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

public class Chunk
{
    public int Id { get; private set; }
    public List<Tile> Tiles { get; private set; } = new();
    public List<Coin> Coins { get; set; } = new();

    public Chunk(int id)
    {
        Id = id;
    }

}
