using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;

    private BoxCollider _collider;

    public int EnemyCount => _enemies.Count;

    public event Action EnemyDying;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].OneEnemyIsDead += OnOneEnemyIsDead;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].OneEnemyIsDead -= OnOneEnemyIsDead;
        }
    }

    public void DeactivateCollider()
    {
        _collider.enabled = false;
    }

    private void OnOneEnemyIsDead(Enemy enemy)
    {
        _enemies.Remove(enemy);
        EnemyDying?.Invoke();
    }
}
