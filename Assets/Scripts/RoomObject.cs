using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomObject : MonoBehaviour
{
    public bool objectEnabled = false;
    public bool activeObject = false;

    public virtual void ChangeVisibility(bool active)
    {
        objectEnabled = active;
    }

    public virtual void HoverObject(bool active)
    {
        activeObject = active;
    }
}
