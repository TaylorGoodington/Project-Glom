using UnityEngine;
using System.Collections.Generic;

public class TestLevel : MonoBehaviour
{
    //TODO remove at some point
    public bool generateLevel;

    public List<GameObject> lLComponents;
    public List<GameObject> lRComponents;
    public List<GameObject> rLComponents;
    public List<GameObject> rRComponents;
    public List<GameObject> bLComponents;
    public List<GameObject> bRComponents;
    public List<GameObject> tLComponents;
    public List<GameObject> tRComponents;
    public GameObject bottomComponent;
    public GameObject bossComponent;
    public int levelBlocks;

    void Start ()
    {
        GameControl.playerHasControl = true;

        if (generateLevel)
        {
            GenerateLevel();
        }
    }

    private void GenerateLevel()
    {
        int blockSize = 128;
        int currentPosition = 0;
        int finishingSide = 0;
        GameObject block;
        int component;
        int position = 1;

        List<LevelComponents> levelComponents = new List<LevelComponents>();
        levelComponents.Add(new LevelComponents(0, 0, 0, lLComponents));
        levelComponents.Add(new LevelComponents(0, 0, 1, lRComponents));
        levelComponents.Add(new LevelComponents(0, 1, 0, rLComponents));
        levelComponents.Add(new LevelComponents(0, 1, 1, rRComponents));

        //Insert Entry block.
        block = Instantiate(bottomComponent, new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
        block.transform.SetParent(gameObject.transform);
        currentPosition += blockSize;

        //Insert Starting block.
        int startingSide = Random.Range(0, 2);
        if (startingSide == 0)
        {
            int section = Random.Range(0, bLComponents.Count);
            block = Instantiate(bLComponents[section], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
            finishingSide = 0;

        }
        else
        {
            int section = Random.Range(0, bRComponents.Count);
            block = Instantiate(bRComponents[section], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
            finishingSide = 1;
        }

        BlockChildingAndObstacleSpawning(blockSize, ref currentPosition, block, ref position);

        //All middle blocks.
        for (int i = 0; i <= levelBlocks; i++)
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
            BlockChildingAndObstacleSpawning(blockSize, ref currentPosition, block, ref position);
        }

        //Insert Top Block
        if (finishingSide == 1)
        {
            component = Random.Range(0, tRComponents.Count);
            block = Instantiate(tRComponents[component], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
        }
        else
        {
            component = Random.Range(0, tLComponents.Count);
            block = Instantiate(tLComponents[component], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
        }

        BlockChildingAndObstacleSpawning(blockSize, ref currentPosition, block, ref position);

        //Insert Boss Block.
        block = Instantiate(bossComponent, new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
        block.transform.SetParent(gameObject.transform);
    }

    private void BlockChildingAndObstacleSpawning(int blockSize, ref int currentPosition, GameObject block, ref int position)
    {
        block.transform.SetParent(gameObject.transform);
        currentPosition += blockSize;
        position++;

        int currentObstacleAllowance = GameControl.difficulty + GameControl.currentLevel + Mathf.FloorToInt(position / 3);
        float trapAllowance = Random.Range(1, 11);
        int currentTrapAllowance = Mathf.RoundToInt((trapAllowance / 10) * currentObstacleAllowance);
        int currentEnemyAllowance = Mathf.RoundToInt(((10 - trapAllowance) * 10) * currentObstacleAllowance);
        
        int remainder = block.transform.GetChild(block.transform.childCount - 1).GetComponent<TrapGeneration>().SpawnTraps(currentTrapAllowance);
        block.GetComponent<EnemySpawner>().SpawnEnemies(currentEnemyAllowance + remainder);
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