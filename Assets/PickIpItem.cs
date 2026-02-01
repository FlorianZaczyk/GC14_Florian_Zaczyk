using System;
using UnityEngine;

public class PickIpItem : MonoBehaviour
{
    [SerializeField] private GameObject _pickupText;
    [SerializeField] private float displayDuration = 2f;
    

    private void Awake()
    {
        _pickupText = GetComponent<GameObject>();
    }

    public void ShowPickupText(string itemName)
    {
        StopAllCoroutines();
        _pickupText.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay());
    }

    private System.Collections.IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        _pickupText.SetActive(false);
    }
}
