using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
   [SerializeField] InputAction thrust;

   [SerializeField] InputAction rotation;
   [SerializeField] float thrustStrength = 1000f;
   [SerializeField] float rotationStrength = 100f;
   [SerializeField] ParticleSystem leftBoosterParticles;
   [SerializeField] ParticleSystem rightBoosterParticles;
   [SerializeField] ParticleSystem mainBoosterParticles;



   Rigidbody rb;
   AudioSource audioSource;

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
      audioSource = GetComponent<AudioSource>();
   }
   private void OnEnable()
   {

      thrust.Enable();
      rotation.Enable();
   }

   private void FixedUpdate()
   {
      ProcessThrust();
      ProcessRotation();
   }

   private void ProcessThrust()
   {
      if (thrust.IsPressed())
      {
         if (!audioSource.isPlaying)
         {
            audioSource.Play();
         }

         rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
         if (!mainBoosterParticles.isPlaying)
         {
            mainBoosterParticles.Play();
         }

      }
      else
      {
         mainBoosterParticles.Stop();
         audioSource.Stop();
      }
   }

   private void ProcessRotation()
   {
      rb.freezeRotation = true; // freezing rotation so we can manually rotate
      float rotationInput = rotation.ReadValue<float>();
      UpdateBoosterParticles(rotationInput);
      transform.Rotate(Vector3.forward * -rotationInput * rotationStrength * Time.fixedDeltaTime);

      rb.freezeRotation = false; // unfreezing rotation so the physics system can take over


   }

   private void UpdateBoosterParticles(float rotationInput)
   {
      if (rotationInput < 0)
      {
         if (!rightBoosterParticles.isPlaying)
         {
            rightBoosterParticles.Play();
         }
      }
      else
      {
         rightBoosterParticles.Stop();
      }

      if (rotationInput > 0)
      {
         if (!leftBoosterParticles.isPlaying)
         {
            leftBoosterParticles.Play();
         }
      }
      else
      {
         leftBoosterParticles.Stop();
      }
   }


}