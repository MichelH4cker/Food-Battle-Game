using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

    private GameObject objectDragInstance;
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;

    public void OnDrag(PointerEventData eventData) {
        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }
}