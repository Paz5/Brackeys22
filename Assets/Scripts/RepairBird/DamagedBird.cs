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
            if (!battery.needRepair) return;
            battery.needRepair = false;
            battery.repairedHighlightEffect.outlineColor = unDamaged;
            
            BatteryRepairAnimation();
        }
        if (partTypeEnum == PartTypeEnum.CAMERA)
        {
            if (!camera.needRepair) return;
            camera.needRepair = false;
            camera.repairedHighlightEffect.outlineColor = unDamaged;
            
            HeadRepairAnimation();
        }
        if (partTypeEnum == PartTypeEnum.WINGS)
        {
            if (!wings.needRepair) return;
            wings.needRepair = false;
            wings.repairedHighlightEffect.outlineColor = unDamaged;
            wings.DamagedView(false);
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
            pt.repairedHighlightEffect.SetHighlighted(true);
            if(pt.needRepair) pt.damagedHighlightEffect.SetHighlighted(true);

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
            pt.repairedHighlightEffect.SetHighlighted(false);
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
                pt.repairedHighlightEffect.outlineColor = damaged;
            }
            else
            {
                pt.repairedHighlightEffect.outlineColor = unDamaged;
            }
        }
    }

    [Header("battery")]
    public GameObject batteryPrefab;
    public Transform batteryParent;
    public Transform batteryBox;
    public Transform batLocalPosStart;
    public Transform batLocalPosEnd;
    public Vector3 batRotate;
    public GameObject animatedBattery;
    public float batFirstMovementDuration;
    public float batSecondMovementDuration;

    private void BatteryRepairAnimation()
    {
        Bra1();
    }

    private void Bra1()
    {
        batteryBox = RepairManager.Instance.batteryBox;
        animatedBattery = Instantiate(batteryPrefab, batteryBox.position, localPosStart.rotation);
        animatedBattery.transform.SetParent(batteryParent, false);
        animatedBattery.transform.position = batteryBox.position;
        animatedBattery.transform.DOLocalMove(batLocalPosStart.localPosition, batFirstMovementDuration).OnComplete(Bra2);
    }

    private void Bra2()
    {
        animatedBattery.transform.DOLocalMove(batLocalPosEnd.localPosition, batSecondMovementDuration);
        animatedBattery.transform.DOLocalRotate(batRotate, batSecondMovementDuration, RotateMode.LocalAxisAdd).OnComplete(Bra3);
    }

    private void Bra3()
    {
        //klapka zamyka sie
        BraCompleted();
    }

    private void BraCompleted()
    {
        battery.DamagedView(false);
    }

    [Header("camera")]
    public GameObject headPrefab;
    public Transform headParent;
    public Transform headBox;
    public Transform localPosStart;
    public Transform localPosEnd;
    public Vector3 headRotate;
    public GameObject animatedCamera;
    public float firstMovementDuration;
    public float secondMovementDuration;

    private void HeadRepairAnimation()
    {
        Hra1();
    }

    private void Hra1()
    {
        headBox = RepairManager.Instance.headBox;
        animatedCamera = Instantiate(headPrefab, headBox.position, localPosStart.rotation);
        animatedCamera.transform.SetParent(headParent,false);
        animatedCamera.transform.position = headBox.position;
        animatedCamera.transform.DOLocalMove(localPosStart.localPosition,firstMovementDuration).OnComplete(Hra2);
    }

    private void Hra2()
    {
        animatedCamera.transform.DOLocalMove(localPosEnd.localPosition, secondMovementDuration);
        animatedCamera.transform.DOLocalRotate(headRotate, secondMovementDuration,RotateMode.LocalAxisAdd).OnComplete(HraCompleted);
    }

    private void HraCompleted()
    {
        camera.DamagedView(false);
    }
}
