using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private List<EnemyData> _enemyData;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private List<Window> _windows;

    private readonly List<EnemyBrain> _enemies = new();
    private Player _player;

    private void Start()
    {
        CreatePlayer();
        _windows.ForEach(w => w.Construct(_player));
        StartCoroutine(SpawnEnemies());
    }

    private void CreatePlayer()
    {
        _player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        _player.Construct(StaticData.PlayerRole.Weapon, StaticData.PlayerRole.StartHealth, Camera.main);
        _camera.Follow = _player.transform;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) 
        {
            yield return new WaitForSeconds(5);
            EnemyData data = _enemyData.Random();
            CreateEnemy(data);
        }
    }

    private void CreateEnemy(EnemyData data)
    {
        var enemy = Instantiate(data.Prefab, _spawnPoint.position, Quaternion.identity);
        _enemies.Add(enemy);
        enemy.Construct(_player, data);
    }
}