using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRagdollWhenLow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -3) {
            Destroy(gameObject);
		}
	}
}

//Changement par Emery Descours pour tester le push