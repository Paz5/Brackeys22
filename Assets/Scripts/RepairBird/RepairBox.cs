using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class RepairBox : MonoBehaviour
{
    public bool mouseIsOver;
    public bool freshPress;
    public float partHoldTime;
    private float partHoldTimer;
    public PartTypeEnum partTypeEnum;
    public MMFeedbacks feedbacks;
    public Image progressCircle;

    private void Update()
    {
        if (mouseIsOver)
        {
            if (RepairManager.Instance.birdUnderRepair != null)
            {

                if (Input.GetMouseButton(0))
                {
                    if (freshPress)
                    {
                        if (partHoldTimer > 0)
                        {
                            partHoldTimer -= Time.deltaTime;
                            progressCircle.fillAmount = (partHoldTime - partHoldTimer) / partHoldTime;
                        }
                        else
                        {
                            RepairManager.Instance.RepairBird(partTypeEnum);
                            freshPress = false;
                            mouseIsOver = false;
                            feedbacks.StopFeedbacks();
                        }
                    }

                }
                else
                {
                    StopRepairing();
                    freshPress = true;
                }
            }
            else
            {
                StopRepairing();
            }
        }
    }

    private void StopRepairing()
    {
        progressCircle.fillAmount = 0;
        mouseIsOver = false;
        feedbacks.StopFeedbacks();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (!mouseIsOver)
            {
                if (RepairManager.Instance.birdUnderRepair != null)
                {
                    progressCircle.fillAmount = 0;
                    partHoldTimer = partHoldTime;
                    mouseIsOver = true;
                    feedbacks.PlayFeedbacks();
                }
            }
        }
    }

    void OnMouseExit()
    {
        if (mouseIsOver)
        {
            StopRepairing();
        }
    }
}
