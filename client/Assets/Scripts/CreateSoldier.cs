using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSoldier : MonoBehaviour
{
    [SerializeField]
    private GameObject soldierPrefab;
    [SerializeField]
    private Transform startTran;
    [SerializeField]
    private Transform soldierParent;

    #region NavAgent's targets(soldiers)
    [SerializeField]
    private Transform[] middleTowers;
    [SerializeField]
    private Transform[] leftTowers;
    [SerializeField]
    private Transform[] rightTowers;
    [SerializeField]
    private Transform[] enemyMiddleTowers;
    [SerializeField]
    private Transform[] enemyLeftTowers;
    [SerializeField]
    private Transform[] enemyRightTowers;
    #endregion

    #region soldier spawn place
    [SerializeField]
    private Transform[] start1;
    [SerializeField]
    private Transform[] start2;
    #endregion


    bool isCreatSoldier = true;
    public int soldierCount = 2;

    public float time;
    public float delayTime;
    public float spawnTime;

    private void Start()
    {
        StartCoroutine(Creat(time, delayTime, spawnTime));
    }

    void CreatSmartSoldier(SoldierType soldierType, Transform startTran, Transform[] towers, int road)
    {
        GameObject go = Instantiate(soldierPrefab, startTran.position, Quaternion.identity) as GameObject;
        go.transform.parent = soldierParent;

        SmartSoldier soldier = go.GetComponent<SmartSoldier>();
        soldier.towers = towers;
        soldier.SetRoad(road);

        soldier.type = (int)soldierType;
    }

    private IEnumerator Creat(float time, float delayTime, float spawnTime)
    {
        yield return new WaitForSeconds(time);
        while (isCreatSoldier)
        {
            for (int i = 0; i < soldierCount; i++)
            {
                CreatSmartSoldier(SoldierType.soldier1, start1[0], enemyMiddleTowers, 1 << 3);
                //CreatSmartSoldier(SoldierType.soldier1, start1[1], enemyLeftTowers, 1 << 4);
                //CreatSmartSoldier(SoldierType.soldier1, start1[2], enemyRightTowers, 1 << 5);

                CreatSmartSoldier(SoldierType.soldier2, start2[0], middleTowers, 1 << 3);
                //CreatSmartSoldier(SoldierType.soldier2, start2[1], leftTowers, 1 << 4);
                //CreatSmartSoldier(SoldierType.soldier2, start2[2], rightTowers, 1 << 5);

                yield return new WaitForSeconds(delayTime);
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
