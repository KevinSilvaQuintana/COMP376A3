using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    [SerializeField]
    private float force;

    public void FireWithOffset(float offsetDistance)
    {
        if (offsetDistance > 0)
        {
            transform.Translate(transform.right * offsetDistance);
        }
        gameObject.rigidbody.AddForce(transform.right * force);
    }

    public void RotateFlightDirection(float deg)
    {
        transform.Rotate(Vector3.forward, deg);
    }
}
