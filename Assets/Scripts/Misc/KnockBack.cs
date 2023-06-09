using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack{get; private set;}
    private Rigidbody2D rb;
    [SerializeField] private float KnockBackTime =  .2f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        gettingKnockedBack =  true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine(){
        yield return new WaitForSeconds(KnockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
