using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject draggingObject;
    public GameObject currentContainer;

    public int currentAllies = 0;

    public static GameManager instance;

    void Awake() {
        instance = this;
    }

    public void PlaceObject() {
        if (draggingObject != null && currentContainer != null) {
            GameObject objectGame = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.objectGame, currentContainer.transform);
            objectGame.GetComponent<FriendController>().enemies = currentContainer.GetComponent<ObjectContainer>().spawnPoint.enemies;
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
            currentAllies++;
        }
    }

}