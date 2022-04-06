using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController : MonoBehaviour {

    public static FriendController instance;

    public static FriendController GetInstance() {
        return instance;
    }

    public GameObject bullet;
    public GameObject toAttack;

    public GameManager gameManager;

    public List<GameObject> allies;
    public List<GameObject> enemies;

    public Image defaultImage;
    public Sprite blinkImage;
    public Sprite defaultImageSprite;

    public bool isAttacking;
    public int Health;
    public int DamageValue;
    public float attackCooldown;

    private float attackTime;
    private bool quizPause;

    void Awake() {
        instance = this;
    }

    void Start() {
        gameManager = GameManager.instance;
        quizPause = GameManager.GetInstance().quizPause;
    }

    private void Update() {
        if (!quizPause) {
            if (enemies.Count > 0){  
                float distance = 1300;
                foreach (GameObject enemy in enemies) {
                    float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (enemyDistance < distance) {
                        toAttack = enemy;
                        distance = enemyDistance;
                    }
                }
            } else {
                toAttack = null;
            }

            if (toAttack != null) {
                if (attackTime <= Time.time) {
                    GameObject bulletInstance = Instantiate(bullet, transform);
                    bulletInstance.GetComponent<Bullet>().DamageValue = DamageValue;
                    attackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    public void ReceiveDamage(int Damage) { // friend receive damage
        if(Health - Damage <= 0) {
            Destroy(this.gameObject);
        } else {
            Health = Health - Damage;
            StartCoroutine(BlinkAlly(null, 0.15f, 1, false));
        }
    }

    public IEnumerator BlinkAlly(GameObject ally, float blinkDelay, int timesToBlink, bool destroy){
        for (int i = 0; i < timesToBlink; i++){
            defaultImage.sprite = blinkImage;
            yield return new WaitForSeconds(blinkDelay);
            defaultImage.sprite = defaultImageSprite;
            yield return new WaitForSeconds(blinkDelay);
        }
        if(destroy){
            Destroy(ally);
        }
    }
    
}