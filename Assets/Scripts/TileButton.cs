using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI numberLabel;
    public Image mineImage;

    Tile _tile;

    public int Index => _tile.index;
    public Action<int> OnPressed;
    
    private void Awake(){
        if (button != null){
            button.onClick.AddListener(OnButtonPressed);
        }
    }

    public void Set(Tile tile){
        if (tile == null){
            return;
        }

        _tile = tile;
        
        numberLabel.SetText(string.Empty);
        numberLabel.gameObject.SetActive(false);
        mineImage.gameObject.SetActive(false);
    }

    public void ShowNumber(int number){
        numberLabel.SetText(number.ToString());
        numberLabel.gameObject.SetActive(true);
    }

    public void ShowMine(){
        mineImage.gameObject.SetActive(true);
    }

    void OnButtonPressed(){
        OnPressed?.Invoke(_tile.index);
    }
    
    
    
}
