using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTile : CustomTileBase
{
    public override void Trigger()
    {
        
    }

    public override void Trigger(Movement movement)
    {
        movement.Kill();
        AudioManager.instance.PlaySoundEffect(2);
    }
}