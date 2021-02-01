using UnityEngine;
using System.Collections.Generic;
using static Utility;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/New Level Generator", order = 1)]
public class LevelGenerator : ScriptableObject
{
    [SerializeField] List<GameObject> leftToLeftComponents = new List<GameObject>();
    [SerializeField] List<GameObject> leftToRightComponents = new List<GameObject>();
    [SerializeField] List<GameObject> rightToLeftComponents = new List<GameObject>();
    [SerializeField] List<GameObject> rightToRightComponents = new List<GameObject>();
    [SerializeField] List<GameObject> bottomLeftComponents = new List<GameObject>();
    [SerializeField] List<GameObject> bottomRightComponents = new List<GameObject>();
    [SerializeField] List<GameObject> topLeftComponents = new List<GameObject>();
    [SerializeField] List<GameObject> topRightComponents = new List<GameObject>();
    [SerializeField] GameObject bottomComponent = null;
    [SerializeField] GameObject bossComponent = null;
    [SerializeField] int levelBlocks = 20;

    public void CreateBottomBlock(Transform transform)
    {
        GameObject block = Instantiate(bottomComponent, new Vector3(0, 0, -0.5f), Quaternion.identity);
        block.transform.SetParent(transform);
    }

    public void GenerateLevel(Transform transform)
    {
        int blockSize = 128;
        int currentPosition = 128;
        int finishingSide;
        GameObject block;
        int component;
        int position = 1;

        List<LevelComponents> levelComponents = new List<LevelComponents>();
        levelComponents.Add(new LevelComponents(0, 0, 0, leftToLeftComponents));
        levelComponents.Add(new LevelComponents(0, 0, 1, leftToRightComponents));
        levelComponents.Add(new LevelComponents(0, 1, 0, rightToLeftComponents));
        levelComponents.Add(new LevelComponents(0, 1, 1, rightToRightComponents));

        //Insert Starting block.
        int startingSide = Random.Range(0, 2);
        if (startingSide == 0)
        {
            int section = Random.Range(0, bottomLeftComponents.Count);
            block = Instantiate(bottomLeftComponents[section], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
            finishingSide = 0;

        }
        else
        {
            int section = Random.Range(0, bottomRightComponents.Count);
            block = Instantiate(bottomRightComponents[section], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
            finishingSide = 1;
        }

        block.transform.SetParent(transform);
        BlockChildingAndObstacleSpawning(blockSize, ref currentPosition, block, ref position);

        //All middle blocks.
        for (int i = 0; i <= levelBlocks; i++)
        {
            List<LevelComponents> tempList = new List<LevelComponents>();

            foreach (LevelComponents levelComponent in levelComponents)
            {
                if (levelComponent.startSide == finishingSide)
                {
                    tempList.Add(levelComponent);
                }
            }

            int category = Random.Range(0, tempList.Count);
            component = Random.Range(0, tempList[category].blocks.Count);
            finishingSide = tempList[category].finishSide;
            block = Instantiate(tempList[category].blocks[component], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
            block.transform.SetParent(transform);
            BlockChildingAndObstacleSpawning(blockSize, ref currentPosition, block, ref position);
        }

        //Insert Top Block
        if (finishingSide == 1)
        {
            component = Random.Range(0, topRightComponents.Count);
            block = Instantiate(topRightComponents[component], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
        }
        else
        {
            component = Random.Range(0, topLeftComponents.Count);
            block = Instantiate(topLeftComponents[component], new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
        }

        block.transform.SetParent(transform);
        BlockChildingAndObstacleSpawning(blockSize, ref currentPosition, block, ref position);

        //Insert Boss Block.
        block = Instantiate(bossComponent, new Vector3(0, currentPosition, -0.5f), Quaternion.identity);
        block.transform.SetParent(transform);
    }

    private void BlockChildingAndObstacleSpawning(int blockSize, ref int currentPosition, GameObject block, ref int position)
    {
        currentPosition += blockSize;
        position++;

        int currentObstacleAllowance = SaveDataController.Instance.chosenDifficulty + LevelSpecificAllowance() + Mathf.FloorToInt(position / 3);
        float trapAllowance = Random.Range(1, 11);
        int currentTrapAllowance = Mathf.RoundToInt((trapAllowance / 10) * currentObstacleAllowance);
        int currentEnemyAllowance = Mathf.RoundToInt(((10 - trapAllowance) * 10) * currentObstacleAllowance);
        
        int remainder = block.transform.GetChild(block.transform.childCount - 1).GetComponent<TrapGeneration>().SpawnTraps(currentTrapAllowance);
        SpawnEnemies(block, currentEnemyAllowance + remainder);
    }

    private void SpawnEnemies(GameObject block, int allowance)
    {
        //list all possible spawn points
        List<GameObject> potentialSpawnPoints = new List<GameObject>(); 
        foreach (Transform child in block.transform)
        {
            if (child.gameObject.layer == (int)GameLayer.ground || child.gameObject.layer == (int)GameLayer.platforms)
            {
                if (child.GetComponent<BoxCollider2D>().size.x >= 48)
                {
                    potentialSpawnPoints.Add(child.gameObject);
                }
            }
        }

        //pick spawn points as needed
        List<GameObject> spawnPoints = new List<GameObject>();
        for (int i = 0; i < allowance; i++)
        {
            if (potentialSpawnPoints.Count > 1)
            {
                int position = Random.Range(0, potentialSpawnPoints.Count - 1);
                spawnPoints.Add(potentialSpawnPoints[position]);
                potentialSpawnPoints.RemoveAt(position);
            }
            else
            {
                break;
            }
        }

        //spawn enemies
        //TODO Improve to add logic about unlocked enemies and positions within the platform for enemies to be randomly instantiated on.
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Instantiate(EnemyDatabase.Instance.enemies[2], spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }
    }

    private int LevelSpecificAllowance()
    {
        return 1;
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