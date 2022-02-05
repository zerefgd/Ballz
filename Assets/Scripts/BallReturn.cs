using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    private BallLauncher _ballLauncher;

    private void Awake()
    {
        _ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _ballLauncher.ReturnBall();
        collision.collider.gameObject.SetActive(false);
    }
}
