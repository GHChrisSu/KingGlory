using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSlider : MonoBehaviour {
    [SerializeField]
    private Transform front;

    private float m_value;

    public float Value
    {
        get
        {
            return m_value;
        }

        set
        {
            m_value = value;
            front.localScale = new Vector3(m_value, 1, 1);
            front.localPosition = new Vector3((1-m_value)*(-2.448f), 0);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
