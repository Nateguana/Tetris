using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct TetrominoData
{

    public readonly string name;
    public readonly TetrominoRotation rotationType;
    public readonly Vector2Int[] cells;
    public readonly byte tile;

    public TetrominoData(string name, byte tile, Vector2Int[] cells, TetrominoRotation rotationType=TetrominoRotation.regular)
    {
        this.name = name;
        this.cells = cells;
        this.rotationType = rotationType;
        this.tile = tile;
    }
}
public enum TetrominoRotation
{
    regular,
    offset,
    none
}
