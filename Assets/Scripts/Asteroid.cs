using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Collider2D _collider2D;
    [SerializeField]
    float _speed = 5f;
    private Animator _animator;
    Player _player;
    int _scoreValue = 10;
    AudioSource _audioSource;
    [SerializeField]
    AudioClip _explosionSound;


    // Start is called before the first frame update
    void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMove();
    }

    void AsteroidMove()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _collider2D.enabled = false;
            TriggerExplosion(true);
            _audioSource.clip = _explosionSound;
            _audioSource.Play();
            _player.AddScore(_scoreValue);
            Destroy(gameObject, 1f);
        }
    }

    void TriggerExplosion(bool triggered)
    {
        _animator.SetBool("expl", triggered);
    }
}
