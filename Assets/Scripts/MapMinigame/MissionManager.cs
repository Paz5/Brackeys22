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
    [SerializeField] private GameEvent resetEvent;

    [SerializeField] private FloatVariable spawnDelay;
    private float t = 0;

    void Start(){
        resetEvent.AddListener(GameReset);
    }

    void GameReset(){
        t = 0;
    }

    void Update(){
        t += Time.deltaTime;
        if (t > spawnDelay.Value){
            Spawn();
            t = 0;
        }
    }

    void Spawn(){
        Vector2 pos = new Vector2(Random.Range(-.3f,.3f),
                                  Random.Range(-.25f,.3f));
        var obj = Instantiate(missionUIprefab).GetComponent<RectTransform>();
        obj.SetParent(canvas.transform,false);
        obj.anchoredPosition = pos;
        obj.GetComponent<MapMission>().SetParams(missions[0]);
    }
}
