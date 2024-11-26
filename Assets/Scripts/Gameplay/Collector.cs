using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collectMoney;
    [SerializeField] private ParticleSystem _collectAlcohol;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collectMoneySound;
    [SerializeField] private AudioClip _collectAlcoholSound;
    
    private int _score = 40;

    public const int AmountPointForMoney = 3;
    public const int AmountPointForAlcohol = -10;
    public const int MaxScore = 120;

    public Action<bool> OnCollectItem;


    public int Score => _score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Money" && other.tag != "Alcohol")
            return;

        if (other.tag == "Money")
        {
            _collectMoney.Play();
            _score += AmountPointForMoney;
            OnCollectItem?.Invoke(true);
            _audioSource.PlayOneShot(_collectMoneySound);
        }

        if (other.tag == "Alcohol")
        {
            _collectAlcohol.Play();
            _score += AmountPointForAlcohol;
            OnCollectItem?.Invoke(false);
            _audioSource.PlayOneShot(_collectAlcoholSound);
        }

        Destroy(other.gameObject);
    }
}
