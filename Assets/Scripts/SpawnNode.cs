using System;
using UnityEngine;

[Serializable]
public class SpawnNode
{
    public SpawnNode(Vector2 position)
    {
        Position = position;
    }

    public bool IsOccupied { get; set; }

    public Vector2 Position { get; set; }
}