using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject deadBodyPrefab;
    GameObject deadBody;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spine") || other.CompareTag("Meteorite"))
        {
            DeadState();
        }
    }
    public void DeadState()
    {
        if (deadBody != null)
        {
            Destroy(deadBody);
        }
        deadBody = Instantiate(deadBodyPrefab, this.transform.position, this.transform.rotation);

        this.gameObject.transform.position = gameManager.startPosition.position;
    }
}
