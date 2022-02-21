using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairManager : MonoBehaviourSingleton<RepairManager>
{
    public DamagedBird birdUnderRepair;

    public void SetBird(DamagedBird db)
    {
        if (db == birdUnderRepair) return;

        if(birdUnderRepair!=null) birdUnderRepair.DisableHighlights();

        birdUnderRepair = db;
    }

    public void RemoveBird()
    {
        birdUnderRepair.DisableHighlights();
        birdUnderRepair = null;
    }

    public void RepairBird(PartTypeEnum partTypeEnum)
    {
        birdUnderRepair.Repair(partTypeEnum);
    }

}
