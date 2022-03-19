using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public Vector3 FinalDestination;
    public int DamageValue;
    public int Health;
    public float DamageCooldown;
    public float movementSpeed;
    private bool isStopped;

    void Update() {
        if(!isStopped) {
            transform.Translate(new Vector3(movementSpeed * -1, 0, 0));
        }
    }
   
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 10){
            StartCoroutine(Attack(collision));
            isStopped = true;
        }           
    }
    
    IEnumerator Attack(Collider2D collision) {
        if (collision == null) {
            isStopped = false;
        } else {
            collision.gameObject.GetComponent<FriendController>().ReceiveDamage(DamageValue);
            
            yield return new WaitForSeconds(DamageCooldown);

            StartCoroutine(Attack(collision));
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
