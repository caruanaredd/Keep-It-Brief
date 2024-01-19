using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPusher : TileBase
{
    [SerializeField] private Direction direction;

    public override void Trigger()
    {
        
    }

    public override void Trigger(Movement movement)
    {
        movement.Push(direction);
        AudioManager.instance.PlaySoundEffect(3);
    }
}
