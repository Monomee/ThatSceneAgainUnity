using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    //[SerializeField] GameObject deadBodyPrefab;
    //public GameObject deadBody; --> idea for later
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spine") || other.CompareTag("Meteorite"))
        {
            Dead();
        }
    }
    public void Dead()
    {
        //if (deadBody != null)
        //{
        //    Destroy(deadBody);
        //}
        //deadBody = Instantiate(deadBodyPrefab, this.transform.position, this.transform.rotation);
        GameManager.Instance.LoadCurrentLevel();

        this.gameObject.transform.position = gameManager.startPosition.position;
    }
}
