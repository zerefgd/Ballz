using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    private int _numOfBlock = 8;

    [SerializeField] private Block _blockPrefab;
    [SerializeField] private float _distanceBetweenBlocks = 4f;
    private int _rowsSpawned = 0;
    private List<Block> _blocksSpawned = new List<Block>();

    private void OnEnable()
    {
        SpawnBlocks();
    }

    public void SpawnBlocks()
    {
        foreach (var block in _blocksSpawned)
        {
            if(block != null)
            {
                block.transform.position += Vector3.down * _distanceBetweenBlocks;
            }
        }

        for (int i = 0; i < _numOfBlock; i++)
        {
            if(UnityEngine.Random.Range(0,100) > 50)
            {
                var block = Instantiate(_blockPrefab, GetPosition(i), Quaternion.identity);
                int hits = UnityEngine.Random.Range(1, 4) + _rowsSpawned;
                block.SetHit(hits);
                _blocksSpawned.Add(block);
            }
        }

        _rowsSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * _distanceBetweenBlocks;
        return position;
    }
}
