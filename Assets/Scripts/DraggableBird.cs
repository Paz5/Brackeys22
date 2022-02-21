using System;
using System.Collections;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Collider))]
public class DraggableBird : MonoBehaviour{
    [HideInInspector] public CageMinigameManager manager;
    [HideInInspector] public ObjectPool<GameObject> pool;
    [SerializeField] private GameEvent succesEvent;
    [SerializeField] private GameEvent failEvent;
    
    [HideInInspector] public InteractableScreen screen;
    [SerializeField] private SmoothFollow smoothFollow;
    private Plane dragPlane;
    private Vector3 dragStartPos = Vector3.zero;
    private bool alreadySpawned = false;
    private bool draggable = true;

    private void OnEnable(){
        if (alreadySpawned){
            transform.position = manager.GetWaitPos();
            smoothFollow.SetPosition(manager.GetSpawnPos());
            correctPosition = false;
            draggable = true;
            dragStartPos = Vector3.zero;
        }
    }

    private void Start(){
        transform.position = manager.GetWaitPos();
        smoothFollow.SetPosition(manager.GetSpawnPos());
        dragPlane = new Plane(-screen.camera.transform.forward, transform.position);
        alreadySpawned = true;
    }

    public void StartDrag(){
        dragStartPos = transform.position;
    }

    public void MoveToMouse(){
        if (draggable){
            Ray ray = screen.camera.ViewportPointToRay(screen.pointerPos.Value);
            dragPlane.Raycast(ray, out float enter);
            Vector3 pos = enter * ray.direction + screen.camera.transform.position;
            transform.position =  pos;
        }
    }

    public void EndDrag(){
        if (draggable){
            if (correctPosition){
                succesEvent.Raise();
                transform.position = targetPos;
            }
            else{
                Fail();
                transform.position += Vector3.up * 10f;
            }
            StartCoroutine(Disappear());
            draggable = false;
        }
    }

    private void Fail(){
        failEvent.Raise();
    }

    IEnumerator Disappear(){
        yield return new WaitForSeconds(1f);
        pool.Release(gameObject);
    }

    private bool correctPosition = false;
    private Vector3 targetPos = Vector3.zero;
    
    private void OnTriggerEnter(Collider other){
        correctPosition = true;
        targetPos = other.transform.position;
    }

    private void OnTriggerExit(Collider other){
        correctPosition = false;
    }
}
