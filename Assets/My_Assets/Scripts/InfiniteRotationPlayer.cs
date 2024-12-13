using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRotationPlayer : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.up, 0.05f, Space.World);
	}
}
