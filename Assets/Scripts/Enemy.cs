using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int WaypointIndex;
    public Transform Target;

    public float MoveSpeed;

    public int Health;
    public int Damage;

    public EnemySO Data;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Events.OnGameOver += GameOver;
        GameManager.Instance.Events.OnEnemyHit += GetHit;

        WaypointIndex = 0;

        Target = GameManager.Instance.CurrentLevel.GetWaypoint(WaypointIndex);
        transform.LookAt(Target);
    }
    private void OnDestroy()
    {
        GameManager.Instance.Events.OnGameOver -= GameOver;
        GameManager.Instance.Events.OnEnemyHit -= GetHit;
    }

    public void InitMe()
    {
        Health = Data.Health;
        MoveSpeed = Data.Speed;
        Damage = Data.Damage;
    }

    private void GetHit(Enemy enemy, int damage)
    {
        if (enemy == this)
        {
            Health -= damage;

            if (Health <= 0)
                KillMe();
        }
    }

    private void KillMe()
    {
        GameManager.Instance.Events.EnemyKilled(this);
        Destroy(gameObject);

    }

    private void GameOver(bool whoWon)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            CheckForLevelFinish();
            return;
        }

        Vector3 newPos = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * MoveSpeed);
        newPos.y = transform.position.y;
        transform.position = newPos;

        //Debug.Log("distance left: " + Vector3.Distance(transform.position, Target.position));

        //float minDistance = ((transform.position.y - Target.position.y) + 0.1f);
        if (Mathf.Abs(Vector3.Distance(transform.position, Target.position)) < 0.51f)
        {
            //transform.rotation = Target.rotation;
            WaypointIndex += 1;
            Target = GameManager.Instance.CurrentLevel.GetWaypoint(WaypointIndex);
            transform.LookAt(Target);
        }

    }

    private void CheckForLevelFinish()
    {
        if (WaypointIndex == GameManager.Instance.CurrentLevel.Waypoints.Length)
        {
            GameManager.Instance.Events.EnemyFinishedPath(Damage);
            Destroy(gameObject);
        }
    }
}
