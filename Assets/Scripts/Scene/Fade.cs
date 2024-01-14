using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject fade;

    private void Awake()
    {
        fade.SetActive(true);
    }

    public void FadeOut()
    {
        fade.SetActive(false);
    }
}
