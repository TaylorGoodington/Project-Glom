using UnityEngine;
using System.Collections.Generic;

public class TestLevel : MonoBehaviour
{
    public List<GameObject> lLComponents;
    public List<GameObject> lRComponents;
    public List<GameObject> rLComponents;
    public List<GameObject> rRComponents;
    public int levelSize;

    void Start ()
    {
        GameControl.playerHasControl = true;
        //GenerateLevel();
    }

    private void GenerateLevel()
    {
        int blockSize = 128;
        int blocks = levelSize / blockSize;
        int currentPosition = 0;
        int finishingSide = 0;

        List<LevelComponents> levelComponents = new List<LevelComponents>();
        levelComponents.Add(new LevelComponents(0, 0, 0, lLComponents));
        levelComponents.Add(new LevelComponents(0, 0, 1, lRComponents));
        levelComponents.Add(new LevelComponents(0, 1, 0, rLComponents));
        levelComponents.Add(new LevelComponents(0, 1, 1, rRComponents));

        for (int i = 0; i <= blocks; i++)
        {
            int component;
            GameObject block;

            if (i == 0)
            {
                int startingCategory = Random.Range(0, levelComponents.Count);
                component = Random.Range(0, levelComponents[startingCategory].blocks.Count);
                finishingSide = levelComponents[startingCategory].finishSide;
                block = Instantiate(levelComponents[startingCategory].blocks[component], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
            }
            else
            {
                List<LevelComponents> tempList = new List<LevelComponents>();

                foreach (var item in levelComponents)
                {
                    if (item.startSide == finishingSide)
                    {
                        tempList.Add(item);
                    }
                }

                int category = Random.Range(0, tempList.Count);
                component = Random.Range(0, tempList[category].blocks.Count);
                finishingSide = tempList[category].finishSide;
                block = Instantiate(tempList[category].blocks[component], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
            }
                
            block.transform.SetParent(gameObject.transform);
            currentPosition += blockSize;
        }
    }
}

public class LevelComponents
{
    public int id;
    public int startSide;
    public int finishSide;
    public List<GameObject> blocks;

    public LevelComponents (int listId, int blockStartSide, int blockFinishSide, List<GameObject> blockCategories)
    {
        id = listId;
        startSide = blockStartSide;
        finishSide = blockFinishSide;
        blocks = blockCategories;
    }
}