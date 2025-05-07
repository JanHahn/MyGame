using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace MyGame;

// Classes that inharites form that class implements function that will be execiuted after pressing interactive button -> see more in PlayerInputControll.cs
    public interface IPlayerInteractable
    {
        public void execute();
    }