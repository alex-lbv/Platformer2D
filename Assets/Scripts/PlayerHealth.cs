using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float health = 100f;

    public void ReduceHealth (float damage) {
        health -= damage;
        animator.SetTrigger("takeDamage");
        if (health <= 0f) Die();
    }

    private void Die () {
        animator.SetTrigger("toDie");
        // gameObject.SetActive(false);
    }
}
