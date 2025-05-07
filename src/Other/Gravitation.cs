using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Security.AccessControl;
using System.ComponentModel;


namespace MyGame;

//Class stores all moveable objects thats are flying at that moment and pulling them to the ground
//List of objects in this class is updated continuously during the game

public class Gravitation{

    const float GROUND_LEVEL = 850f; // <- dodajemy tę stałą
    const float ACCELERATION = 6000;

    

    private List<IFallingObject> currentlyInAir_; //stores objects position and currnet speed
    public List<IFallingObject> CurrentlyInAir {
        set { currentlyInAir_ = value; }
    }

    private List<(IFallingObject, ICollidable)> connectedObjects;
    private CollisionManager collisionManager_;


    public Gravitation(CollisionManager collisionManager){
        currentlyInAir_ = new List<IFallingObject>();
        connectedObjects = new List<(IFallingObject, ICollidable)>();
        collisionManager_ = collisionManager;
    }


    public void Add(IFallingObject fallingObject){
        //dodać obsługę że podczas dodawania sprawdza czy istnieje już w liście
        if (!currentlyInAir_.Contains(fallingObject)){
            currentlyInAir_.Add(fallingObject);
        }
        
    }

    public void Remove(IFallingObject fallingObject){
        currentlyInAir_.Remove(fallingObject);
    }


    void updatePosiiton(GameTime gameTime){

        List<IFallingObject> toRemove = new List<IFallingObject>();

        Console.WriteLine(currentlyInAir_.Count);
        foreach (IFallingObject fallingObject in currentlyInAir_){
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updatedSpeed = fallingObject.FallingSpeed + (deltaTime*ACCELERATION);
            fallingObject.FallingSpeed = updatedSpeed;


            float deltaDistance = deltaTime * fallingObject.FallingSpeed; // if spped is positive number then object is moving down;
            float updatedPositionY = fallingObject.Position.Y + deltaDistance;
            

            fallingObject.Position = new Vector2(fallingObject.Position.X, updatedPositionY);//-= fallingObject.FallingSpeed*deltaTime;
            

            
            if (fallingObject.FallingSpeed > 0){
                (bool, ICollidable) var = collisionManager_.TopCorrection(fallingObject);
                if (var.Item1){
                    Console.WriteLine(fallingObject.FallingSpeed);
                    toRemove.Add(fallingObject);
                    fallingObject.FallingSpeed = 0;
                    fallingObject.IsFalling = false;
                    connectedObjects.Add((fallingObject ,var.Item2));
                    continue;    
                }
            }

            else if (fallingObject.FallingSpeed < 0) {
                if (collisionManager_.BottomCorrection(fallingObject)){
                    Console.WriteLine(fallingObject.FallingSpeed);
                    fallingObject.FallingSpeed = 0;
                    continue;
                }
            }
            
            if (fallingObject.Position.Y > GROUND_LEVEL){
                fallingObject.Position = new Vector2(fallingObject.Position.X, GROUND_LEVEL);
                toRemove.Add(fallingObject);
            }
        }
        foreach (var obj in toRemove){
            currentlyInAir_.Remove(obj);
            obj.IsFalling = false;
        }
    }

    public void CheckIfStandingOnEachOther(){

        List<(IFallingObject, ICollidable)> toRemove = new List<(IFallingObject, ICollidable)>();

        //to sie wypierdoli bo usówasz z listy którą przeglądasz obecnie
        foreach ((IFallingObject, ICollidable) objects in connectedObjects){
            IFallingObject fallingObj = objects.Item1;
            ICollidable CollisionObj = objects.Item2;
            if (fallingObj.Position.X + CollisionObj.Width < CollisionObj.Position.X || fallingObj.Position.X > CollisionObj.Position.X + CollisionObj.Width || fallingObj.Position.Y + fallingObj.Height < CollisionObj.Position.Y){
                this.Add(fallingObj);
                toRemove.Add(objects);
            }
        }
        foreach (var obj in toRemove){
            connectedObjects.Remove(obj);
        }
    }

    public void Update(GameTime gameTime){
        updatePosiiton(gameTime);
        CheckIfStandingOnEachOther();
    }

}