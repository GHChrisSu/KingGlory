using OT.Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
    private GameObject target;
    public float speed = 20f;
    private Tower tower;

    // Use this for initialization
    void Start () {
        tower = GetComponentInParent<Tower>();
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "soldier")
        {
            Shoot(col, tower.listSoldier, 0.1f);
        }
        else if (col.gameObject.tag == "Player")
        {
            Shoot(col, tower.listHero, 0.01f);
        }
    }

    private void Shoot(Collider col, List<GameObject> list, float damage)
    {
        Health health = col.GetComponent<Health>();
        bool isDead = health.TakeDamage(damage);
        if (isDead) list.Remove(col.gameObject);
        tower.SendMessage("ShootCompleteMessageFromBullet");
        Debuger.Log("Bullet", "Shoot", "Destroy Bullet");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (target)
        {
            Vector3 dir = target.transform.position - transform.position;
            GetComponent<Rigidbody>().velocity = dir.normalized * speed;
        }
        else
        {
            Debuger.Log("Bullet", "Update", "Destroy Bullet");
            Destroy(gameObject);
        }
	}

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
