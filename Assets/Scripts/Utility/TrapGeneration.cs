using System.Collections;
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

    private List<TrapInfo> MapTraps()
    {
        List<TrapInfo> trapInfo = new List<TrapInfo>();
        int area = height * length;
        Color pixelColor;
        int currentRow = 0;
        int currentColumn = 0;

        List<TrapInfo.Directions> directions = new List<TrapInfo.Directions>();

        for (int i = 0; i < area; i++)
        {
            pixelColor = heightMap.GetPixel(currentColumn, currentRow);

            if (pixelColor.a == 1)
            {
                directions = TrapDirections(pixelColor, currentColumn, currentRow);

                if (directions.Count >= 1)
                {
                    if (pixelColor == blue)
                    {
                        trapInfo.Add(new TrapInfo(directions, TrapInfo.TrapType.Spike, new Vector2(currentColumn + blueOffset, currentRow + blueOffset)));
                    }
                    else
                    {
                        trapInfo.Add(new TrapInfo(directions, TrapInfo.TrapType.Fire, new Vector2(currentColumn + redXOffset, currentRow + redYOffset)));
                    }
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

        return trapInfo;
    }

    private List<TrapInfo.Directions> TrapDirections(Color color, int x, int y)
    {
        List<TrapInfo.Directions> directions = new List<TrapInfo.Directions>();

        Color northColorPixel;
        Color southColorPixel;
        Color eastColorPixel;
        Color westColorPixel;

        int xOffset = 0;
        int yOffset = 0;

        if (color == blue)
        {
            xOffset = blueOffset;
            yOffset = blueOffset;
        }
        else if (color == red)
        {
            xOffset = redXOffset;
            yOffset = redYOffset;
        }

        //Checks for if the Pixel is on the map.
        if ((x + xOffset * 2) <= length && (y + yOffset * 2) <= height)
        {
            northColorPixel = heightMap.GetPixel(x + xOffset, y + (yOffset * 2) - 1);
            southColorPixel = heightMap.GetPixel(x + xOffset, y);
            eastColorPixel = heightMap.GetPixel(x + (xOffset * 2) - 1, y + yOffset);
            westColorPixel = heightMap.GetPixel(x, y + yOffset);

            if (northColorPixel.a == 1 && southColorPixel.a == 1 && eastColorPixel.a == 1 && westColorPixel.a == 1)
            {
                if (color == blue)
                {
                    //if (northColorPixel.a == 1 && northColorPixel != blue)
                    //{
                    //    directions.Add(TrapInfo.Directions.North);
                    //}

                    //if (southColorPixel.a == 1 && southColorPixel != blue)
                    //{
                    //    directions.Add(TrapInfo.Directions.South);
                    //}

                    //if (eastColorPixel.a == 1 && eastColorPixel != blue)
                    //{
                    //    directions.Add(TrapInfo.Directions.East);
                    //}

                    //if (westColorPixel.a == 1 && westColorPixel != blue)
                    //{
                    //    directions.Add(TrapInfo.Directions.West);
                    //}

                    directions.Add(TrapInfo.Directions.South);
                }
                else if (color == red)
                {
                    if (northColorPixel.a == 1 && northColorPixel != red)
                    {
                        directions.Add(TrapInfo.Directions.North);
                    }

                    if (southColorPixel.a == 1 && southColorPixel != red)
                    {
                        directions.Add(TrapInfo.Directions.South);
                    }

                    if (eastColorPixel.a == 1 && eastColorPixel != red)
                    {
                        directions.Add(TrapInfo.Directions.East);
                    }

                    if (westColorPixel.a == 1 && westColorPixel != red)
                    {
                        directions.Add(TrapInfo.Directions.West);
                    }
                }
            }
        }
                
        return directions;
    }

    //TODO Finish by incorporating position for difficulty
    public int SpawnTraps(int allowance)
    {
        List<TrapInfo> trapInfo = new List<TrapInfo>();
        heightMap = GetComponent<SpriteRenderer>().sprite.texture;
        trapInfo = MapTraps();
        List<int> trapsList = new List<int>();
        int remainder = 0;
        int delayPath = 1;

        for (int i = 0; i < trapInfo.Count; i++)
        {
            trapsList.Add(i);
        }

        int trapSlots;
        //Figure out if were gonna use Quality or Quantity as the primary factor.
        //Will do so by seeing if Quantity is viable based on the number of potential positions
        //Next the block will "buy" appropriate traps based on the outcome.

        if (trapsList.Count <= allowance)
        {
            trapSlots = trapsList.Count;
        }
        else
        {
            trapSlots = allowance;
        }

        for (int i = 0; i < trapSlots; i++)
        {
            if (delayPath == 3)
            {
                delayPath = 1;
            }
            else
            {
                delayPath += 1;
            }

            int position = trapsList[Random.Range(0, trapsList.Count)];
            StartCoroutine(DelayTrap(delayPath, trapInfo, position));
            trapsList.Remove(position);
        }

        return remainder;
    }

    private IEnumerator DelayTrap(int delayPath, List<TrapInfo> trapInfo, int position)
    {
        if (delayPath == 1)
        {
            CreateTrap(trapInfo, position);
        }
        else if (delayPath == 2)
        {
            yield return new WaitForSeconds(1);
            CreateTrap(trapInfo, position);
        }
        else
        {
            yield return new WaitForSeconds(2);
            CreateTrap(trapInfo, position);
        }
    }

    private void CreateTrap(List<TrapInfo> trapInfo, int position)
    {
        GameObject trap = Instantiate(TrapsDatabase.staticTraps[(int)trapInfo[position].type], trapInfo[position].location + (Vector2)transform.position, Quaternion.identity);
        trap.transform.SetParent(transform);
        TrapInfo.Directions trapDirection = trapInfo[position].possibleDirections[Random.Range(0, trapInfo[position].possibleDirections.Count)];

        if (trapDirection == TrapInfo.Directions.North)
        {
            trap.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (trapDirection == TrapInfo.Directions.South)
        {
            trap.GetComponent<SpriteRenderer>().flipY = false;
        }
        else if (trapDirection == TrapInfo.Directions.East)
        {
            trap.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (trapDirection == TrapInfo.Directions.West)
        {
            trap.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}