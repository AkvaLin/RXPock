using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public int duration = 60; //seconds
    public IObservable<long> timer = Observable.Timer(TimeSpan.FromSeconds(0f), TimeSpan.FromSeconds(1f));
    private int coins;
    private AudioSource sounds;
    public CompositeDisposable storage = new CompositeDisposable();

    private void Start()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin").Length;
        sounds = GameObject.Find("Sounds").GetComponent<AudioSource>();
        
        timer.Subscribe(tick =>
        {
            if (tick == duration)
            {
                storage.Clear();
                SceneManager.LoadScene("DefeatScene");
            }
        }).AddTo(storage);

        Player player = FindObjectOfType<Player>();
        
        player.currentHealth.Subscribe(health =>
        {
            if (health == 0)
            {
                storage.Clear();
                SceneManager.LoadScene("DefeatScene");
            }
        }).AddTo(storage);

        player.command.Subscribe(coins =>
        {
            sounds.Play();
            
            if (this.coins == coins)
            {
                storage.Clear();
                SceneManager.LoadScene("WinScene");
            }
        }).AddTo(storage);
    }
}
