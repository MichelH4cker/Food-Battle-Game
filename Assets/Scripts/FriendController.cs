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

    public List<GameObject> enemies;

    public Text HealthText;

    public Image defaultImage;
    public Sprite blinkImage;
    public Sprite defaultImageSprite;

    public bool isAttacking;
    public float attackCooldown;

    public int Health;
    public int DamageValue;
    private float attackTime;
    private bool quizPause;
    private const float BLINK_DELAY = 0.15f;

    void Awake() {
        instance = this;
        HealthText.text = Health + "X";
    }

    void Start() {
        gameManager = GameManager.instance;
        quizPause = GameManager.GetInstance().quizPause;
    }

    private void Update() {
        quizPause = GameManager.GetInstance().quizPause;
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
                    SoundManager.PlaySound(SoundManager.Sound.Shoot);
                    GameObject bulletInstance = Instantiate(bullet, transform);
                    bulletInstance.GetComponent<Bullet>().DamageValue = DamageValue;
                    attackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    public void ReceiveDamage(int Damage) { // friend receive damage
        if(Health - Damage <= 0) {
            SoundManager.PlaySound(SoundManager.Sound.HealthyDied);
            Destroy(this.gameObject);
            GameManager.GetInstance().livingAllies--;
        } else {
            SoundManager.PlaySound(SoundManager.Sound.HealthyReceiveDamage);
            Health = Health - Damage;
            HealthText.text = "x" + Health;
            StartCoroutine(BlinkAlly(1, false));
        }
    }

    public void DestroyAlly(){
        StartCoroutine(BlinkAlly(3, true));
        GameManager.GetInstance().livingAllies--;
    }

    public IEnumerator BlinkAlly(int timesToBlink, bool destroy){
        yield return new WaitForSeconds(3f);
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