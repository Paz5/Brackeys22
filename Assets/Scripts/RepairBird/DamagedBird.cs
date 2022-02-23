using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using DG.Tweening;

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
            RepairAnimation();
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

            pt.DamagedView(pt.needRepair);
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

    public GameObject headPrefab;
    public Transform headParent;
    public Transform headBox;
    public Transform localPosStart;
    public Transform localPosEnd;
    public Vector3 headRotate;
    public GameObject animatedObject;
    public float firstMovementDuration;
    public float secondMovementDuration;

    
    private void RepairAnimation()
    {
        headBox = RepairManager.Instance.headBox;
        animatedObject = Instantiate(headPrefab, headBox.position, localPosStart.rotation);
        animatedObject.transform.SetParent(headParent,false);
        animatedObject.transform.position = headBox.position;
        animatedObject.transform.DOLocalMove(localPosStart.localPosition,firstMovementDuration).OnComplete(LastPartOfAnim);
    }

    private void LastPartOfAnim()
    {
        animatedObject.transform.DOLocalMove(localPosEnd.localPosition, secondMovementDuration);
        animatedObject.transform.DOLocalRotate(headRotate, secondMovementDuration,RotateMode.LocalAxisAdd);
    }
}
