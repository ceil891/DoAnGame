using UnityEngine;
using UnityEngine.Tilemaps;

public class FruitTilemapSpawner : MonoBehaviour
{
    public Tilemap fruitTilemap;

    public TileBase appleTile;
    public TileBase bananaTile;
    public TileBase orangeTile;
    public TileBase kiwiTile;
    public TileBase melonTile;
    public TileBase strawberryTile;
    public TileBase pineappleTile;
    public TileBase cherriesTile;

    void Start()
    {
        foreach (var pos in fruitTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = fruitTilemap.GetTile(pos);
            if (tile == null) continue;

            Vector3 worldPos =
                fruitTilemap.CellToWorld(pos) + new Vector3(0.5f, 0.5f);

            if (tile == appleTile)
                FruitPool.Instance.Get(worldPos, FruitType.Apple);
            else if (tile == bananaTile)
                FruitPool.Instance.Get(worldPos, FruitType.Banana);
            else if (tile == orangeTile)
                FruitPool.Instance.Get(worldPos, FruitType.Orange);
            else if(tile == kiwiTile)FruitPool.Instance.Get(worldPos, FruitType.Kiwi);
            else if(tile == melonTile)FruitPool.Instance.Get(worldPos, FruitType.Melon);
            else if(tile == strawberryTile)FruitPool.Instance.Get(worldPos, FruitType.Strawberry);
            else if(tile == pineappleTile)FruitPool.Instance.Get(worldPos, FruitType.Pineapple);
            else if(tile == cherriesTile)FruitPool.Instance.Get(worldPos, FruitType.Cherries);
        }

        fruitTilemap.gameObject.SetActive(false);
    }
}
