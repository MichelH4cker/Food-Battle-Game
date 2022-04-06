using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public static EnemyController instance;

    public static EnemyController GetInstance() {
        return instance;
    }

    public Text HealthText;

    public Image defaultImage;
    public Sprite blinkImage;
    public Sprite defaultImageSprite;

    public int DamageValue;
    public float DamageCooldown;    
    
    private bool isStopped;
    private bool quizPause;

    private int RemainingHeartsInt;
    private int Health = 5;
    private const int DESTROY_X_POSITION = -150;
    private const float MOVEMENT_SPEED = 0.3f;

    void Awake() {
        instance = this;
        HealthText.text = "x" + Health;
    }

    void Update() {   
        quizPause = GameManager.GetInstance().quizPause;
        if(!isStopped && !quizPause) {
            transform.Translate(new Vector3(MOVEMENT_SPEED * -1, 0, 0));
        }
        
        if (LeftTheMap()) {
            Destroy(this.gameObject);
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
            HealthText.text = "x" + Health;
            StartCoroutine(BlinkEnemy(null, 0.15f, 1, false));
        }
    }

    public IEnumerator BlinkEnemy(GameObject enemy, float blinkDelay, int timesToBlink, bool destroy){
        for (int i = 0; i < timesToBlink; i++){
            defaultImage.sprite = blinkImage;
            yield return new WaitForSeconds(blinkDelay);
            defaultImage.sprite = defaultImageSprite;
            yield return new WaitForSeconds(blinkDelay);
        }
        if(destroy){
            Destroy(enemy);
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
