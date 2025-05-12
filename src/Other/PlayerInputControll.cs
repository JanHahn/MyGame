using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class PlayerInputControll{

    IPlayerInteractable interactiveObject;


    private Keys moveRight_;
    private Keys moveLeft_;
    private Keys jump_;
    private Keys shot_;
    private Keys interactiveButton_;


    //getters and setters
    public Keys MoveRight
    {
        get { return moveRight_; }
        set { moveRight_ = value; }
    }

    public Keys MoveLeft
    {
        get { return moveLeft_; }
        set { moveLeft_ = value; }
    }

    public Keys Jump
    {
        get { return jump_; }
        set { jump_ = value; }
    }


    private float movementSpeed_ = 10;
    private bool rightButtonHoldFlag;
    private bool leftButtonHoldFlag;



    public PlayerInputControll(){
        moveRight_ = Keys.D;
        moveLeft_ = Keys.A;
        jump_ = Keys.W;
        shot_ = Keys.Enter;
        interactiveButton_ = Keys.E;
    }


    public void checkInput(Hero player, GameTime gameTime, Gravitation gravitation, CollisionManager collisionManager){

        var keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(moveLeft_) && !keyboard.IsKeyDown(moveRight_)){
            player.Position = new Vector2(player.Position.X - movementSpeed_, player.Position.Y);
            collisionManager.RightCorrection(player);
            if (!player.IsFalling && leftButtonHoldFlag){
                //Włączamy sprite aktywny do chodzenia w lewo 
                player.SpriiteAnimator.IsLeft = true;
                player.SpriiteAnimator.ChangeState(HeroActions.Run);
            }
        }
        if (keyboard.IsKeyDown(moveRight_) && !keyboard.IsKeyDown(moveLeft_)){
            player.Position = new Vector2(player.Position.X + movementSpeed_, player.Position.Y);
            collisionManager.LeftCorrection(player);
            if (!player.IsFalling && !rightButtonHoldFlag){
                //Włączamy sprite aktywny do chodzenia w prawo 
                player.SpriiteAnimator.IsLeft = false;
                player.SpriiteAnimator.ChangeState(HeroActions.Run);
            }
        }

        if (keyboard.IsKeyDown(jump_) && player.IsFalling != true){
            player.FallingSpeed = -2000;
            player.IsFalling = true;
            gravitation.Add(player);
            Console.WriteLine("skacze");
            //włączamy aktywny sprite do bycia w powietrzu
        }

        if (keyboard.IsKeyDown(shot_)){
            ;
        }

        // if (keyboard.IsKeyDown(interactiveButton_)){
        //     interactiveObject.execute();
        // }
        
    }
    
}