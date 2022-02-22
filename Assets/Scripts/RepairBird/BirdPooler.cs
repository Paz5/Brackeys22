using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using DG.Tweening;

public class BirdPooler : MonoBehaviour
{
    public GameEvent failed;
    public GameEvent success;
    public GameObject birdPrefab;
    public Transform birdSpawnPosition;
    public Transform birdDespawnPosition;
    public float despawnDistance = 0.1f;
    public List<DamagedBird> birdsToRepair = new List<DamagedBird>();
    private float coolDownTimer;
    public ScrollTexture scrollTexture;
    public float offsetForTextureScroll;
    
    [SerializeField] private FloatVariable spawnDelay;
    [SerializeField] private FloatVariable timeWindow;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DespawnBirds();
        PoolBirds();
        scrollTexture.scroll.y = scrollTexture.scroll.y + (Time.deltaTime * (1/timeWindow.Value) * offsetForTextureScroll);
    }

    public void OnRestartDo()
    {
        for (int i = birdsToRepair.Count - 1; i >= 0; i--)
        {
            Destroy(birdsToRepair[i].gameObject);
            birdsToRepair.Remove(birdsToRepair[i]);
        }
    }

    private void PoolBirds()//rewrite into pooling for performance
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else
        {
            SpawnBird();
            coolDownTimer = spawnDelay.Value;
        }
    }

    private void DespawnBirds()
    {
        for (int i = birdsToRepair.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(birdsToRepair[i].transform.position, birdDespawnPosition.position) < despawnDistance)
            {
                if (birdsToRepair[i].IsDamaged())
                {
                    failed.Raise();
                }
                else
                {
                    success.Raise();
                }
                Destroy(birdsToRepair[i].gameObject);
                birdsToRepair.Remove(birdsToRepair[i]);
            }
        }
    }

    private void SpawnBird()
    {
        DamagedBird db = Instantiate(birdPrefab, birdSpawnPosition.position, birdSpawnPosition.rotation).GetComponent<DamagedBird>();
        db.Initialize();
        birdsToRepair.Add(db);
        db.transform.DOMove(birdDespawnPosition.position, timeWindow.Value).SetEase(Ease.Linear);
    }
}
