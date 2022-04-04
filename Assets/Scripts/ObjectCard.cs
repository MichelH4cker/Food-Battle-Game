using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

    private GameManager gameManager;
    private GameObject objectDragInstance;
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;
    public Text RemainingAllyText;

    public int MAX_ALLY;
    int currentNumberAlly;

    private void Start() {
        RemainingAllyText.text = MAX_ALLY + " X";
        currentNumberAlly = MAX_ALLY;
        gameManager = GameManager.instance; 

    }

    public void OnDrag(PointerEventData eventData) {
        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        
        if (gameManager.currentAllies < MAX_ALLY) {
            objectDragInstance.transform.position = Input.mousePosition;
            objectDragInstance.GetComponent<ObjectDragging>().card = this;
            
            gameManager.draggingObject = objectDragInstance; 
            
            currentNumberAlly--;
            RemainingAllyText.text = currentNumberAlly + " X";
            
            Debug.Log("Máximo de aliados nessa carta é de: " + MAX_ALLY);
        } else {
            Debug.Log("ERRO!");
        }

    }

    public void OnPointerUp(PointerEventData eventData) {
        gameManager.PlaceObject();
        gameManager.draggingObject = null; 
        Destroy(objectDragInstance);
        
        if (GameManager.GetInstance().positioned == false){
            currentNumberAlly++;
            RemainingAllyText.text = currentNumberAlly + " X";
        }
    }
}