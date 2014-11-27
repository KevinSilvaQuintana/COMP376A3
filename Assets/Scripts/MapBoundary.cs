using UnityEngine;
using System.Collections;

public class MapBoundary : MonoBehaviour {

    void Start()
    {
        //Debug.Log("Wall Tag: " + gameObject.name);
    }

    //void OnTriggerExit(Collider collider)
    //{
    //    Debug.Log("Wall Tag: " + gameObject.name);
    //    WrapAround wrapAround = collider.gameObject.GetComponent<WrapAround>();
    //    if (wrapAround != null)
    //    {
    //        wrapAround.ScreenWrap();
    //    }
    //    else if (collider.gameObject.tag == "Missile")
    //    {
    //        //collider.gameObject.GetComponent<WallBounce>().Bounce();
    //    }
    //    else
    //    {
    //        Debug.Log("Nothing to Wrap!");
    //    }
    //}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnCollisionEnter: " + gameObject.name);
        Debug.Log("upVector: " + gameObject.transform.up);
        WrapAround wrapAround = collider.gameObject.GetComponent<WrapAround>();
        if (wrapAround != null)
        {
            wrapAround.ScreenWrap();
        }
        else if (collider.gameObject.tag == "Missile")
        {
            collider.gameObject.GetComponent<WallBounce>().Bounce(gameObject.transform.up);
        }
        else
        {
            Debug.Log("Nothing to Wrap!");
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        Debug.Log("OnCollisionEnter: " + gameObject.name);
        Debug.Log("upVector: " + gameObject.transform.up);
        WrapAround wrapAround = collider.gameObject.GetComponent<WrapAround>();
        if (wrapAround != null)
        {
            wrapAround.ScreenWrap();
        }
        else if (collider.gameObject.tag == "Missile")
        {
            collider.gameObject.GetComponent<WallBounce>().Bounce(gameObject.transform.up);
        }
        else
        {
            Debug.Log("Nothing to Wrap!");
        }
    }
}
