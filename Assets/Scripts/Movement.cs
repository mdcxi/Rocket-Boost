using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 60f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem rocketMainEngineParticle;
    [SerializeField] ParticleSystem LefThrustParticle;
    [SerializeField] ParticleSystem RightThrustParticle;
    Rigidbody rg;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else
        {
            StopParticales();
        }
    }

    void StartThrusting()
    {
        rg.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!rocketMainEngineParticle.isPlaying)
        {
            rocketMainEngineParticle.Play();
        }
    }

      void StopThrusting()
    {
        audioSource.Stop();
        rocketMainEngineParticle.Stop();
    }

    private void StopParticales()
    {
        LefThrustParticle.Stop();
        RightThrustParticle.Stop();
    }

    private void StartRightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!RightThrustParticle.isPlaying)
        {
            RightThrustParticle.Play();
        }
    }

    private void StartLeftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!LefThrustParticle.isPlaying)
        {
            LefThrustParticle.Play();
        }
    }

    void ApplyRotation(float rotatePerFrame)
    {
        rg.freezeRotation = true; //freezing rotation so we manually rotate
        transform.Rotate(Vector3.forward * rotatePerFrame * Time.deltaTime);
        rg.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
