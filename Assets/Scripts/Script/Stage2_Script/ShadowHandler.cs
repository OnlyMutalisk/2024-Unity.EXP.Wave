using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHandler : MonoBehaviour
{
    public ParticleSystemRenderer ps;
    private void Start()
    {
        ps = GetComponent<ParticleSystemRenderer>();
    }
    void Update()
    {

        if(ps!=null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("작동확인");
                ps.flip = new Vector3(1, ps.flip.y, ps.flip.z);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("작동확인");
                ps.flip = new Vector3(-1, ps.flip.y, ps.flip.z);
            }
        }
    }
}
