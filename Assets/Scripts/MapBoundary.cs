using UnityEngine;
using System.Collections;

public class MapBoundary : MonoBehaviour {

    void OnTriggerExit(Collider collider)
    {
        WrapAround wrapAround = collider.gameObject.GetComponent<WrapAround>();
        if (wrapAround != null)
        {
            Debug.Log("SUCCESS!");
            wrapAround.ScreenWrap();
        }
        else
        {
            Debug.Log("FAILED!");
        }
    }
}
