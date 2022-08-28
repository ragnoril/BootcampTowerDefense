using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform GunTurret;
    public SphereCollider RangeCollider;
    public List<Enemy> TargetList;
    public Enemy CurrentTarget;

    public GameObject BulletPrefab;

    public TowerSO TowerData;

    private float _shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        TargetList = new List<Enemy>();

        GameManager.Instance.Events.OnGameOver += GameOver;
        GameManager.Instance.Events.OnBaseSelected += BaseSelected;
        GameManager.Instance.Events.OnTowerSelected += TowerSelected;
        GameManager.Instance.Events.OnEnemyKilled += RemoveEnemyFromTargets;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Events.OnGameOver -= GameOver;
        GameManager.Instance.Events.OnBaseSelected -= BaseSelected;
        GameManager.Instance.Events.OnTowerSelected -= TowerSelected;
        GameManager.Instance.Events.OnEnemyKilled -= RemoveEnemyFromTargets;
    }

    private void RemoveEnemyFromTargets(Enemy enemy)
    {
        if (TargetList.Contains(enemy))
            TargetList.Remove(enemy);

    }

    public void InitTower()
    {
        RangeCollider.radius = TowerData.Range;
        _shootingTimer = 0f;
    }

    private void GameOver(bool whoWon)
    {
        TargetList.Clear();
        CurrentTarget = null;
    }

    private void TowerSelected(Tower tower)
    {
        if (tower != this)
        {
            // close ui
        }

    }

    private void BaseSelected(TowerBase towerBase)
    {
        // close ui
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTarget == null)
        {
            FindNewTarget();
        }
        else
        {
            _shootingTimer -= Time.deltaTime;
            GunTurret.LookAt(CurrentTarget.transform);
            Shoot();
        }

    }

    private void Shoot()
    {
        if (_shootingTimer < 0f)
        {
            _shootingTimer = TowerData.AttackRate;

            GameObject go = Instantiate(BulletPrefab, GunTurret.position, GunTurret.rotation);
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.Damage = TowerData.Damage;
            bullet.Go();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            TargetList.Add(enemy);

            if (CurrentTarget == null)
                FindNewTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            if (TargetList.Contains(enemy))
            {
                TargetList.Remove(enemy);
                if (CurrentTarget != enemy)
                    FindNewTarget();
            }
        }
    }

    private void FindNewTarget()
    {
        CurrentTarget = null;
        if (TargetList.Count == 0) return;

        float maxDistance = 0f;
        Enemy target = TargetList[0];
        foreach(Enemy enemy in TargetList)
        {
            float dist = Mathf.Abs(Vector3.Distance(transform.position, enemy.transform.position));

            if (dist > maxDistance)
            {
                maxDistance = dist;
                target = enemy;
            }
        }

        CurrentTarget = target;

    }
}
