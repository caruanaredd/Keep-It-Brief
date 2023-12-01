using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopper : TileBase
{
    public override void Trigger()
    {
        
    }

    public override void Trigger(Movement movement)
    {
        if (movement.hasControl)
            return;
            
        movement.Stop();
    }
}
