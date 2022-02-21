using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class DamagedBird : MonoBehaviour
{
    public PartType battery;
    public PartType camera;
    public PartType wings;
    private List<PartType> parts = new List<PartType>();

    public Color damaged;
    public Color unDamaged;//throw to scirptable

    public GameEvent startRepairEvent;

    public void Repair(PartTypeEnum partTypeEnum)
    {
        if (partTypeEnum == PartTypeEnum.BATTERY)
        {
            battery.needRepair = false;
            battery.highlightEffect.outlineColor = unDamaged;
        }
        if (partTypeEnum == PartTypeEnum.CAMERA)
        {
            camera.needRepair = false;
            camera.highlightEffect.outlineColor = unDamaged;
        }
        if (partTypeEnum == PartTypeEnum.WINGS)
        {
            wings.needRepair = false;
            wings.highlightEffect.outlineColor = unDamaged;
        }

    }

    public bool IsDamaged()
    {
        foreach (PartType pt in parts)
        {
            if (pt.needRepair) return true;
        }
        return false;
    }

    public void OnClick()
    {
        startRepairEvent.Raise();
        foreach (PartType pt in parts)
        {
            pt.highlightEffect.SetHighlighted(true);
        }
        RepairManager.Instance.SetBird(this);
    }

    public void OnRepairEnd()
    {
        DisableHighlights();
    }

    public void DisableHighlights()
    {
        foreach (PartType pt in parts)
        {
            pt.highlightEffect.SetHighlighted(false);
        }
    }

    public void Initialize()
    {
        parts.Add(battery);
        parts.Add(camera);
        parts.Add(wings);

        foreach (PartType pt in parts)
        {
            if(Random.Range(0,2)>0) pt.needRepair = false;
            else pt.needRepair = true;


            if (pt.needRepair)
            {
                pt.highlightEffect.outlineColor = damaged;
            }
            else
            {
                pt.highlightEffect.outlineColor = unDamaged;
            }
        }
    }
}
