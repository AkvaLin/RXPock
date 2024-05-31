using System;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI coins;
    
    private void Start()
    {
        GameController gc = FindObjectOfType<GameController>();
        
        timer.text = $"Time left: {gc.duration}";
        coins.text = $"Collected coins: 0";
        
        gc.timer
            .Sample(TimeSpan.FromSeconds(1f))
            .Subscribe(n =>
            {
                timer.text = $"Time left: {gc.duration - n - 1}";
            }).AddTo(gc.storage);

        Player player = FindObjectOfType<Player>(); 
        
        player.currentHealth.Subscribe(health =>
        {
            this.health.text = string.Concat(System.Linq.Enumerable.Repeat("<3", health));
        }).AddTo(gc.storage);;

        player.command.Subscribe(coins =>
        {
            this.coins.text = $"Collected coins: {coins}";
        }).AddTo(gc.storage);;
    }
}
