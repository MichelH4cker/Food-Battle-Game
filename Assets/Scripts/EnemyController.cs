using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public Vector3 FinalDestination;
    public int Health;
    public float movementSpeed;
    private bool isStopped;

    void Update() {
        if(!isStopped) {
            transform.Translate(new Vector3(movementSpeed * -1, 0, 0));
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 10){
            isStopped = true;
        }           
    }
    
    public void ReceiveDamage(int Damage) {
        if(Health - Damage <= 0) {
            // enemy is DEAD
            transform.parent.GetComponent<SpawnPoint>().enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        } else {
            // enemy is not DEAD, needs receive damage 
            Health = Health - Damage;
        }
    }

}
