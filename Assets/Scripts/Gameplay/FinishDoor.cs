using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    [SerializeField] private Animation _openDoorAnimation;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Collector _collector;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClips;

    private static int _multiplier = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (_multiplier < _uiManager.StatusInfo.Length && _collector.Score > _uiManager.StatusInfo[_multiplier].value)
        {
            _openDoorAnimation.Play();
            _audioSource.PlayOneShot(_audioClips);
            _multiplier++;
        }
        else
        {
            PlayerPrefs.SetInt("Money", _collector.Score * _multiplier);
            PlayerPrefs.SetInt("LevelCompleted", PlayerPrefs.GetInt("LevelCompleted", 0) + 1);
            _uiManager.OpenWinWindow(_collector.Score * _multiplier);
            _multiplier = 1;           
        }        
    }
}
