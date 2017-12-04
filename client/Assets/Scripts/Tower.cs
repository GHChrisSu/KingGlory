using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<GameObject> listSoldier = new List<GameObject>();
    public List<GameObject> listHero = new List<GameObject>();
    [HideInInspector]
    public int towerType;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletStart;
    [SerializeField]
    private Transform parent;

    private GameObject previousBullet;
    private bool repeated;

    // Use this for initialization
    void Start()
    {
        if (gameObject.tag.Equals("Tower"))
        {
            towerType = 0;
        }
        else
        {
            towerType = 1;
        }
        InvokeRepeating("CreateBullet", 0.1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            listHero.Add(col.gameObject);
        }
        SmartSoldier soldier = col.GetComponent<SmartSoldier>();
        if (soldier && soldier.type != towerType)
        {
            listSoldier.Add(col.gameObject);
            print(listSoldier.Count);
        }

    }

    public void CreateBullet()
    {
        if (listHero.Count == 0 && listSoldier.Count == 0) return;
        if (previousBullet == null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletStart.position, Quaternion.identity) as GameObject;
            previousBullet = bullet;
            bullet.transform.parent = parent;
            BulletTarget(bullet);
            repeated = false;
        }
        else
        {
            repeated = true;
        }
    }

    public void BulletTarget(GameObject bullet)
    {
        if (listSoldier.Count > 0)
        {
            listSoldier.RemoveAll(t => t == null);
            if (listSoldier.Count > 0) bullet.GetComponent<Bullet>().SetTarget(listSoldier[0]);
        }
        else if (listHero.Count > 0)
        {
            listHero.RemoveAll(t => t == null);
            if (listHero.Count > 0) bullet.GetComponent<Bullet>().SetTarget(listHero[0]);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            listHero.Remove(col.gameObject);
        }
        SmartSoldier soldier = col.GetComponent<SmartSoldier>();
        if (soldier && soldier.type != towerType)
        {
            listSoldier.Remove(col.gameObject);
        }
    }

    public void ShootCompleteMessageFromBullet()
    {
        if (repeated)
        {
            previousBullet = null;
            CreateBullet();
        }
    }

}
