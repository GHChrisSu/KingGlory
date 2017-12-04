using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    private SpriteSlider hp;
    private uint value = 1;

	// Use this for initialization
	void Start () {
        hp = GetComponentInChildren<SpriteSlider>();
        Init();
	}

    private void Init()
    {
        hp.Value = value;
    }

    //return : true - dead, false - alive
    public bool TakeDamage(float damage)
    {
        hp.Value -= damage;
        if (hp.Value <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
