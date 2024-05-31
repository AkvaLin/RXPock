using System;
using UniRx;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 3;
    public readonly ReactiveProperty<int> currentHealth = new ReactiveProperty<int>();
    [SerializeField] private float speed = 1f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private int coins = 0;
    public readonly ReactiveCommand<int> command = new ReactiveCommand<int>();

    void Start()
    {
        currentHealth.Value = health;
        rb = GetComponent<Rigidbody2D>();
        command.Execute(0);
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth.Value -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coins += 1;
            command.Execute(coins);
            Destroy(other.gameObject);
        }
    }
}