using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
public class TestTilemap : MonoBehaviour
{
    public Tilemap myMap;
    public TileBase liveCell;
    int neighbourCount;
    public int[,] multiDimensionalMap = new int[15, 15];
    public int[,] tempMap = new int[15, 15];

    private void Start()
    {
        for (int y = 0; y < multiDimensionalMap.GetLength(1); y++)
        {
            for (int x = 0; x < multiDimensionalMap.GetLength(0); x++)
            {
                multiDimensionalMap[x, y] = Random.Range(0, 2);
            }
        }
        DrawMap();
        StartCoroutine(Automate());
    }

    void DrawMap()
    {
        for (int y = 0; y < multiDimensionalMap.GetLength(1); y++)
        {
            for (int x = 0; x < multiDimensionalMap.GetLength(0); x++)
            {
                if (multiDimensionalMap[x, y] == 0)
                {
                    myMap.SetTile(new Vector3Int(x, y, 0), null);
                }
                else
                {
                    myMap.SetTile(new Vector3Int(x, y, 0), liveCell);
                }
                CountNeighbouringCells(x, y);
            }
        }
        
    }

    int CountNeighbouringCells(int x, int y)
    {
        neighbourCount = 0;
       
        for (int newX = -1; newX <= 1; newX++) 
        {
            for (int newY = -1; newY <= 1; newY++)
            {
                int checkX = newX + x;
                int checkY = newY + y;

                // Keep within range of the map.
                if (checkX >= 0 && checkX < multiDimensionalMap.GetLength(0) &&
                checkY >= 0 && checkY < multiDimensionalMap.GetLength(1))
                {
                    if (multiDimensionalMap[checkX, checkY] == 1)
                    {
                        neighbourCount++;
                    }
                }
            }
        }
        if (multiDimensionalMap[x, y] == 1)
        {
            neighbourCount--;
        }
        return neighbourCount;
    }   

    IEnumerator Automate()
    {
        while (true)
        {
            GenerateCells();
            DrawMap();

            yield return new WaitForSeconds(0.1f);
        }
    }

    void GenerateCells()
    {
        for (int y = 0; y < multiDimensionalMap.GetLength(1); y++)
        {
            for (int x = 0; x < multiDimensionalMap.GetLength(0); x++)
            {
                neighbourCount = CountNeighbouringCells(x, y);

                if (multiDimensionalMap[x, y] == 1)
                {
                    if (neighbourCount < 2 || neighbourCount > 3)
                    {
                        tempMap[x, y] = 0;
                    }
                    else if (neighbourCount == 3 || neighbourCount == 2)
                    {
                        tempMap[x, y] = 1;
                    }
                }
                else if (multiDimensionalMap[x, y] == 0)
                {
                    if (neighbourCount == 3)
                    {
                        tempMap[x, y] = 1;
                    }
                }
            }
        }

        for (int y = 0; y < multiDimensionalMap.GetLength(1); y++)
        {
            for (int x = 0; x < multiDimensionalMap.GetLength(0); x++)
            {
                multiDimensionalMap[x, y] = tempMap[x, y];
            }
        }
       
        DrawMap();
    }
}
