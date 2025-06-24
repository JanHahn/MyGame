using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;


public class IdleInputState : IPlayerInputState
{
    public void HandleInput(PlayerInputController context, Hero player, Gravitation gravitation, CollisionManager collisionManager, KeyboardState keyboard)
    {
        bool left = keyboard.IsKeyDown(context.MoveLeft);
        bool right = keyboard.IsKeyDown(context.MoveRight);

        if (left || right)
        {
            context.SetState(new RunningInputState());
            return;
        }

        if (keyboard.IsKeyDown(context.Jump) && !player.IsFalling)
        {
            player.FallingSpeed = -2000;
            player.IsFalling = true;
            gravitation.Add(player);
            context.SetState(new JumpingInputState());
            return;
        }

        player.SpriiteAnimator.ChangeState(HeroActions.Idle);
    }
}