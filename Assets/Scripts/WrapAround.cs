using UnityEngine;
using System.Collections;

//Adapted from http://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055
public class WrapAround : MonoBehaviour
{
    private BoxCollider mapBoxCollider;

    // Use this for initialization
    void Start()
    {
        mapBoxCollider = GameObject.FindGameObjectWithTag("MapBoundary").GetComponent<MapBoundary>().GetComponent<BoxCollider>();
    }

    public void ScreenWrap()
    {
        transform.position = -transform.position;
    }
}
