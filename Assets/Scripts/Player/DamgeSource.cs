using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeSource : MonoBehaviour
{
    [SerializeField] private int damgeAmount = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<EmemyHealth>())
        {
         EmemyHealth enemyHealth = other.gameObject.GetComponent<EmemyHealth>();
         enemyHealth.TakeDamge(damgeAmount);
        }
    }
}
