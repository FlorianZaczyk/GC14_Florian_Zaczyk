using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;



public class CameraManager : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public CinemachineCamera cm;

    public AudioSource _music;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        cm.Priority = 10;
        Debug.Log("Trigger");
        }
    }
 
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cm.Priority = 0;
        }
    }
}
