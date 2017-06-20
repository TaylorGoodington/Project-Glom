using System.Collections.Generic;
using UnityEngine;

public class ObjectDatabase : MonoBehaviour
{
    public List<LevelObjects> objects;
    public static List<LevelObjects> staticObjects;

	void Start ()
    {
        staticObjects = new List<LevelObjects>();
        staticObjects = objects;
    }
}

[System.Serializable]
public class LevelObjects
{
    public int objectId;
    public ObjectCategories objectCategory;
    public int beginingLevel;
    public int endingLevel;
    public int objectWidth;
    public int objectHeight;
    public GameObject objectTemplate;

    public LevelObjects(int id, ObjectCategories category, int begining, int ending, int width, int height, GameObject template)
    {
        objectId = id;
        objectCategory = category;
        beginingLevel = begining;
        endingLevel = ending;
        objectWidth = width;
        objectHeight = height;
        objectTemplate = template;
    }
    
    public enum ObjectCategories
    {
        solidBlock,
        attachedPlatform,
        floatingPlatform,
        ladder
    }
}