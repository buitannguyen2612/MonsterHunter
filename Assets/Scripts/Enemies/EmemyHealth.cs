using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyHealth : MonoBehaviour
{
    [SerializeField] private int strartingHealth = 3;
    public int currentHealth;
    private KnockBack knockback;
    private Flash flash;

    

     private void Awake() {
        flash = GetComponent<Flash>();
        knockback  = GetComponent<KnockBack>();
    }

    private void Start() {
        currentHealth =  strartingHealth;
    }

    public void TakeDamge(int damge){
        currentHealth = currentHealth - damge;
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine(){
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }
    private void DetectDeath(){
        if (currentHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
