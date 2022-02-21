using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapMission : MonoBehaviour
{
    [SerializeField] private Mission missionObject;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private Image missionIcon;
    [SerializeField] private TextMeshProUGUI missionTitle;

    [SerializeField] private GameEvent succeedEvent;
    [SerializeField] private GameEvent failEvent;
    
    private void Start(){
        leftButton.image.sprite = missionObject.leftButtonIcon;
        rightButton.image.sprite = missionObject.rightButtonIcon;

        missionIcon.sprite = missionObject.missionIcon;
        missionTitle.text = missionObject.MissionTitle;
    }

    public void ButtonPressLeft(){
        if(missionObject.leftIsSucced) Succeed();
        else Fail();
    }

    public void ButtonPressRight(){
        if(!missionObject.leftIsSucced) Succeed();
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
