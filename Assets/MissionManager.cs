using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissionManager : MonoBehaviour{
    [SerializeField] private List<Mission> missions;
    [SerializeField] private GameObject missionUIprefab;
    [SerializeField] private GameObject map;
    [SerializeField] private RectTransform canvas;

    [SerializeField] private FloatVariable missionSpawnDelay;
    private float t = 0;

    void Update(){
        t += Time.deltaTime;
        if (t > missionSpawnDelay.Value){
            Spawn();
            t = 0;
        }
    }

    void Spawn(){
        Vector2 pos = new Vector2(Random.Range(-.4f,.4f),
                                  Random.Range(-.4f,.4f));
        var obj = Instantiate(missionUIprefab).GetComponent<RectTransform>();
        obj.SetParent(canvas.transform,false);
        obj.anchoredPosition = pos;
        obj.GetComponent<MapMission>().SetParams(missions[0]);
    }
}
