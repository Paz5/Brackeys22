using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;

public class ScoreSystem : MonoBehaviour
{
    public MMProgressBar susBar;
    public Image susCube;
    private float score;
    private float suspicionMeter;
    private float suspicionCoolDown;
    private bool suspicionCoolingDown;
    public float suspicionCoolDownTime = 1f;
    public float maxSuspicionMeter = 100;

    [ContextMenu("Fail")]
    public void Fail()
    {
        OnFailedDo(10);
    }

    [ContextMenu("Succed")]
    public void Succed()
    {
        OnSuccessDo(1);
    }

    private void OnRestartDo()
    {
        score = 0;
        suspicionMeter = 0;
    }

    private void OnFailedDo(float suspicion)
    {
        ChangeSusMeter(suspicionMeter += suspicion);
        suspicionCoolDown = suspicionCoolDownTime;

        if (maxSuspicionMeter < suspicionMeter)
        {
            OnEndDo();
        }
    }

    private void OnSuccessDo(float score)
    {
        this.score += score;
    }

    private void OnEndDo()
    {
        OnRestartDo();
    }

    private void ChangeSusMeter(float value)
    {
        suspicionMeter = value;
        susBar.UpdateBar(suspicionMeter, 0, maxSuspicionMeter);
    }

    private void SusBarHandling()
    {
        if (suspicionCoolDown > 0)
        {
            suspicionCoolingDown = true;
            suspicionCoolDown -= Time.deltaTime;
            susCube.fillAmount = suspicionCoolDown / suspicionCoolDownTime;
        }
        else
        {
            if (suspicionCoolingDown)
            {
                ChangeSusMeter(0);
                suspicionCoolingDown = false;
            }
        }

        
    }

    private void Update()
    {
        SusBarHandling();
    }
}
