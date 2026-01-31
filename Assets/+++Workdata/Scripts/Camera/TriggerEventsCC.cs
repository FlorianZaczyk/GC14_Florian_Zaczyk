using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventsCC : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private CinemachineCamera cm;
    public AudioSource _audio;
    public AudioSource _audio2;

    public void Start()
    {
        cm = gameObject.GetComponent<CinemachineCamera>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cm.Priority.Value = 5;
            _audio.Play();
            _audio2.Play();
        }
        
        
        
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cm.Priority.Value = 0;
            _audio.Stop();
            _audio2.Play();
        }
    
    }
}