using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows to move forward if theres an obstacle in front, it has to become the parent of this afterwards, allows to move to the sides if there isnt an obstacle
public class PlayerTyle : MonoBehaviour
{
    bool moving;
    float counter;

    Vector3 origin;

    [SerializeField] float rayDistance;
    [SerializeField] float moveDistance;

    private void Awake()
    {
        origin = transform.position;
    }

    private void Update()
    {
        if (moving)
        {
            counter += Time.deltaTime;
            if (counter > 1)
            {
                moving= false;
            }
        }
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.right * rayDistance, Color.yellow);
        Debug.DrawRay(transform.position, transform.right * rayDistance, Color.yellow);
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.yellow);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, rayDistance))
            {
                Debug.Log("obstacle left");
                return; //there is an obstacle in the left
            }
            MoveToTyle("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, rayDistance))
            {
                Debug.Log("obstacle right");
                return; //there is an obstacle in the right
            }
            MoveToTyle("right");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance)) //if hit is a standing place, move there and parent self to it
            {
                MoveToTyle("up");
            }
            else
            {
                Debug.Log("no platform in front");
                Die();
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
    }

    void Die()
    {
        transform.position = origin;
    }
}
