using System;
using ScriptableObjectArchitecture;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DraggableBirds : MonoBehaviour
{
    [SerializeField] private GameEvent succesEvent;
    [SerializeField] private GameEvent failEvent;
    
    [SerializeField] private InteractableScreen screen;
    private Plane dragPlane;
    private Vector3 dragStartPos = Vector3.zero;

    private void Start(){
        dragPlane = new Plane(-screen.camera.transform.forward, transform.position);
    }

    public void StartDrag(){
        dragStartPos = transform.position;
    }

    public void MoveToMouse(){
        Ray ray = screen.camera.ViewportPointToRay(screen.pointerPos.Value);
        dragPlane.Raycast(ray, out float enter);
        Vector3 pos = enter * ray.direction + screen.camera.transform.position;
        transform.position =  pos;
    }

    public void EndDrag(){
        if (correctPosition){
            succesEvent.Raise();
            Destroy(gameObject);
        }
        else{
            failEvent.Raise();
            transform.position = dragStartPos;
        }
    }

    private bool correctPosition = false;
    
    private void OnTriggerEnter(Collider other){
        correctPosition = true;
    }

    private void OnTriggerExit(Collider other){
        correctPosition = false;
    }
}