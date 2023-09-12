using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public virtual void Begin() {}
    
    public virtual bool IsFinished()
    {
        return false;
    }
    
    public virtual Status GetMyStatus()
    {
        return Status.None;
    }
    
    public virtual void ChooseGuid(int index){}

    public virtual void Reset()
    {
    }
}