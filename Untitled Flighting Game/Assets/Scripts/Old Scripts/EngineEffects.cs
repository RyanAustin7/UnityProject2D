using UnityEngine;

public class EngineEffects : MonoBehaviour
{
    public GameObject rocketEngine;
    public KeyCode upKey = KeyCode.W;

    private ParticleSystem ps;
    private bool isEnginePlaying = false;

    private void Start()
    {
        ps = rocketEngine.GetComponent<ParticleSystem>();
        ps.Stop();
    }

    private void Update()
    {
        HandleEngineEffects();
    }

    public void HandleEngineEffects()
    {
        if (Input.GetKeyDown(upKey))
        {
            ps.Play();
            if (!isEnginePlaying)
            {
                // AkSoundEngine.PostEvent("Play_Engine", gameObject);
                isEnginePlaying = true;
            }
        }
        if (Input.GetKeyUp(upKey))
        {
            ps.Stop();
            // AkSoundEngine.PostEvent("Stop_Engine", gameObject);
            isEnginePlaying = false;
        }

        // AkSoundEngine.SetRTPCValue("Speed", rb.velocity.magnitude * 15.6f);
    }
}
