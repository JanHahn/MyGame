using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class JumpingInputState : IPlayerInputState
{
    public void HandleInput(PlayerInputController context, Hero player, Gravitation gravitation, CollisionManager collisionManager, KeyboardState keyboard)
    {
        bool left = keyboard.IsKeyDown(context.MoveLeft);
        bool right = keyboard.IsKeyDown(context.MoveRight);

        if (left && !right)
        {
            player.SpriiteAnimator.IsLeft = true;
            player.Position = new Vector2(player.Position.X - player.MovementSpeed, player.Position.Y);
            player.WorldCoordinates = new Vector2(player.WorldCoordinates.X - player.MovementSpeed, player.Position.Y);
            collisionManager.RightCorrection(player);
        }
        else if (right && !left)
        {
            player.SpriiteAnimator.IsLeft = false;
            player.Position = new Vector2(player.Position.X + player.MovementSpeed, player.Position.Y);
            player.WorldCoordinates = new Vector2(player.WorldCoordinates.X + player.MovementSpeed, player.Position.Y);
            collisionManager.LeftCorrection(player);
        }

        player.SpriiteAnimator.ChangeState(HeroActions.Jump);

        // Wylądował i nic nie wciska → Idle
        if (!player.IsFalling && !left && !right)
        {
            context.SetState(new IdleInputState());
        }
        // Wylądował i wciska → Running
        else if (!player.IsFalling && (left || right))
        {
            context.SetState(new RunningInputState());
        }
    }
}