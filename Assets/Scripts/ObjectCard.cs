using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

    public static ObjectCard instance;

    public static ObjectCard GetInstance() {
        return instance;
    }

    private GameManager gameManager;
    private GameObject objectDragInstance;
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;
    public Text RemainingAllyText;

    public int MAX_ALLY;
    public int alliesLeft;

    void Awake(){
        instance = this;
    }

    private void Start() {
        RemainingAllyText.text = MAX_ALLY + " X";
        alliesLeft = MAX_ALLY;
        gameManager = GameManager.instance; 
    }

    public void OnDrag(PointerEventData eventData) {
        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        
        if (alliesLeft > 0) {
            objectDragInstance.transform.position = Input.mousePosition;
            objectDragInstance.GetComponent<ObjectDragging>().card = this;
            
            gameManager.draggingObject = objectDragInstance; 
            
            if(GameManager.GetInstance().positioned || alliesLeft == MAX_ALLY){
                alliesLeft--;
                RemainingAllyText.text = alliesLeft + " X";
            }
        } else {
            Debug.Log("ERRO!");
            GameDetails.GetInstance().ShowErrorMessage();
        }

    }

    public void OnPointerUp(PointerEventData eventData) {
        if (alliesLeft >= 0) {
            gameManager.PlaceObject();
        }

        Destroy(objectDragInstance);
        gameManager.draggingObject = null; 
        /*
        if (GameManager.GetInstance().positioned == false){
            alliesLeft++;
            RemainingAllyText.text = alliesLeft + " X";
        }*/
    }
}