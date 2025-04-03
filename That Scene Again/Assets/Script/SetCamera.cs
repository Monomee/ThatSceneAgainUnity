using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] GameObject newCam;

    // Start is called before the first frame update
    void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other!=null && other.CompareTag("Player"))
        {
            Debug.Log("in");
            mainCam.gameObject.SetActive(false);
            newCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player")) 
        { 
            Debug.Log("out");
            mainCam.gameObject.SetActive(true);
            newCam.SetActive(false);
        }
    }
}
