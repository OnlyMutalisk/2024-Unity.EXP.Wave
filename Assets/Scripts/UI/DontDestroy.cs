using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("Start");
    }
}
