using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField] GameObject on;
    [SerializeField] GameObject off;
    [SerializeField] GameObject thingWillBeActive = null;

    bool status;

    // Start is called before the first frame update
    void Awake()
    {
        status = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //status = !status;
        on.SetActive(status);
        off.SetActive(!status);
        
        //Debug.Log(status);
        if (thingWillBeActive != null) thingWillBeActive.SetActive(status);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            on.SetActive(!status);
            off.SetActive(status);
        }
    }
}
