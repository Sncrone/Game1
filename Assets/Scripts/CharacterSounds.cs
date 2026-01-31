using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterSounds : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioClip walkSound;
    
    [Header("Walk Sound Settings")]
    [SerializeField] private float walkSoundInterval = 0.3f; // Time between footstep sounds
    [SerializeField] private float volume = 0.5f;
    
    private AudioSource audioSource;
    private float walkSoundTimer = 0f;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
    }
    
    void Update()
    {
        walkSoundTimer -= Time.deltaTime;
    }
    
    public void PlayWalkSound(bool isMoving, bool isGrounded)
    {
        // Only play if moving, grounded, and enough time has passed
        if (isMoving && isGrounded && walkSoundTimer <= 0f && walkSound != null)
        {
            audioSource.PlayOneShot(walkSound);
            walkSoundTimer = walkSoundInterval;
        }
    }
}