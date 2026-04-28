using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.Video; 

public class CommandAudio : MonoBehaviour
{
    [Header("Audio")]

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    [Header("Video")]
    [SerializeField] private VideoPlayer _videoPlayer;

    void Start()
    {
        // on ajoute la fonction au clic du bouton
        GetComponent<Button>().onClick.AddListener(TogglePlayPause);

        // on assigne le clip audio au lecteur
        _audioSource.clip = _audioClip;

        // la vidéo tourne en boucle
        _videoPlayer.isLooping = true;

        // la vidéo ne démarre pas toute seule
        _videoPlayer.playOnAwake = false;
    }

    //fonction pour mettre en pause / play l'audio et la vidéo
    public void TogglePlayPause()
    {
        // si l’audio est terminé alors on reset tout
        if (IsFinished())
        {
            // on remet l'audio à zéro
            ResetAudio();
            // on relance l'audio et la vidéo
            PlayBoth();   
            return;
        }

        // si l'audio est en cours de lecture alors on met en pause l'audio et la vidéo
        if (_audioSource.isPlaying)
        {
            PauseBoth();
        }
        else
        {
            // sinon on joue l'audio et la vidéo
            PlayBoth();
        }
    }


    // fonction pour jouer l'audio et la vidéo
    public void PlayBoth()
    {
        // on lance l'audio
        _audioSource.Play();
        // on lance la vidéo
        _videoPlayer.Play(); 
    }


    // fonction pour mettre en pause l'audio et la vidéo
    public void PauseBoth()
    {
        // on met en pause l'audio
        _audioSource.Pause();
        // on met en pause la vidéo
        _videoPlayer.Pause(); 
    }


    // fonction pour reset uniquement l'audio 
    public void ResetAudio()
    {
        // on stop l'audio
        _audioSource.Stop();
        // on remet l'audio au début
        _audioSource.time = 0f; 
    }


    // fonction pour vérifier si l’audio est terminé
    public bool IsFinished()
    {
        // on vérifie qu’un clip existe et qu'il est proche de la fin
        return _audioClip != null && _audioSource.time >= _audioClip.length - 0.05f;
    }
}