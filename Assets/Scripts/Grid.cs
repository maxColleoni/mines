using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject tilePrefab;
    public Vector2 gridSize;
    public int totalMines = 10;

    List<Tile> tiles;

    private void Start(){
        Generate(gridSize);
    }

    public void Generate(Vector2 size){
        tiles = new List<Tile>();

        int count = 0;
        for (int i = 0; i < size.x; i++){
            for (int j = 0; j < size.y; j++){
                Tile tile = GetTile(count, new Vector2(i, j));
                tiles.Add(tile);
                count++;
            }
        }
        
        Debug.LogError(tiles.Count);
    }

    Tile GetTile(int count, Vector2 position){
        Tile tile = new Tile();
        tile.index = count;
        tile.position = position;
        tile.isShown = false;
        tile.isMine = GetIsMine();

        return tile;
    }

    bool GetIsMine(){
        
        return false;
    }
}
