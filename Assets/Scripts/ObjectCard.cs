using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

    private GameManager gameManager;
    private GameObject objectDragInstance;
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;

    private int MAX_ALLIES = 5;

    private void Start() {
        gameManager = GameManager.instance; 
    }

    public void OnDrag(PointerEventData eventData) {
        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        
        if (gameManager.currentAllies < MAX_ALLIES) {
            objectDragInstance.transform.position = Input.mousePosition;
            objectDragInstance.GetComponent<ObjectDragging>().card = this;
            
            gameManager.draggingObject = objectDragInstance; 
        } else {
            Debug.Log("ERRO!");
        }

    }

    public void OnPointerUp(PointerEventData eventData) {
        gameManager.PlaceObject();
        gameManager.draggingObject = null; 
        Destroy(objectDragInstance);
    }
}