using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml.Serialization;
using System.Linq;


namespace MyGame;

public class CollisionManager {
    

    //Stores all objects that can collide with each other
    private List<ICollidable> collidableObjects_;

    public List<ICollidable> CollidableObjects{
        get { return collidableObjects_; }
        set { collidableObjects_ = value; }
    }


    public CollisionManager(){
        collidableObjects_ = new List<ICollidable>();
    }

    public void Add(ICollidable collidable){
        collidableObjects_.Add(collidable);
    }


    public bool CheckSingleIntersection(ICollidable movebleObject, ICollidable staticObject){
        Rectangle MoveHitBox = new Rectangle((int)movebleObject.Position.X, (int)movebleObject.Position.Y, movebleObject.Width, movebleObject.Height);
        Rectangle StaticHitBox = new Rectangle((int)staticObject.Position.X, (int)staticObject.Position.Y, staticObject.Width, staticObject.Height);
        return MoveHitBox.Intersects(StaticHitBox);
    }

    public List<ICollidable> GetAllIntersections(ICollidable movebleObject){
        List<ICollidable> intersectionList = new List<ICollidable>();
        foreach (ICollidable collidableObject in collidableObjects_) {
            if (collidableObject == movebleObject){
                continue;
            }
            if (CheckSingleIntersection(movebleObject, collidableObject)){
                intersectionList.Add(collidableObject);
            }
        }
        return intersectionList;
    }

    // Function correct moveable object to the left side of the object that has collision with,
    // returns true if the collision occured.
    // All four function below must be executed after every change of moveable object's Vector
    public bool LeftCorrection(ICollidable movebleObject){
        List<ICollidable> collisionOccuredObjects = GetAllIntersections(movebleObject);
        if (collisionOccuredObjects.Count != 0){
            List<float> objects_X_Posiitons = new List<float>();
            foreach (ICollidable collisionOccuredObject in collisionOccuredObjects){
                objects_X_Posiitons.Add(collisionOccuredObject.Position.X); 
            }
            movebleObject.Position = new Vector2(objects_X_Posiitons.Min() - movebleObject.Width, movebleObject.Position.Y);
            return true;
        }
        return false;
    }

    public bool RightCorrection(ICollidable movebleObject){
        List<ICollidable> collisionOccuredObjects = GetAllIntersections(movebleObject);
        if (collisionOccuredObjects.Count != 0){
            ICollidable return_collidable = collisionOccuredObjects[0];
            foreach (ICollidable collisionOccuredObject in collisionOccuredObjects){
                if (collisionOccuredObject.Position.X > return_collidable.Position.X) {
                    return_collidable = collisionOccuredObject;
                }
            }
            movebleObject.Position = new Vector2(return_collidable.Position.X + return_collidable.Width, movebleObject.Position.Y);
            return true;
        }
        return false;
    }

    public (bool, ICollidable) TopCorrection(ICollidable movebleObject){
        List<ICollidable> collisionOccuredObjects = GetAllIntersections(movebleObject);
        if (collisionOccuredObjects.Count != 0){
            ICollidable return_collidable = collisionOccuredObjects[0];
            foreach (ICollidable collisionOccuredObject in collisionOccuredObjects){
                if (collisionOccuredObject.Position.Y < return_collidable.Position.Y){
                    return_collidable = collisionOccuredObject;
                }
            }
            movebleObject.Position = new Vector2(movebleObject.Position.X, return_collidable.Position.Y - movebleObject.Height);
            return (true, return_collidable);
        }
        return (false, null);
    }

    public bool BottomCorrection(ICollidable movebleObject){
        List<ICollidable> collisionOccuredObjects = GetAllIntersections(movebleObject);
        if (collisionOccuredObjects.Count != 0){
            ICollidable return_collidable = collisionOccuredObjects[0];
            foreach (ICollidable collisionOccuredObject in collisionOccuredObjects){
                if (collisionOccuredObject.Position.Y > return_collidable.Position.Y){
                    return_collidable = collisionOccuredObject;
                }
            }
            movebleObject.Position = new Vector2(movebleObject.Position.X, return_collidable.Position.Y + return_collidable.Height);
            return true;
        }
        return false;
    }

}