using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public static bool original;
    public bool isOriginal;
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public ScreenShake screenShake;
    public GameObject sendScoreScreen;
    public ParticleSystem particles, particlePrefab;

    public Tile[] tiles;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector2Int spawnPosition = new Vector2Int(-1, 8);

    public ulong score = 0;

    [SerializeField] private Flashing flashEffect;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        original = isOriginal;
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = GetComponentInChildren<Piece>();
    }

    private void Start()
    {
        Piece.gameOver = false;
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int pieceNumber = original ? Data.originalPieces : Data.tetrominoes.Length;

        TetrominoData data = Data.tetrominoes[Random.Range(0, pieceNumber)];

        activePiece.Initialize(this, spawnPosition, data);

        if (!IsValidPosition(activePiece.cells, spawnPosition))
        {
            GameOver();
        }
        else
        {
            Set(activePiece);
        }
    }

    public void GameOver()
    {
        tilemap.ClearAllTiles();
        AudioManager.Play("GameOverPlaceholder"); //Placeholder

        if (!original)
        {
            Time.timeScale = 0f;
            Piece.gameOver = true;
            sendScoreScreen.SetActive(true);
        }   
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector2Int tilePosition = piece.cells[i].positon + piece.position;
            tilemap.SetTile((Vector3Int)tilePosition, tiles[piece.data.tile]);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = (Vector3Int)(piece.cells[i].positon + piece.position);
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Cell[] cells, Vector2Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < cells.Length; i++)
        {
            Vector2Int tilePosition = cells[i].positon + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains(tilePosition))
            {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (tilemap.HasTile((Vector3Int)tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;
        int rowsCleared = 0;
        // Clear from bottom to top
        while (row < bounds.yMax)
        {
            // Only advance to the next row if the current is not cleared
            // because the tiles above will fall down when a row is cleared
            if (IsLineFull(row))
            {
                LineClear(row);
                rowsCleared++;
                AudioManager.Play("ClearPlaceholder"); //Placeholder
                if (!original)
                {
                    flashEffect.Flash();
                    StartCoroutine(screenShake.Shake(.1f, .5f));
                }
            }
            else
            {
                row++;
                AudioManager.Play("LandPlaceholder"); // Placeholder
            }
        }
        if (!original&& rowsCleared > 0 && rowsCleared < 5)
            AddScore(Data.points[rowsCleared - 1]);
    }

    public bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            // The line is not full if a tile is missing
            if (!tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        // Clear all tiles in the row
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            if (!original)
            {
            particles.transform.position = new Vector3Int(0, row + 1/2, 0);
            particles.Play();
            }
            Vector3Int position = new Vector3Int(col, row, 0);
            tilemap.SetTile(position, null);
        }

        // Shift every row above down one
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }
    }
    public void AddScore(ulong add)
    {
        score += add;
        GetComponentInChildren<TMPro.TMP_Text>().text = score.ToString();
    }

}
