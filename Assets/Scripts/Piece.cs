using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector2Int[] cells { get; private set; }
    public Vector2Int position { get; private set; }
    public int rotationIndex { get; private set; }

    public float stepDelay = 1f;
    public float moveDelay = 0.1f;
    public float lockDelay = 0.25f;

    private float stepTime;
    private float moveTime;
    private float lockTime;

    public void Initialize(Board board, Vector2Int position, TetrominoData data)
    {
        this.data = data;
        this.board = board;
        this.position = position;
        rotationIndex = 0;

        stepTime = Time.time + stepDelay;
        moveTime = Time.time + moveDelay;
        lockTime = 0f;

        if (cells == null) {
            cells = new Vector2Int[data.cells.Length];
        }

        for (int i = 0; i < cells.Length; i++) {
            cells[i] = data.cells[i];
        }
    }

    private void Update()
    {
        board.Clear(this);

        // We use a timer to allow the player to make adjustments to the piece
        // before it locks in place
        lockTime += Time.deltaTime;

        // Handle rotation
        if (Input.GetKeyDown(KeyCode.Q)) {
            Rotate(-1);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            Rotate(1);
        }

        // Handle hard drop
        if (Input.GetKeyDown(KeyCode.Space)) {
            HardDrop();
        }

        // Allow the player to hold movement keys but only after a move delay
        // so it does not move too fast
        if (Time.time > moveTime) {
            HandleMoveInputs();
        }

        // Advance the piece to the next row every x seconds
        if (Time.time > stepTime) {
            Step();
        }

        board.Set(this);
    }

    private void HandleMoveInputs()
    {
        moveTime = Time.time + moveDelay;

        // Soft drop movement
        if (Input.GetKey(KeyCode.S)) {
            MoveCheck(Vector2Int.down);
        }

        // Left/right movement
        if (Input.GetKey(KeyCode.A)) {
            MoveCheck(Vector2Int.left);
        } else if (Input.GetKey(KeyCode.D)) {
            MoveCheck(Vector2Int.right);
        }
    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;

        // Do not move down if the player is already holding down
        // otherwise it can cause a double movement
        if (!Input.GetKey(KeyCode.S)) {
            MoveCheck(Vector2Int.down);
        }

        // Once the piece has been inactive for too long it becomes locked
        if (lockTime >= lockDelay) {
            Lock();
        }
    }

    private void HardDrop()
    {
        while (MoveCheck(Vector2Int.down)) {
            continue;
        }

        Lock();
    }

    private void Lock()
    {
        board.Set(this);
        board.ClearLines();
        board.SpawnPiece();
    }

    private bool Move(Vector2Int translation)
    {
        Vector2Int newPosition = position + translation;

        bool valid = board.IsValidPosition(cells, newPosition);

        // Only save the movement if the new position is valid
        if (valid)
        {
            position = newPosition;
            lockTime = 0f; // reset
        }

        return valid;
    }
    private void Rotate(int direction)
    {
        // Store the current rotation in case the rotation fails
        // and we need to revert
        int originalRotation = rotationIndex;

        // Rotate all of the cells using a rotation matrix
        rotationIndex = Wrap(rotationIndex + direction, 0, 4);
        Vector2Int[] newCells = ApplyRotationMatrix(direction);

        ApplyBounds(newCells);
    }

    private Vector2Int[] ApplyRotationMatrix(int direction)
    {
        float[] matrix = Data.RotationMatrix;
        Vector2Int[] newCells = new Vector2Int[cells.Length];
        // Rotate all of the cells using the rotation matrix
        for (int i = 0; i < cells.Length; i++)
        {
            Vector2 cell = cells[i];

            int x, y;

            switch (data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    // "I" and "O" are rotated from an offset center point
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;
            }

            newCells[i] = new Vector2Int(x, y);
        }
        return newCells;
    }
    private void ApplyBounds(Vector2Int[] newCells)
    {
        Vector2Int newPos = position;
        RectInt bounds = board.Bounds;
        bool collision;
        int cutoff = 1024;
        do
        {
           collision = false;
            for (int i = 0; i < cells.Length; i++)
            {
                int x = newCells[i].x + newPos.x;
                if (x < bounds.xMin)
                {
                    newPos += Vector2Int.right;
                    collision = true;
                    break;
                }
                if (x >= bounds.xMax)
                {
                    newPos += Vector2Int.left;
                    collision = true;
                    break;
                }
            }
            if (!collision && !board.IsValidPosition(newCells, newPos)) return;
        } while (collision&& cutoff-->0);
        cells = newCells;
        position = newPos;

    }
    private int Wrap(int input, int min, int max)
    {
        if (input < min) {
            return max - (min - input) % (max - min);
        } else {
            return min + (input - min) % (max - min);
        }
    }

}
