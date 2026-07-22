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
         StartThrusting();

      }
      else
      {
         StopThrusting();
      }
   }

   private void StopThrusting()
   {
      mainBoosterParticles.Stop();
      audioSource.Stop();
   }

   private void StartThrusting()
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
   private void ProcessRotation()
   {
      float rotationInput = rotation.ReadValue<float>();
      UpdateBoosterParticles(rotationInput);
      ApplyRotation(rotationInput);
   }

   private void ApplyRotation(float rotationInput)
   {
      rb.freezeRotation = true;
      transform.Rotate(Vector3.forward * -rotationInput * rotationStrength * Time.fixedDeltaTime);
      rb.freezeRotation = false;
   }

   private void UpdateBoosterParticles(float rotationInput)
   {
      UpdateBooster(rightBoosterParticles, rotationInput < 0);
      UpdateBooster(leftBoosterParticles, rotationInput > 0);
   }

   private void UpdateBooster(ParticleSystem booster, bool shouldPlay)
   {
      if (shouldPlay)
      {
         if (!booster.isPlaying) booster.Play();
      }
      else
      {
         booster.Stop();
      }
   }


}