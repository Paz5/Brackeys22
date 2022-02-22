using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using ScriptableObjectArchitecture;

public class ScoreSystem : MonoBehaviour
{
    public MainUiReferences mainUiReferences;
    public GameEvent failEvent;
    private float score;
    private float suspicionMeter;
    private float suspicionCoolDown;
    private bool suspicionCoolingDown;
    public float suspicionCoolDownTime = 1f;
    public float maxSuspicionMeter = 100;
    public MMFeedbacks scoreSound;

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

    public void Restart()
    {
        OnRestartDo();
    }

    private void OnRestartDo()
    {
        score = 0;
        suspicionMeter = 0;
        ChangeSusMeter(0);
        mainUiReferences.scoreText.text = "0";
    }

    private void OnFailedDo(float suspicion)
    {
        ChangeSusMeter(suspicionMeter += suspicion);
        suspicionCoolDown = suspicionCoolDownTime;

        if (maxSuspicionMeter <= suspicionMeter)
        {
            OnEndDo();
        }
    }

    private void OnSuccessDo(float score)
    {
        this.score += score;
        mainUiReferences.scoreText.text = "" + this.score;
        scoreSound.PlayFeedbacks();
    }

    private void OnEndDo()
    {
        mainUiReferences.endGamePanel.SetActive(true);
        mainUiReferences.endScoreText.text = score+"";
    }

    private void ChangeSusMeter(float value)
    {
        suspicionMeter = value;
        mainUiReferences.susBar.UpdateBar(suspicionMeter, 0, maxSuspicionMeter);
    }

    private void SusBarHandling()
    {
        if (suspicionCoolDown > 0)
        {
            suspicionCoolingDown = true;
            suspicionCoolDown -= Time.deltaTime;
            mainUiReferences.susCube.fillAmount = suspicionCoolDown / suspicionCoolDownTime;
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
