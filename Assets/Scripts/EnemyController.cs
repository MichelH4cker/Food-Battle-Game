using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public Vector3 FinalDestination;
    public int Health;
    public int Damage;
    public float movementSpeed;

    void Update() {
         transform.Translate(new Vector3(movementSpeed * -1, 0, 0));
    }
}
