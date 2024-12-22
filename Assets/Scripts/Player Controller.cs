using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] float moveSpeed = 4f;
    //public float sprintMultiplier = 6f;
    [SerializeField] Transform focalPoint;
    Rigidbody rb;
    public float jumpForce = 7f;
    public bool isGrounded = true;
    public bool hasRepelPowerUp;
    //public bool hasSprintPowerUp;
    public float powerUpStrength = 15.0f;
    public Vector3 originalPos;
    int jumpCount = 0;
    int maxJumpCount = 2;
    [SerializeField] GameObject repelPowerIndicator;
    [SerializeField] GameObject sprintPowerIndicator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {        
        focalPoint.position = transform.position;

        Move();

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        repelPowerIndicator.SetActive(hasRepelPowerUp); 
        //sprintPowerIndicator.SetActive(hasSprintPowerUp);
        repelPowerIndicator.transform.position = transform.position + new Vector3(0f, 0f, 0f);
        //sprintPowerIndicator.transform.position = transform.position + new Vector3(0f, 2f, 0f);
    }

    void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2 (verticalInput, horizontalInput).normalized;

        rb.AddForce((focalPoint.forward * moveDirection.x + focalPoint.right * moveDirection.y) * moveSpeed);
    }

    void Jump()
    {
        //rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        jumpCount++;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("KillZone"))
        {
            gameObject.transform.position = originalPos;
        }
        jumpCount = 0;

        if (collision.gameObject.CompareTag("Enemy") && hasRepelPowerUp)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidBody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            Debug.Log("Player collided with " + collision.gameObject.name + " with powerup set to " + hasRepelPowerUp);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        jumpCount = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RepelPowerUp"))
        {
            Destroy(other.gameObject);
            StartCoroutine(repelPowerCooldown());
        }

        if (other.CompareTag("SprintPowerUp"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SprintPowerCooldown());
        }
    }
    private IEnumerator repelPowerCooldown()
    {
        hasRepelPowerUp = true;
        //wait for X seconds
        yield return new WaitForSeconds(10);
        hasRepelPowerUp = false;
    }
    private IEnumerator SprintPowerCooldown()
    {
        yield return new WaitForSeconds(15);
        //hasSprintPowerUp = false;
    }
}
