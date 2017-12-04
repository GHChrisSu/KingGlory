using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fire1;
    [SerializeField]
    private ParticleSystem fire2;
    private Animator ani;
    private int type;
    private List<GameObject> enemyList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        if (this.tag == "Player")
        {
            type = 0;
        }
        else
        {
            type = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyList.Remove(other.gameObject);
        enemyList.RemoveAll(t => t == null);
    }

    public void Atk1()
    {
        ani.SetInteger("state", AnimState.ATTACK1);
        enemyList.RemoveAll(t => t == null);
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].name.Contains("Tower"))
            {
                SmartSoldier ss = enemyList[i].GetComponent<SmartSoldier>();
                if (ss && ss.type != this.type)
                {
                    ss.TakeDamage(0.5f);
                }
            }
        }

    }

    public void Atk2()
    {
        ani.SetInteger("state", AnimState.ATTACK2);
    }

    public void Dance()
    {
        ani.SetInteger("state", AnimState.DANCE);
    }

    public void EffectPlay1()
    {
        fire1.Play();
    }

    public void EffectPlay2()
    {
        fire2.Play();
    }

    public void ResetIdle()
    {
        ani.SetInteger("state", AnimState.IDLE);
    }
}
