using OT.Foundation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartSoldier : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animation ani;
    private float distance;
    public Transform target;
    public Transform[] towers;
    private List<Transform> enemyList = new List<Transform>();
    public int type;
    float initializedSpeed;
    private Health HP;

    // Use this for initialization
    void Start()
    {
        HP = GetComponent<Health>();
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animation>();
        target = GetTarget();
        initializedSpeed = nav.speed;
        SoldierMove();
    }

    // Update is called once per frame
    void Update()
    {
        SoldierMove();
    }

    void SoldierMove()
    {
        if (target == null)
        {
            target = GetTarget();
            return;
        }
        ani.CrossFade("Run");
        nav.SetDestination(target.position);
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < 2)
        {
            nav.speed = 0;
            Vector3 tarPos = target.position;
            Vector3 lookPos = new Vector3(tarPos.x, transform.position.y, tarPos.z);
            transform.LookAt(lookPos);
            ani.CrossFade("Attack1");
        }
        else
        {
            nav.speed = initializedSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        HP.TakeDamage(damage);
    }

    Transform GetTarget()
    {
        enemyList.RemoveAll(t => t == null);
        if (enemyList.Count > 0)
        {
            return enemyList[0];
        }
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] != null)
            {
                return towers[i];
            }
        }
        return null;
    }

    public void SetRoad(int road)
    {
        nav = GetComponent<NavMeshAgent>();
        nav.areaMask = road;
    }

    public void Attack()
    {
        print("Attack");
        if (target == null)
        {
            target = GetTarget();
            return;
        }
        Health health = target.GetComponent<Health>();
        print(health.gameObject.name + " : " + Vector3.Distance(transform.position, target.position));
        if (health.gameObject.name.Contains("Tower") && Vector3.Distance(transform.position, target.position) > 2) return;
        float damage = Random.Range(0.1f, 0.6f);
        if (health.TakeDamage(damage))
        {
            //TODO remove current target, get next target
            if(enemyList.Contains(target)) enemyList.Remove(target);
            else DestoryTowerInList(target);

            target = GetTarget();
            if (target)
            {
                nav.SetDestination(target.transform.position);
                nav.speed = initializedSpeed;
                ani.CrossFade("Run");
            }
        }
    }

    private void DestoryTowerInList(Transform destoryTower)
    {
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] == destoryTower)
            {
                towers[i] = null;
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && this.type == 1)
        {
            AddToEnemyList(other);
        }
        else
        {
            SmartSoldier ss = other.GetComponent<SmartSoldier>();
            if (ss!=null && ss.type != this.type && !this.enemyList.Contains(other.transform))
            {
                AddToEnemyList(other);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2);
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemyList.Contains(other.transform))
        {
            Debug.Log("Remove enemy target!");
            enemyList.Remove(other.transform);
            target = GetTarget();
        }
    }

    private void AddToEnemyList(Collider other)
    {
        enemyList.Add(other.transform);
        Transform temp = enemyList[0];
        if (target == null || temp != target)
        {
            target = temp;
        }
    }
}
