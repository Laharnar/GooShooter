using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float movementSpeed = 2;
    public float cameraFollowSpeed = 2;
    private Vector3 cameraOffset;
    public Animator playerAnimator;

    public Bullet bulletPrefab;
    public Transform gunExitPoint;

    private void Start()
    {
        cameraOffset = cam.transform.position;
        cameraOffset.y = 0;
    }
    private void Update()
    {
        HandlePlayerMovement();
        RotatePlayerTowardsMouse();
        CameraFollowPlayer();
        UpdateShooting();
    }

    private void UpdateShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetTrigger("Shoot");
            StartCoroutine(ShootWithDelay(0.3f));
        }
    }

    private IEnumerator ShootWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Bullet bullet = Instantiate<Bullet>(bulletPrefab, gunExitPoint.position, Quaternion.identity, null);
        bullet.Shoot(transform.forward);
    }

    private void CameraFollowPlayer()
    {
        Vector3 movementDirecton = transform.position - cam.transform.position + cameraOffset;
        movementDirecton.y = 0;
        cam.transform.position += movementDirecton * Time.deltaTime * cameraFollowSpeed;
    }

    private void HandlePlayerMovement()
    {
        bool moved = false;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * movementSpeed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * movementSpeed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * movementSpeed;
            moved = true;
        }
        if (moved)
        {
            playerAnimator.SetInteger("Run", 1);
        }
        else
        {
            playerAnimator.SetInteger("Run", 0);
        }
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