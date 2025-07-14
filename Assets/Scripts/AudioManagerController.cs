using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;


    public AudioClip background;




    private void Start()
    {
        musicSource.clip = background;
    }


}
