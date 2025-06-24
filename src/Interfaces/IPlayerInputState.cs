using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace MyGame;

public interface IPlayerInputState
{
    void HandleInput(PlayerInputController context, Hero player, Gravitation gravitation, CollisionManager collisionManager, KeyboardState keyboard);
}