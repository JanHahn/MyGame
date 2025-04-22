using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public interface IPrintable{

    public void Draw(SpriteBatch spriteBatch);
    public void Update(GameTime gameTime);
}