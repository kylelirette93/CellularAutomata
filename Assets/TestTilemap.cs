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
    public int[,] multiDimensionalMap = new int[25, 25];

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
    }

    void DrawMap()
    {
        for (int y = 0; y < multiDimensionalMap.GetLength(1) - 1; y++)
        {
            for (int x = 0; x < multiDimensionalMap.GetLength(0) - 1; x++)
            {
                if (multiDimensionalMap[x, y] == 0)
                {
                    myMap.SetTile(new Vector3Int(x, y, 0), null);
                }
                else
                {
                    myMap.SetTile(new Vector3Int(x, y, 0), liveCell);
                    CountNeighbouringCells(x, y);
                }
            }
        }
    }

    int CountNeighbouringCells(int x, int y)
    {
        int neighbourCount = 0;
        // # # #
        // _ * _    * = x 1 and y 1
        // _ _ _
         // x - 1 y -1  ,  x ,   x + 1
         // x -1 y 
        for (int newX = -1; newX < 2; newX++) 
        {
            for (int newY = -1; newY < 2; newY++)
            {
                if (multiDimensionalMap[x + newX, y + newY] == 1)
                {
                    neighbourCount++;
                }
            }
        }
        if (multiDimensionalMap[x, y] == 1)
        {
            neighbourCount--;
        }
        return neighbourCount;

    }

    

    
   
}
