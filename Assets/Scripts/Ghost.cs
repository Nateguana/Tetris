using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public Tile tile;
    public Board mainBoard;
    public Piece trackingPiece;

    public Tilemap tilemap { get; private set; }
    public Cell[] cells { get; private set; }
    public Vector2Int position { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        if(cells!=null)
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = (Vector3Int)(cells[i].positon + position);
            tilemap.SetTile(tilePosition, null);
        }
    }

    private void Copy()
    {
        cells = (Cell[]) trackingPiece.cells?.Clone();
    }

    private void Drop()
    {
        Vector2Int pos = trackingPiece.position;

        int current = pos.y;
        int bottom = -mainBoard.boardSize.y / 2 - 1;

        mainBoard.Clear(trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            pos.y = row;

            if (mainBoard.IsValidPosition(trackingPiece.cells, pos)) {
                position = pos;
            } else {
                break;
            }
        }

        mainBoard.Set(trackingPiece);
    }

    private void Set()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = (Vector3Int)(cells[i].positon + position);
            tilemap.SetTile(tilePosition, tile);
        }
    }

}
