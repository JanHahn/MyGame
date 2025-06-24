using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class PlayerInputController
{
    private IPlayerInputState currentState;

    public Keys MoveRight { get; set; } = Keys.D;
    public Keys MoveLeft { get; set; } = Keys.A;
    public Keys Jump { get; set; } = Keys.W;
    public Keys Shot { get; set; } = Keys.Enter;
    public Keys InteractiveButton { get; set; } = Keys.E;

    private IPlayerInteractable interactiveObject;

    public PlayerInputController()
    {
        currentState = new IdleInputState();
    }

    public void SetState(IPlayerInputState newState)
    {
        currentState = newState;
    }

    public void CheckInput(Hero player, Gravitation gravitation, CollisionManager collisionManager)
    {
        var keyboard = Keyboard.GetState();

        currentState.HandleInput(this, player, gravitation, collisionManager, keyboard);

        if (keyboard.IsKeyDown(InteractiveButton))
        {
            interactiveObject?.execute();
        }
    }
}
