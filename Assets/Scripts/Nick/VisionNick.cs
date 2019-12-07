using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionNick : MonoBehaviour
{
    void FixedUpdate()
    {
        lookMouse();
    }

    void lookMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direccionvista = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = direccionvista;
    }
}
