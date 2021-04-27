using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected int health;
    [SerializeField] int startingHealth = 1;
    [SerializeField] AudioClip damageAudio;
    [SerializeField] protected AudioClip deathAudio;
    protected AudioSource audioSource;
    // Start is called before the first frame update
    protected virtual void Awake() {
        health = startingHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if(damageAudio != null)
        {
            audioSource.PlayOneShot(damageAudio);
        }       
        if(health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    protected virtual IEnumerator Death()
    {
        audioSource.Stop();
        if(deathAudio != null)
        {
            audioSource.PlayOneShot(deathAudio);
        }
        yield return new WaitForSeconds(deathAudio.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
