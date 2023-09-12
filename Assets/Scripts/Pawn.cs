using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public virtual void Init() {}
    
    public virtual void Begin() {}
    
    public virtual bool IsFinished()
    {
        return false;
    }

    public virtual void ChooseGuid(int index){}

    public virtual void Reset()
    {
    }
}