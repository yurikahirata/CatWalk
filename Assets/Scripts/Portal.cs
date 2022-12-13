using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Portal : MonoBehaviour
{
    private string level;
    public bool isFinalLevel;

    private AudioSource source;
    public AudioClip portalSound;

    void Start()
    {
        source = GetComponent<AudioSource>();

        // splicing level name to automatically pull next level
        level = SceneManager.GetActiveScene().name;

        // if final level, load main menu
        if (isFinalLevel) level = "Menu";
        else
        {
            float levelNumber = float.Parse(level.Substring(5)) + 1;
            level = "Level" + levelNumber;
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LevelFinished());
        }
    }

    private IEnumerator LevelFinished()
    {
        source.clip = portalSound;
        source.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
    }


}