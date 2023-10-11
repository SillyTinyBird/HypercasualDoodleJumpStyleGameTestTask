using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelaySeconds = 5f;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Instantiate(_coinPrefab, new Vector2(Random.Range(-_camera.orthographicSize * _camera.aspect, _camera.orthographicSize * _camera.aspect), transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(_spawnDelaySeconds);
        }
    }
}
