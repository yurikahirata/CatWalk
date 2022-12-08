using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Portal : MonoBehaviour
{
    private string level;
    public bool isFinalLevel;

    void Start()
    {
        // splicing level name to automatically pull next level
        level = SceneManager.GetActiveScene().name;

        // if final level, load main menu
        if (isFinalLevel) level = "MainMenu";
        else
        {
            float levelNumber = float.Parse(level.Substring(5)) + 1;
            level = "Level" + levelNumber;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(level);
        }
    }

}
