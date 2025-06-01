using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json;


namespace MyGame;

//represents data deserialised form chunk stored in json file 
public class LayerJsonData
{
    [JsonProperty("data")]
    public List<int> Data { get; set; }



    [JsonProperty("height")]
    public int Rows { get; set; }



    [JsonProperty("width")]
    public int Columns { get; set; }



    [JsonProperty("collidable")]
    public bool IsCollidable { get; set; }


    [JsonProperty("tileSetType")]
    public string TileSetAtlas { get; set; }

}