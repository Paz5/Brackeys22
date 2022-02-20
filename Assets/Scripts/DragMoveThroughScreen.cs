using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMoveThroughScreen : MonoBehaviour{
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
        transform.position =  pos;//dragPlane.ClosestPointOnPlane(pos);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,dragPlane.normal);
    }
}
