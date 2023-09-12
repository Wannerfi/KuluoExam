using System;
using System.Collections.Generic;
using UnityEngine;

public class Chess
{
    private int[] grids = new int[9];
    private ItemStatus[] gridItem = new ItemStatus[9];
    
    public void AddGridItem(ItemStatus s)
    {
        gridItem[s.number] = s;
    }

    public void Reset()
    {
        grids = new int[9];
        foreach (var i in gridItem)
        {
            i.Reset();
        }
    }

    public bool IsSet(int index)
    {
        return grids[index] != 0;
    }
    
    public bool TryToSetGrids(int index, Status s)
    {
        if (grids[index] == 0)
        {
            grids[index] = (int)s;
            gridItem[index].ChangeStatus(s);
            return true;
        }
        
        return false;
    }

    public bool TestOver(int index, Status s)
    {
        var cur = grids[index];
        grids[index] = (int)s;
        bool res = IsOver();
        grids[index] = cur;
        return res;
    }

    private List<int> overIndex = new List<int>();

    public List<int> OverIndex
    {
        get
        {
            return overIndex;
        }
    }
    public bool IsOver()
    {
        int cAll = (int)Status.Cross;
        int rAll = (int)Status.Round;
        bool link = IsRowOrColOver(cAll, out overIndex) || IsRowOrColOver(rAll, out overIndex) ||
               IsHypoOver(cAll, out overIndex) || IsHypoOver(rAll, out overIndex);

        if (link || !isExistEmpty())
            return true;
        return false;
    }

    private bool isExistEmpty()
    {
        for (int i = 0; i < 9; ++i)
        {
            if (grids[i] == 0)
                return true;
        }

        return false;
    }

    private bool IsRowOrColOver(int value, out List<int> linkIndex)
    {
        for (int i = 0; i < 3; ++i)
        {
            if ((value == grids[i * 3]) && (value == grids[i * 3 + 1]) && (value == grids[i * 3 + 2]))
            {
                linkIndex = new List<int>() { grids[i * 3], grids[i * 3 + 1], grids[i * 3 + 2] };
                return true;
            }

            if ((value == grids[i]) && (value == grids[3 + i]) && (value == grids[6 + i]))
            {
                linkIndex = new List<int>() { grids[i], grids[3 + i], grids[6 + i] };
                return true;
            }
        }

        linkIndex = new List<int>();
        return false;
    }

    private bool IsHypoOver(int value, out List<int> linkIndex)
    {
        if ((value == grids[0]) && (value == grids[4]) && (value == grids[8]))
        {
            linkIndex = new List<int>() { grids[0], grids[4], grids[8] };
            return true;
        }

        if ((value == grids[2]) && (value == grids[4]) && (value == grids[6]))
        {
            linkIndex = new List<int>() { grids[2], grids[4], grids[6] };
            return true;
        }

        linkIndex = new List<int>();
        return false;
    }
}
