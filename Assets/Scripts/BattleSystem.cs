using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    public void Start()
    {
        StartBattle();
    }
    private void StartBattle()
    {
        //enemyTransform.GetComponent<EnemySpawn>().Spawn();
    }
}
