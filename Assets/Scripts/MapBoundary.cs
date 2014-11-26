using UnityEngine;
using System.Collections;

public class MapBoundary : MonoBehaviour {

    public float size;

    void Start()
    {
        transform.localScale = new Vector3(size, size, size);
    }

    void OnTriggerExit(Collider collider)
    {
        WrapAround wrapAround = collider.gameObject.GetComponent<WrapAround>();
        if (wrapAround != null)
        {
            wrapAround.ScreenWrap();
        }
        else
        {
            Debug.Log("Nothing to Wrap!");
        }
    }
}
