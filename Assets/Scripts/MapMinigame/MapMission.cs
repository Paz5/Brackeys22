using System;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapMission : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private Image missionIcon;
    [SerializeField] private TextMeshProUGUI missionTitle;

    [SerializeField] private GameEvent succeedEvent;
    [SerializeField] private GameEvent failEvent;
    [SerializeField] private GameEvent resetEvent;
    private bool leftIsSucced;

    private void Start(){
        resetEvent.AddListener(GameReset);
    }

    private void OnDestroy(){
        resetEvent.RemoveListener(GameReset);
    }

    private void GameReset(){
        Destroy(gameObject);
    }

    public void SetParams(Mission missionObject){
        leftButton.image.sprite = missionObject.leftButtonIcon;
        rightButton.image.sprite = missionObject.rightButtonIcon;

        missionIcon.sprite = missionObject.missionIcon;
        missionTitle.text = missionObject.MissionTitle;
        leftIsSucced = missionObject.leftIsSucced;
    }

    public void ButtonPressLeft(){
        if(leftIsSucced) Succeed();
        else Fail();
    }

    public void ButtonPressRight(){
        if(!leftIsSucced) Succeed();
        else Fail();
    }

    private void Succeed(){
        succeedEvent.Raise();
        Destroy(gameObject);
    }

    private void Fail(){
        failEvent.Raise();
        Destroy(gameObject);
    }
}
