using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour {
    public GameObject bullet;
    public List<GameObject> enemies;
    public bool isAttacking;
    public float attackCooldown;
    private float attackTime;

    private void Update() {
        if (enemies.Count > 0 && isAttacking == false){  
            isAttacking = true;
        } else if (enemies.Count  == 0 && isAttacking == true) {
            isAttacking = false;
        }
    }
}
