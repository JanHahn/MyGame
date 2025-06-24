using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;
public class RunningInputState : IPlayerInputState
{
    private bool hasStartedRunning = false;

    public void HandleInput(PlayerInputController context, Hero player, Gravitation gravitation, CollisionManager collisionManager, KeyboardState keyboard)
    {
        bool left = keyboard.IsKeyDown(context.MoveLeft);
        bool right = keyboard.IsKeyDown(context.MoveRight);

        if (keyboard.IsKeyDown(context.Jump) && !player.IsFalling)
        {
            player.FallingSpeed = -2000;
            player.IsFalling = true;
            gravitation.Add(player);
            context.SetState(new JumpingInputState());
            return;
        }

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
        else if (!player.IsFalling)
        {
            context.SetState(new IdleInputState());
            return;
        }

        if (player.IsFalling)
        {
            context.SetState(new JumpingInputState());
            return;
        }

        // Ustaw animację tylko raz przy wejściu do Running
        if (!hasStartedRunning)
        {
            player.SpriiteAnimator.ChangeState(HeroActions.Run);
            hasStartedRunning = true;
        }
    }
}