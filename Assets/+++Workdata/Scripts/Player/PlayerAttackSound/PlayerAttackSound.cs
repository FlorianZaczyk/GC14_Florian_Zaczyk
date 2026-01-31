using UnityEngine;

public class PlayerAttackSound : MonoBehaviour
{
    public Collider2D swordCollider;   
    public Collider2D swordCollider2;  
    public Collider2D swordCollider3;  

    
    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip attackSound3;

    
    private AudioSource audioSource;

    private void Start()
    {
        swordCollider.enabled = false;
        swordCollider2.enabled = false;
        swordCollider3.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    public void EnableSwordHitbox()
    {
        Debug.Log("Enable 1");
        swordCollider.enabled = true;
        PlayAttackSound1();
    }

    public void EnableSwordHitbox2()
    {
        Debug.Log("Enable 2");
        swordCollider2.enabled = true;
        PlayAttackSound2();
    }
    
    public void EnableSwordHitbox3()
    {
        Debug.Log("Enable 3");
        swordCollider3.enabled = true;
        PlayAttackSound3();
    }

    public void DisableSwordHitbox()
    {
        Debug.Log("Disable");
        swordCollider.enabled = false;
        swordCollider2.enabled = false;
    }

    void PlayAttackSound1()
    {
        audioSource.PlayOneShot(attackSound1);
    }

    void PlayAttackSound2()
    {
        audioSource.PlayOneShot(attackSound2);
    }
    
    void PlayAttackSound3()
    {
        audioSource.PlayOneShot(attackSound3);
    }
}