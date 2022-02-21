using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairManager : MonoBehaviourSingleton<RepairManager>
{
    public DamagedBird birdUnderRepair;
    public Transform repairButtonPanel;

    public void SetBird(DamagedBird db)
    {
        if (db == birdUnderRepair) return;

        if(birdUnderRepair!=null) birdUnderRepair.DisableHighlights();

        birdUnderRepair = db;
        repairButtonPanel.gameObject.SetActive(true);
    }

    public void RemoveBird()
    {
        birdUnderRepair.DisableHighlights();
        birdUnderRepair = null;
        repairButtonPanel.gameObject.SetActive(false);
    }

    //ale wstyd te funkcje co ;(( trzebaby przepisac na nieuposledzone
    public void RepairWings()
    {
        birdUnderRepair.wings.needRepair = false;
        birdUnderRepair.wings.highlightEffect.outlineColor = birdUnderRepair.unDamaged;
    }

    public void RepairCamera()
    {
        birdUnderRepair.camera.needRepair = false;
        birdUnderRepair.camera.highlightEffect.outlineColor = birdUnderRepair.unDamaged;
    }

    public void RepairBattery()
    {
        birdUnderRepair.battery.needRepair = false;
        birdUnderRepair.battery.highlightEffect.outlineColor = birdUnderRepair.unDamaged;
    }
}
