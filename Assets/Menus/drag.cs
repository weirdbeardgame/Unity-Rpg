using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{

    bool isHeld;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            isHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isHeld = false;
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("isHeld: " + isHeld);

        if (isHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        }
    }
}
