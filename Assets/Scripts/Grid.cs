using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public Vector2 gridSize;
    public int totalMines = 3;
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

        // TODO: refactoring como método
        List<int> mineIds = new();
        System.Random rnd = new();
        for (int i = 0; i < totalMines; i++)
        {
            int id = rnd.Next(0, (int)size.x * (int)size.y);
            if (!mineIds.Contains(id))
            {
                mineIds.Add(id);
            }
        }

        int count = 0;
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                bool isMine = mineIds.Contains(count);
                Tile tile = CreateTile(count, new Vector2(i, j), isMine);
                tiles.Add(tile);
                count++;
            }
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            int mineCount = 0;
            Tile tile = tiles[i];
            if (!tile.isMine)
            {
                int x = (int)tile.position.x;
                int y = (int)tile.position.y;

                for (int k = -1; k < 2; k++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        Vector2 v = new Vector2(x + j, y + k);
                        Tile t = CreateTileByPosition(v);
                        if (t != null && t.isMine)
                        {
                            mineCount += 1;
                        }

                    }
                }
                tile.adjacentMines = mineCount;
            }
        }
    }

    Tile CreateTile(int count, Vector2 position, bool isMine)
    {
        Tile tile = new Tile();
        tile.index = count;
        tile.position = position;
        tile.isShown = false;
        tile.isMine = isMine;

        return tile;
    }

    Tile CreateTileByPosition(Vector2 position)
    {
        Tile tile = null;
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile t = tiles[i];
            if (t.position != position)
            {
                continue;
            }
            tile = t;
        }
        return tile;
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
        var tile = tiles.FirstOrDefault(t => t.index == index);
        var tileButton = tileButtons.FirstOrDefault(t => t.Index == index);

        if (tile != null && tile.isMine)
        {
            tileButton?.ShowMine();
            return;
        }

        tileButton.ShowNumber(tile.adjacentMines);

    }
}
