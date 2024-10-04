using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public Vector2 gridSize;
    public int totalMines = 20;
    public GridLayoutGroup gridLayout;
    public TileButton tilePrefab;

    List<Tile> tiles;
    List<TileButton> tileButtons;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        CreateTiles();
        CreateGridUI();
    }

    void CreateTiles()
    {
        tiles = new List<Tile>();
        Vector2 size = gridSize;

        int count = 0;
        int mineCount = 0;
        bool isMine = false;

        // TODO: refactoring como método
        List<int> mineIds = new();
        System.Random rnd = new();
        for (int i = 0; i < totalMines; i++)
        {
            int id = rnd.Next(0, (int)size.x * (int)size.y);
            if (!mineIds.Contains(id))
            {
                mineIds.Add(id);
                Debug.LogError($"Mine id: {id} in {i}");
            }
        }

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                isMine = mineIds.Contains(count);

                Tile tile = GetTile(count, new Vector2(i, j), isMine);
                tiles.Add(tile);
                count++;
            }
        }

        // int n = 0;
        // int t = 0;
        // for (int i = 0; i < tiles.count; i++)
        // {
        //     Tile tile = tiles[i];
        //     if (tile.isMine)
        //     {
        //         int x = tile.position.x
        //         int y = tile.position.y
        //         // Esquinas
        //         if (x == 0 && y == 0)
        //         {
        //             if (tiles[i + 1].isMine) { t += 1 }
        //             if ( == 1) { t += 1 }
        //             if ( == 1) { t += 1 }
        //             // Se asigna el valor al objeto Tile
        //                 = t
        //         }
        //         if (x == size.x && y == size.y)
        //         {

        //         }
        //         // Bordes

        //         // Interior

        //     }
        // }







    }

    Tile GetTile(int count, Vector2 position, bool isMine)
    {
        Tile tile = new Tile();
        tile.index = count;
        tile.position = position;
        tile.isShown = false;
        tile.isMine = isMine;

        return tile;
    }

    void addNumbersToTiles(int index, int n)
    {
        var tile = tiles.FirstOrDefault(t => t.index == index);
        tile.adjacentMines = n;
    }

    void CreateGridUI()
    {
        tileButtons = new List<TileButton>();
        ClearGrid();

        foreach (var tile in tiles)
        {
            TileButton tileButton = Instantiate(tilePrefab, gridLayout.transform);
            tileButton.Set(tile);
            tileButton.OnPressed = OnTilePressedHandler;
            tileButtons.Add(tileButton);
        }

        SetGridSize();
    }

    void SetGridSize()
    {
        RectTransform gridRectTransform = gridLayout.GetComponent<RectTransform>();
        Vector2 gridLayoutSize = gridSize * tilePrefab.GetComponent<RectTransform>().rect.height;
        gridRectTransform.sizeDelta = gridLayoutSize;
    }

    void ClearGrid()
    {
        tileButtons.ForEach(Destroy);
        tileButtons.Clear();
    }

    void OnTilePressedHandler(int index)
    {
        Debug.LogError($"TilePressedHandler: {index}");

        var tile = tiles.FirstOrDefault(t => t.index == index);
        var tileButton = tileButtons.FirstOrDefault(t => t.Index == index);

        if (tile != null && tile.isMine)
        {
            tileButton?.ShowMine();
            return;
        }
        Debug.LogError($"Is mine? {tile.isMine}");
        Debug.LogError($"Adjacent mines? {tile.adjacentMines}");

        tileButton.ShowNumber(3);

    }
}
