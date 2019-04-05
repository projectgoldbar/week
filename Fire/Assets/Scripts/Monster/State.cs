using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public virtual void Initiate()
    {
    }

    public virtual void Ing()
    {
    }

    public virtual StateIndex NextState()
    {
        return StateIndex.IDLE;
    }
}