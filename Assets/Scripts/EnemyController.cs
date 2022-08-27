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
    private bool leftMap;
    private bool quizPause;

    private int RemainingHeartsInt;
    private int Health = 5;
    private const float MOVEMENT_SPEED = 60f;
    private const float BLINK_DELAY = 0.15f;

    void Awake() {
        instance = this;
        HealthText.text = "x" + Health;
    }

    void Update() {   
        quizPause = GameManager.GetInstance().quizPause;
        if(!isStopped && !quizPause) {
            transform.position += new Vector3(-1, 0, 0) * MOVEMENT_SPEED * Time.deltaTime * .7f;
            //transform.Translate(new Vector3(MOVEMENT_SPEED * -1, 0, 0));
        }

        if (leftMap == true) {
            Destroy(this.gameObject);
            SceneLoader.Load(SceneLoader.Scene.EndScene);
        }
    }
   
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 10){
            StartCoroutine(Attack(collision));
            isStopped = true;
        } else if (collision.gameObject.layer == 15){
            leftMap = true;
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
            SoundManager.PlaySound(SoundManager.Sound.UnhealthyDied);
            transform.parent.GetComponent<SpawnPoint>().enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        } else {
            SoundManager.PlaySound(SoundManager.Sound.UnhealthyReceiveDamage);
            Health = Health - Damage;
            HealthText.text = "x" + Health;
            StartCoroutine(BlinkEnemy(1, false));
        }
    }

    public void DestroyEnemy(){
        StartCoroutine(BlinkEnemy(3, true));
    }

    public IEnumerator BlinkEnemy(int timesToBlink, bool destroy){
        if(destroy){
            yield return new WaitForSeconds(2f);
        }
        for (int i = 0; i < timesToBlink; i++){
            defaultImage.sprite = blinkImage;
            yield return new WaitForSeconds(BLINK_DELAY);
            defaultImage.sprite = defaultImageSprite;
            yield return new WaitForSeconds(BLINK_DELAY);
        }
        if(destroy){
            Destroy(this.gameObject);
        }
    }
}
