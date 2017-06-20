using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    ///Rules:
    ///1. Generation will take place in horizontal sections 96 Pixels tall.
    ///2. Objects will be a max of 48 pixels tall. 
    ///3. Each row needs to have a platform to which the player can land on.
    ///

    private List<int> potentialFloatingObjects;
    private List<int> potentialEdgeObjects;

	void Start ()
    {
        ComplilePotentialObjects();

        //written like this for testing, will not be invoked during actual game.
        Invoke("FirstRow", 1f);
	}

    private void GenerateObject (int id, Vector2 desiredLocation)
    {
        float objectWidth = ObjectDatabase.staticObjects[id].objectWidth;
        float objectheight = ObjectDatabase.staticObjects[id].objectHeight;
        float locationX = desiredLocation.x + objectWidth / 2;
        float locationY = desiredLocation.y + objectheight / 2;

        GameObject test = Instantiate(ObjectDatabase.staticObjects[id].objectTemplate, new Vector3(locationX, locationY, -0.5f), Quaternion.identity);
        test.transform.parent = transform;
        ApplyCorrectSprite(test);
    }

    private void ComplilePotentialObjects ()
    {
        potentialFloatingObjects = new List<int>();
        potentialEdgeObjects = new List<int>();

        foreach (LevelObjects obj in ObjectDatabase.staticObjects)
        {
            if (obj.beginingLevel <= GameControl.currentLevel && obj.endingLevel >= GameControl.currentLevel)
            {
                if (obj.objectCategory == LevelObjects.ObjectCategories.attachedPlatform || obj.objectCategory == LevelObjects.ObjectCategories.solidBlock)
                {
                    potentialEdgeObjects.Add(obj.objectId);
                }
                else
                {
                    potentialFloatingObjects.Add(obj.objectId);
                }
            }
        }
    }

    private void ApplyCorrectSprite (GameObject obj)
    {
        //check the sprite database for appropriate sprite based on level and object.

        //obj.GetComponent<SpriteRenderer>().sprite;

        //if the object is attached to something on the right we need to flip the sprite.
    }

    private void FirstRow ()
    {
        //either generates something on the right side or the left.
        int side = Random.Range(0, 2);
        
        if (side == 0)
        {
            int objectId = Random.Range(0, potentialEdgeObjects.Count);
            GenerateObject(objectId, new Vector2(0, 0));
        }
        else
        {
            int objectId = Random.Range(0, potentialEdgeObjects.Count);
            GenerateObject(objectId, new Vector2(112, 0));
        }
    }
}