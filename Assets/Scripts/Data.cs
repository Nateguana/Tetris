using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };

    private static Vector2Int V(int x, int y)
    {
        return new Vector2Int(x, y);
    }

    public static readonly Dictionary<Tetromino, Vector2Int[]> Cells = new Dictionary<Tetromino, Vector2Int[]>()
    {
        { Tetromino.I, new Vector2Int[] { V(-1, 1), V( 0, 1), V( 1, 1), V( 2, 1) } },
        { Tetromino.J, new Vector2Int[] { V(-1, 1), V(-1, 0), V( 0, 0), V( 1, 0) } },
        { Tetromino.L, new Vector2Int[] { V( 1, 1), V(-1, 0), V( 0, 0), V( 1, 0) } },
        { Tetromino.O, new Vector2Int[] { V( 0, 1), V( 1, 1), V( 0, 0), V( 1, 0) } },
        { Tetromino.S, new Vector2Int[] { V( 0, 1), V( 1, 1), V(-1, 0), V( 0, 0) } },
        { Tetromino.T, new Vector2Int[] { V( 0, 1), V(-1, 0), V( 0, 0), V( 1, 0) } },
        { Tetromino.Z, new Vector2Int[] { V(-1, 1), V( 0, 1), V( 0, 0), V( 1, 0) } },
    };
}

