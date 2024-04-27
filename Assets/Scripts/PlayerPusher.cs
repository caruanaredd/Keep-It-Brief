using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Custom/Pusher Tile", fileName = "New Pusher Tile")]
public class PlayerPusher : Tile
{
    [SerializeField] private Direction direction;
    public Direction Direction => direction;
}
