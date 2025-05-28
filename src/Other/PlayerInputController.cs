using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class PlayerInputController{

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


    private float movementSpeed_ = 12;
    private bool rightButtonHoldFlag;
    private bool leftButtonHoldFlag;
    private bool idleHoldFlag = true;



    public PlayerInputController(){
        moveRight_ = Keys.D;
        moveLeft_ = Keys.A;
        jump_ = Keys.W;
        shot_ = Keys.Enter;
        interactiveButton_ = Keys.E;
    }


    public void checkInput(Hero player, Gravitation gravitation, CollisionManager collisionManager){

        var keyboard = Keyboard.GetState();

        if (player.FallingSpeed != 0) {
            player.SpriiteAnimator.ChangeState(HeroActions.Jump);
        }

        if (keyboard.IsKeyDown(moveLeft_) && !keyboard.IsKeyDown(moveRight_))
        {
            if (!player.IsFalling && !leftButtonHoldFlag)
            {
                //Włączamy sprite aktywny do chodzenia w lewo 
                player.SpriiteAnimator.ChangeState(HeroActions.Run);
                leftButtonHoldFlag = true;
                rightButtonHoldFlag = false;
                idleHoldFlag = false;
            }
            player.SpriiteAnimator.IsLeft = true;
            player.Position = new Vector2(player.Position.X - movementSpeed_, player.Position.Y);
            collisionManager.RightCorrection(player);
        }

        if (keyboard.IsKeyDown(moveRight_) && !keyboard.IsKeyDown(moveLeft_)){
            if (!player.IsFalling && !rightButtonHoldFlag){
                //Włączamy sprite aktywny do chodzenia w prawo 
                player.SpriiteAnimator.ChangeState(HeroActions.Run);
                leftButtonHoldFlag = false;
                idleHoldFlag = false;
                rightButtonHoldFlag = true;
            }
            player.SpriiteAnimator.IsLeft = false;
            player.Position = new Vector2(player.Position.X + movementSpeed_, player.Position.Y);
            collisionManager.LeftCorrection(player);
        }

        if (keyboard.IsKeyDown(jump_) && player.IsFalling != true){
            player.SpriiteAnimator.ChangeState(HeroActions.Jump);
            player.FallingSpeed = -2000;
            player.IsFalling = true;
            gravitation.Add(player);
            //włączamy aktywny sprite do bycia w powietrzu
        }
        if (keyboard.IsKeyDown(shot_)){
            ;
        }

        if (keyboard.IsKeyDown(interactiveButton_)){
            interactiveObject.execute();
        }

        if (player.IsFalling != true && !keyboard.IsKeyDown(moveRight_) && !keyboard.IsKeyDown(moveLeft_) && !idleHoldFlag){
            player.SpriiteAnimator.ChangeState(HeroActions.Idle);
            leftButtonHoldFlag = false;
            rightButtonHoldFlag = false;
            idleHoldFlag = true;
        }
        
    }
    
}