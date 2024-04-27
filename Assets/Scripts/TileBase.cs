using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomTileBase : MonoBehaviour
{
    public abstract void Trigger();
    public abstract void Trigger(Movement movement);
}
