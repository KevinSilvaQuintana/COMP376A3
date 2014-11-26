using UnityEngine;
using System.Collections;

//Adapted from http://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055
public class WrapAround : MonoBehaviour
{
    public void ScreenWrap()
    {
        transform.position = -transform.position;
    }
}
