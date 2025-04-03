using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public MovementController controller;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            controller.isOnGround = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            controller.isOnGround = false;
        }
        
    }
}
