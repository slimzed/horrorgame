using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;


    public AudioClip background;
    public AudioClip knifeHit;
    public AudioClip monsterHit;
    public AudioClip grenadeThrow;




    private void Start()
    {
        musicSource.clip = background;
    }


}
