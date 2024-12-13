using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ColliderBarriere : MonoBehaviour
{
    public List<Collider> colliders = new List<Collider>();
	public List<GameObject> fences = new List<GameObject>();

	public void ReactivateBarriers()
	{
		foreach (Collider collider in colliders)
		{
			collider.enabled = true;
		}
	}

	public void CloseFence()
	{
		foreach (GameObject fence in fences)
		{
			if (fence.transform.parent.name == "Barrières-Haut")
			{
				if (fence.name == "Barriere_gauche")
					fence.transform.rotation = Quaternion.Euler(-90, -90, -90);
				else
					fence.transform.rotation = Quaternion.Euler(-90, 90, -90);
			} 
			else if (fence.transform.parent.name == "Barrières-Bas")
			{
				if (fence.name == "Barriere_gauche")
					fence.transform.rotation = Quaternion.Euler(-90, 90, -90);
				else
					fence.transform.rotation = Quaternion.Euler(-90, -90, -90);
			} 
			else
			{
				if (fence.name == "Barriere_gauche")
					fence.transform.rotation = Quaternion.Euler(-90, 180, 90);
				else
					fence.transform.rotation = Quaternion.Euler(-90, 0, 90);
			}
		}
	}
}
