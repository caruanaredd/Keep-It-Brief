using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopper : CustomTileBase
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
