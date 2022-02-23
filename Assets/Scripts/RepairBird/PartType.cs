using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;
using System;

[Serializable]
public class PartType
{
    public HighlightEffect highlightEffect;
    public bool needRepair;
    public GameObject toHide;

    public void DamagedView(bool x)
    {
        if (toHide == null) return;

        if (x)
        {
            toHide.SetActive(false);
        }
        else
        {

        }

    }
}

public enum PartTypeEnum { WINGS, CAMERA, BATTERY };
