using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TrapEntry
{
    public TileBase markerTile;
    public GameObject prefab;
}

public class TrapSpawner : MonoBehaviour
{
    public Tilemap trapTilemap;
    public TrapEntry[] traps;

    void Start()
    {
        foreach (var pos in trapTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = trapTilemap.GetTile(pos);
            if (tile == null) continue;

            foreach (var trap in traps)
            {
                if (tile == trap.markerTile)
                {
                    Vector3 worldPos = trapTilemap.GetCellCenterWorld(pos);

                    GameObject obj = Instantiate(trap.prefab, worldPos, Quaternion.identity);

                    // ❗ CỰC KỲ QUAN TRỌNG
                    obj.transform.rotation = Quaternion.identity;
                    obj.transform.localScale = Vector3.one;

                    break;
                }
            }
        }

        trapTilemap.ClearAllTiles();
    }
}
