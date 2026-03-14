using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapVisualSpawner : MonoBehaviour
{
    public Tilemap trapTilemap;
    public GameObject sawPrefab;
    // public GameObject spikePrefab;
    // public GameObject firePrefab;

    // public TileBase sawTile;
    // public TileBase spikeTile;
    // public TileBase fireTile;

    void Start()
    {
        foreach (var pos in trapTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = trapTilemap.GetTile(pos);
            if (tile == null) continue;

            Vector3 worldPos = trapTilemap.GetCellCenterWorld(pos);

            // if (tile == sawTile)
            Instantiate(sawPrefab, worldPos, Quaternion.identity);
            // else if (tile == spikeTile)
            //     Instantiate(spikePrefab, worldPos, Quaternion.identity);
            // else if (tile == fireTile)
            //     Instantiate(firePrefab, worldPos, Quaternion.identity);
        }
    }
}
