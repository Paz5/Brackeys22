using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Mission",menuName = "Mission")]
public class Mission : ScriptableObject{
    public Sprite missionIcon;
    public Sprite leftButtonIcon;
    public Sprite rightButtonIcon;
    public string MissionTitle;
    public bool leftIsSucced;
}
