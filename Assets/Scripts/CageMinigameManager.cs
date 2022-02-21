using System.Collections.Generic;
using ScriptableObjectArchitecture;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CageMinigameManager : MonoBehaviour{

    [SerializeField] private InteractableScreen screen;
    [SerializeField] private List<GameObject> birdTypes;
    private ObjectPool<GameObject> birdPool;
    [SerializeField] private Transform spawnPos;

    void Start(){
        birdPool = new ObjectPool<GameObject>(
            () => CreateBird(),
            bird => { bird.SetActive(true); },
            bird => { bird.SetActive(false); },
            bird => { Destroy(bird); },
            false,
            5,
            5);
    }
    
    private GameObject CreateBird(){
        var bird = Instantiate(birdTypes[Random.Range(0, birdTypes.Count)],spawnPos.position,quaternion.identity).GetComponent<DraggableBird>();
        bird.screen = screen;
        return bird.gameObject;
    }

    [SerializeField] private FloatVariable spawnDelay;
    private float t = 0;
    
    private void Update(){
        t += Time.deltaTime;
        if (t > spawnDelay.Value){
            birdPool.Get();
            t = 0;
        }
            
    }
    

}
