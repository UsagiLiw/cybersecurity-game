using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioManager AM;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AM = FindObjectOfType<AudioManager>();
    }

    /*
     * Handle audio for AudioManager which is intialize in the Menu scene.
     * For the sake of calling the method in Unity Editor.
     * Can still be call from other scripts but not recommended, call directly from the manager.
     */
    public void Play(string name)
    {
        AM.Play(name);
    }
}
