using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2 size;
    public GameObject tilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size.x; i++){
            for (int j = 0; j < size.y; j++){
                Vector2 pos = new Vector2(i, j);
                Instantiate(tilePrefab, pos, Quaternion.identity);
            }
        }
    }

}
