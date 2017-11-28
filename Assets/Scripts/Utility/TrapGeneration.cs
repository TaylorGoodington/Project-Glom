using System.Collections.Generic;
using UnityEngine;

public class TrapGeneration : MonoBehaviour
{
    private Texture2D heightMap;
    private int height = 128;
    private int length = 160;
    private Color red = Color.red;
    private int redXOffset = 16;
    private int redYOffset = 8;
    private Color blue = Color.blue;
    private int blueOffset = 8;

    public List<TrapInfo> trapInfo;

    void Start ()
    {
        trapInfo = new List<TrapInfo>();
        heightMap = GetComponent<SpriteRenderer>().sprite.texture;

        MapTraps();
        SpawnTraps();
	}

    private void MapTraps()
    {
        int area = height * length;
        Color pixelColor;
        int currentRow = 0;
        int currentColumn = 0;

        List<TrapInfo.Directions> directions = new List<TrapInfo.Directions>();

        for (int i = 0; i < area; i++)
        {
            pixelColor = heightMap.GetPixel(currentColumn, currentRow);

            if (pixelColor.a == 1 && IsPixelBottomLeftCorner(currentColumn, currentRow))
            {
                directions = TrapDirections(pixelColor, currentColumn, currentRow);

                //add to trap info with directions and position.
                if (pixelColor == blue)
                {

                }
                else
                {

                }
            }

            //Deals with the next pixel to check.
            if (currentColumn == length)
            {
                currentRow += 1;
                currentColumn = 0;
            }
            else
            {
                currentColumn += 1;
            }
        }
    }

    //TODO Finish
    private bool IsPixelBottomLeftCorner (int x, int y)
    {
        return true;
    }

    private List<TrapInfo.Directions> TrapDirections(Color color, int x, int y)
    {
        Vector2 northColorPixel;
        Vector2 southColorPixel;
        Vector2 eastColorPixel;
        Vector2 westColorPixel;

        //check for direction of trap
        northColorPixel = new Vector2(x + blueOffset, y + blueOffset * 2);
        southColorPixel = new Vector2(x + blueOffset, y);
        eastColorPixel = new Vector2(x + blueOffset * 2, y + blueOffset);
        westColorPixel = new Vector2(x, y + blueOffset);

        //TODO change with actual list.
        return new List<TrapInfo.Directions>();
    }

    //TODO Finish
    private void SpawnTraps()
    {

    }
}

[System.Serializable]
public class TrapInfo
{
    public TrapType type;
    public Vector2 location;
    public List<Directions> possibleDirections;

    public TrapInfo(List<Directions> trapDirection, TrapType trapType, Vector2 trapLocation)
    {
        type = trapType;
        location = trapLocation;
        possibleDirections = trapDirection;
    }

    public enum Directions
    {
        North,
        East,
        South,
        West
    }

    public enum TrapType
    {
        Fire,
        Spike
    }
}