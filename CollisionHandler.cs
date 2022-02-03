using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;
    
    bool isTransitioning = false;
    bool collisionDisabled = false;

    AudioSource audioSource;

    void Start() 
    {
       audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //collisionDisabled = true;


        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) { return;  }
        switch(other.gameObject.tag)
        {
        case "Friendly":
            Debug.Log("This thing is friendly!");
            break;

        case "Finish":
            StartSuccessSubsequence();
            break;

        default:
            StartCrashSequence();
            break;
        }
    }

    void StartSuccessSubsequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    
    }

    void StartCrashSequence ()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }

    void LoadNextLevel ()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }  
}
