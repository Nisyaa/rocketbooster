using System;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    AudioSource audioSource;
    bool isControllable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) { return; }
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                Debug.Log("You have finished the level");
                break;
            default:
                StartCrushSequence();
                Debug.Log("You have crashed");
                break;
        }


    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrushSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }
    void StartSuccessSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        isControllable = false;
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }
}
