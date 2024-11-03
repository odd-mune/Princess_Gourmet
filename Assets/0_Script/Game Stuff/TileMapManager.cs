using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [Tooltip("현재 Scene에 있는 grass용 tilemap들 목록")]
    public List<Tilemap> grassTilemaps;
    [Tooltip("현재 Scene에 있는 rock용 tilemap들 목록")]
    public List<Tilemap> rockTilemaps;
    [Tooltip("현재 Scene에 있는 wood용 tilemap들 목록")]
    public List<Tilemap> woodTilemaps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
