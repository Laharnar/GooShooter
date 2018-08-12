﻿using System;
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
    public GameObject playButton;
    public UiController uiController;
    public Rigidbody rig;

    public int hp = 100;

    private void Start()
    {
        cameraOffset = cam.transform.position;
        cameraOffset.y = 0;
    }
    private void Update()
    {
        if (playButton.gameObject.activeSelf)
        {
            return;
        }

        HandlePlayerMovement();
        RotatePlayerTowardsMouse();
        CameraFollowPlayer();
        UpdateShooting();
    }

    private void UpdateShooting()
    {
        if (Input.GetMouseButton(0))
        {
            playerAnimator.SetTrigger("Shoot");
            StartCoroutine(ShootWithDelay(0.3f));
        }
    }

    internal void Damage(int slimeDmg)
    {
        hp -= slimeDmg;
        if (hp <= 0)
            Death();
    }

    private void Death()
    {
        Destroy(gameObject);
        uiController.ShowReplayScreen();
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
            rig.MovePosition(transform.position + Vector3.forward * Time.deltaTime * movementSpeed);
            //transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
             rig.MovePosition(transform.position + Vector3.back * Time.deltaTime * movementSpeed);
            //transform.position += Vector3.back * Time.deltaTime * movementSpeed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
             rig.MovePosition(transform.position + Vector3.left * Time.deltaTime * movementSpeed);
            //transform.position += Vector3.left * Time.deltaTime * movementSpeed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
             rig.MovePosition(transform.position + Vector3.right * Time.deltaTime * movementSpeed);
            //transform.position += Vector3.right * Time.deltaTime * movementSpeed;
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
        rig.velocity = Vector3.zero;
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