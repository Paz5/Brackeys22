using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour{
    
    [SerializeField] private UnityEvent clickEvent;
    [SerializeField] private UnityEvent startDragEvent;
    [SerializeField] private UnityEvent dragEvent;
    [SerializeField] private UnityEvent endDragEvent;

    private bool dragging = false;
    private int framesSinceDrag = 0;
    
    private void Update(){
        if(framesSinceDrag>0) EndDrag();
        framesSinceDrag++;
        
    }
    
    public void Click(){
        clickEvent.Invoke();
    }

    public void Drag(){
        if (dragging == false){
            StartDrag();
        }
        else{
            dragEvent.Invoke();
        }
        framesSinceDrag = 0;
    }

    private void StartDrag(){
        dragging = true;
        startDragEvent.Invoke();
    }

    private void EndDrag(){
        dragging = false;
        endDragEvent.Invoke();
    }
}
