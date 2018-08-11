using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        RotatePlayerTowardsMouse();
    }

    private void RotatePlayerTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerpos = cam.WorldToScreenPoint(transform.position);
        float dx = mousePos.x - playerpos.x;
        float dy = mousePos.y - playerpos.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0, (-angle), 0);
    }
}