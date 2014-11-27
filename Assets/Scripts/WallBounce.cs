using UnityEngine;
using System.Collections;

public class WallBounce : MonoBehaviour {

    public void Bounce(Vector3 wallNormal)
    {
        Debug.Log("Bounce!");
        //Quaternion upTrajectory = Quaternion.AngleAxis(180, -wallNormal);
        //transform.rotation = transform.rotation * upTrajectory;

        //transform.rigidbody.velocity = transform.rigidbody.velocity * -wallNormal;
        transform.rigidbody.velocity = transform.rigidbody.velocity;

        Vector3 newVelocity = new Vector3(1,1,1);
        if(wallNormal.x >= 0.5f || wallNormal.x <= -0.5f)
        {
            newVelocity = new Vector3(-rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
        }
        else if (wallNormal.y >= 0.5f || wallNormal.y <= -0.5f)
        {
            newVelocity = new Vector3(rigidbody.velocity.x, -rigidbody.velocity.y, rigidbody.velocity.z);
        }
        else if (wallNormal.z >= 0.5f || wallNormal.z <= -0.5f)
        {
            newVelocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, -rigidbody.velocity.z);
        }

        transform.rigidbody.velocity = newVelocity;

    }
}
