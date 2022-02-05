using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    [SerializeField] private Ball _ballPrefab;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private LaunchPreview _launchPreview;
    private BlockSpawner _blockSpawner;

    private List<Ball> _balls =  new List<Ball>();

    private int _ballsReady = 0;
    private bool _canMove;
    private bool _canDrag;

    private void Awake()
    {
        _launchPreview = GetComponent<LaunchPreview>();
        _blockSpawner = FindObjectOfType<BlockSpawner>();
        _canMove = true;
        _canDrag = false;
        CreateBall();
    }

    private void CreateBall()
    {
        var ball = Instantiate(_ballPrefab);
        ball.gameObject.SetActive(false);
        _balls.Add(ball);
        _ballsReady++;
    }

    public void ReturnBall()
    {
        _ballsReady++;
        if(_ballsReady == _balls.Count)
        {
            _blockSpawner.SpawnBlocks();
            CreateBall();
            _canMove = true;
            _canDrag = true;
        }
    }

    private void Update()
    {
        if (!_canMove) return;
        if(_canDrag && Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if(Input.GetMouseButton(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.back*-10);
            ContinueDrag(worldPosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void EndDrag()
    {
        StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        _canMove = false;
        Vector3 direction = _endPosition - _startPosition;
        direction.Normalize();

        foreach (var ball in _balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(direction);
            _ballsReady--;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        _endPosition = worldPosition;
        _launchPreview.SetEndPoint(_endPosition);
    }

    private void StartDrag()
    {
        _canDrag = false;
        _startPosition = transform.position;
        _launchPreview.SetStartPoint(_startPosition);
    }
}
