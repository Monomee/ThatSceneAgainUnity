using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] GameObject newCam;
    void Awake()
    {
        mainCam = Camera.main;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other!=null && other.CompareTag("Player"))
        {
            Debug.Log("in");
            UIManager.Instance.content.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(false);
            newCam.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player")) 
        { 
            Debug.Log("out");
            UIManager.Instance.content.gameObject.SetActive(true);
            if (mainCam != null) mainCam.gameObject.SetActive(true);
            if (newCam != null) newCam.SetActive(false);
        }
    }
}
