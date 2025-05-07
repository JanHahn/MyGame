using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace MyGame;

// Represents object that can be managed by gravitation.cs class
    public interface IFallingObject : ICollidable
    {
        public float FallingSpeed { get; set; }
        public bool IsFalling { get; set; }

    }