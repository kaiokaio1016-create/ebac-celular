using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // O namespace continua com "Management"

public class LoadSceneHelper : MonoBehaviour
{
    public void Load(int i)
    {
        // O comando correto é SceneManager
        SceneManager.LoadScene(i);
    }

    public void Load(string s)
    {
        // O comando correto é SceneManager
        SceneManager.LoadScene(s);
    }
}
