using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float moveSpeed = 4;
    Rigidbody rb;
    Transform playerTransform;
    public bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerTransform = GameObject.FindFirstObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            PlayerFollow();
        }
    }

    void PlayerFollow()
    {
        Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
        rb.AddForce(moveDirection * moveSpeed);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
