using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{ 
    [SerializeField] GameObject go;
    [SerializeField] float gravityScale;
    [SerializeField] int mode; //0 for fall and 1 for fly
    float time;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0)
        {
            if (other.CompareTag("Player"))
            {
                if (go != null)
                {
                    go.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                    StartCoroutine(DestroyObject(go));
                }
            }
        }
        else if (mode == 1)
        {
            StartCoroutine(FlyObject());
        }
        
    }
    IEnumerator DestroyObject(GameObject go)
    {
        yield return new WaitForSeconds(3);
        Destroy(go);
    }

    IEnumerator FlyObject()
    {
        time = 0f;
        while (time < 1.5f)
        {
            if (go == null) yield break; 

            go.transform.position += Vector3.up;
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }

        if (go != null) Destroy(go);
    }
}
