using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset;
    [SerializeField] float followSpeed = 0.03f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, followSpeed);
    }
}
