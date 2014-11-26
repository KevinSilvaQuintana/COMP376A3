using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private List<string> tagsToIgnore; 

    // Use this for initialization
    void Start() {
        foreach (string s in tagsToIgnore)
        {
            GameObject[] objectsToignore = GameObject.FindGameObjectsWithTag(s);
            foreach (GameObject obj in objectsToignore)
            {
                Collider otherCollider = obj.GetComponentInChildren<Collider>();
                Collider thisCollider = gameObject.GetComponentInChildren<Collider>();

                Physics.IgnoreCollision(thisCollider, otherCollider);
            }
        }     
	}
}
