using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public float movementSpeed;
    public int DamageValue;

    private bool quizPause;
    private const int DESTROY_X_POSITION = 2100;

    void Update() {
        quizPause = GameManager.GetInstance().quizPause;
        if(!quizPause){
            transform.Translate(new Vector3(movementSpeed, 0, 0));
            if (transform.position.x > DESTROY_X_POSITION){
                Destroy(this.gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 11){
            collision.gameObject.GetComponent<EnemyController>().ReceiveDamage(DamageValue);
            Destroy(this.gameObject);
        }    
    }

}
