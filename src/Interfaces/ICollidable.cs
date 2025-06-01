using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace MyGame;

// Represents object that can be managed by CollisionManager.cs class
    public interface ICollidable 
    {
        public Vector2 Position { get; set; }
        public int Width { get; }
        public int Height { get; }

    }