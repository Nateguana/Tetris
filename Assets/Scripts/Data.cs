using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Data
{
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };

    private static Vector2Int V(int x, int y) => new Vector2Int(x, y);

    public static readonly TetrominoData[] tetrominoes = new TetrominoData[] {
        new TetrominoData("I", 4, new Vector2Int[] { V(-1, 1), V( 0, 1), V( 1, 1), V( 2, 1) },TetrominoRotation.offset),
        new TetrominoData("O", 2, new Vector2Int[] { V( 0, 1), V( 1, 1), V( 0, 0), V( 1, 0) },TetrominoRotation.none),
        new TetrominoData("T", 6,new Vector2Int[] { V( 0, 1), V(-1, 0), V( 0, 0), V( 1, 0) }),
        new TetrominoData("J", 5,new Vector2Int[] { V(-1, 1), V(-1, 0), V( 0, 0), V( 1, 0) }),
        new TetrominoData("L", 1,new Vector2Int[] { V( 1, 1), V(-1, 0), V( 0, 0), V( 1, 0) }),
        new TetrominoData("S", 3,new Vector2Int[] { V( 0, 1), V( 1, 1), V(-1, 0), V( 0, 0) }),
        new TetrominoData("Z", 0,new Vector2Int[] { V(-1, 1), V( 0, 1), V( 0, 0), V( 1, 0) }),
        new TetrominoData("Big O", 2,new Vector2Int[] { V(-2, 1), V(-1, 1), V(0, 1), V( 1, 1),
        V(-2, 0), V(-1, 0), V(0, 0), V( 1, 0),
        V(-2, -1), V(-1, -1), V(0, -1), V( 1, -1),
        V(-2, -2), V(-1, -2), V(0, -2), V( 1, -2),
        },TetrominoRotation.none)};
}

