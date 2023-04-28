using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows to move forward if theres an obstacle in front, it has to become the parent of this afterwards, allows to move to the sides if there isnt an obstacle
public class PlayerTyle : MonoBehaviour
{
    Vector3 origin;

    [SerializeField] float rayDistance;
    [SerializeField] float moveDistance;
    [SerializeField] float groundDistance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip moveClip;
    [SerializeField] AudioClip dieClip;
    

    private void Awake()
    {
        origin = transform.position;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, rayDistance))
            {
                return; //there is an obstacle in the left
            }
            MoveToTyle("left");
            audioSource.PlayOneShot(moveClip);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, rayDistance))
            {
                return; //there is an obstacle in the right
            }
            MoveToTyle("right");
            audioSource.PlayOneShot(moveClip);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.Instance.ScorePoint();
            MoveToTyle("up");
            audioSource.PlayOneShot(moveClip);
            TylesSpawner.instance.SpawnTyle();
        }

        //this to detect if die
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Death"))
            {
                Die();
                audioSource.PlayOneShot(dieClip);
            }
        }

        
    }

    void MoveToTyle(string direction)
    {
        Vector3 newOffset = Vector3.zero;
        if (direction == "left")
        {
            newOffset.x = -moveDistance;
            transform.position += newOffset;
        }
        else if (direction == "right")
        {
            newOffset.x = moveDistance;
            transform.position += newOffset;
        }
        else if(direction== "up")
        {
            newOffset.z = moveDistance;
            transform.position += newOffset;
        }

        //this to detect if grounded
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up * groundDistance, Color.black);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, groundDistance))
        {
            transform.parent = hit.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    void Die()
    {
        transform.position = origin;
        transform.parent = null;
        GameManager.Instance.ResetPoints();
    }
}
