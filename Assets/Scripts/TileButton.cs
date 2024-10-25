using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileButton : MonoBehaviour, IPointerDownHandler
{
    public Button button;
    public TextMeshProUGUI numberLabel;
    public Image mineImage;
    public Image flagImage;
    public Image questionMarkImage;

    Tile _tile;
    List<Image> _images = new List<Image>();

    public int Index => _tile.index;
    public Action<int, PointerEventData> OnPressed;
    public bool IsFlagged => flagImage.gameObject.activeInHierarchy;

    private void Awake(){
        _images = new List<Image>(){
            mineImage,
            flagImage,
            questionMarkImage
        };
    }

    public void Set(Tile tile){
        if (tile == null){
            return;
        }

        _tile = tile;
        
        numberLabel.SetText(string.Empty);
        numberLabel.gameObject.SetActive(false);

        ClearTile();
    }

    void ClearTile(){
        foreach (var image in _images){
            image.gameObject.SetActive(false);
        }
        
        numberLabel.gameObject.SetActive(false);
        _tile.isShown = false;
    }

    public void ShowNumber(){
        ClearTile();
        int number = _tile.adjacentMines;
        numberLabel.SetText(number.ToString());
        numberLabel.gameObject.SetActive(true);
        _tile.isShown = true;
    }

    public void ShowMine(){
        ClearTile();
        mineImage?.gameObject.SetActive(true);
    }

    public void FlagMine(){
        bool flag = !IsFlagged;
        ClearTile();
        
        flagImage?.gameObject.SetActive(flag);
        _tile.isShown = IsFlagged;
    }
    
    public void FlagUnknown(){
        ClearTile();
        questionMarkImage?.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData){
        OnPressed?.Invoke(_tile.index, eventData);
    }
}
