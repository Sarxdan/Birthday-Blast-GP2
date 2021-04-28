using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected int health;
    [SerializeField] int startingHealth = 1;
    // Start is called before the first frame update
    protected virtual void Awake() {
        health = startingHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;   
        if(health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    protected virtual IEnumerator Death()
    {
        yield return new WaitForEndOfFrame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
