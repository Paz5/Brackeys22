using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private string interactable = "Interactable";
    [SerializeField] private string screen = "Screen";

    private void Update(){
        if(Input.GetMouseButtonDown(0)) Click();
        if(Input.GetMouseButton(0)) Drag();
    }

    private void Click(){
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,99f)){
            if (hit.transform.CompareTag(interactable)){
                hit.transform.GetComponent<Interactable>().Click();
            }

            if (hit.transform.CompareTag(screen)){
                ScreenClick(transform.GetComponent<InteractableScreen>());
            }
        }
    }

    private void Drag(){
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,99f)){
            if (hit.transform.CompareTag(interactable)){
                hit.transform.GetComponent<Interactable>().Drag();
            }

            if (hit.transform.CompareTag(screen)){
                ScreenDrag(transform.GetComponent<InteractableScreen>());
            }
        }
    }

    private void ScreenClick(InteractableScreen screen){
        if (Physics.Raycast(screen.camera.ViewportPointToRay(screen.pointerPos.Value), out RaycastHit hit, 99f)){
            if (hit.transform.CompareTag(interactable)){
                hit.transform.GetComponent<Interactable>().Click();
            }
        }
    }

    private void ScreenDrag(InteractableScreen screen){
        if (Physics.Raycast(screen.camera.ViewportPointToRay(screen.pointerPos.Value), out RaycastHit hit, 99f)){
            if (hit.transform.CompareTag(interactable)){
                hit.transform.GetComponent<Interactable>().Drag();
            }
        }
    }
}
