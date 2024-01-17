using UnityEngine;

public class Main : MonoBehaviour
{
    public void Awake()
    {
        GameObject gameObjectMenu = GameObject.Find("CanvasDontDestroy").transform.GetChild(0).gameObject;
        gameObjectMenu.SetActive(false);
        GameManager.stages.Clear();
    }
}
