using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public  int DamageValue;
    public  int Health;
    public  float DamageCooldown;    
    
    private bool isStopped;

    private const int DESTROY_X_POSITION = -150;
    private const float MOVEMENT_SPEED = 0.3f;

    void Update() {   
        if(!isStopped) {
            transform.Translate(new Vector3(MOVEMENT_SPEED * -1, 0, 0));
        }
        
        if (LeftTheMap()) {
            Destroy(this.gameObject);
        }
    }
   
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 10){
            Debug.Log(transform.position);
            StartCoroutine(Attack(collision));
            isStopped = true;
        }    
    }
    
    IEnumerator Attack(Collider2D collision) {
        if (collision == null) {
            isStopped = false;
        } else {
            collision.gameObject.GetComponent<FriendController>().ReceiveDamage(DamageValue); // friend receive damage
            yield return new WaitForSeconds(DamageCooldown);
            StartCoroutine(Attack(collision));
        }
    }
    
    public void ReceiveDamage(int Damage) { // enemy receive damage
        if(Health - Damage <= 0) {
            transform.parent.GetComponent<SpawnPoint>().enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        } else {
            Health = Health - Damage;
        }
    }

    private bool LeftTheMap() {
        if (transform.position.x < DESTROY_X_POSITION) {
            return true;
        } else {
            return false;
        }
    }

}
