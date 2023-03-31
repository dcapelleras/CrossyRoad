using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    float moveHorizontal;
    float moveVertical;
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    bool grounded;

    Vector3 initialPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        if (grounded)
        {
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce((movement + Vector3.up) * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        transform.parent = collision.transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
        grounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Die();
    }

    void Die()
    {
        transform.position = initialPosition;
        Debug.Log("You died");
    }
}
