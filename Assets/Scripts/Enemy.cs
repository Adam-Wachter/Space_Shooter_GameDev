using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int _scoreValue = 25;
    [SerializeField]
    int _currentHealth = 2;
    [SerializeField]
    float _resetPosition = 7.6f;
    [SerializeField]
    float _downLimit = -6.5f;
    [SerializeField]
    float _speed = 5f;
    [SerializeField]
    GameObject _laser;
    [SerializeField]
    float _fireRate = .5f;
    float _nextFire;
    bool _playerDead = false;
    Player _player;
    private Animator _animator;
    bool _stopFiring = false;
    [SerializeField]
    private BoxCollider2D _boxCollider1;
    [SerializeField]
    private BoxCollider2D _boxCollider2;
    AudioSource _audioSource;
    float _volume;
    [SerializeField]
    AudioClip _laserSound;
    [SerializeField]
    AudioClip _damageSound;
    [SerializeField]
    AudioClip _explosionSound;



    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _player = FindAnyObjectByType<Player>();
        if (_player == null)
        {
            StartCoroutine(EnemyDeathSequence(true));
        }

        EnemyMovement();
        EnemyShoot();
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _downLimit)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-9f, 9f), _resetPosition, 0);
            transform.position = randomPosition;
        }
    }

    public void TakeDamage()
    {
        Damage();
    }

    void Damage()
    {
        _currentHealth--;
        _audioSource.clip = _damageSound;
        _audioSource.Play();

        if (_currentHealth < 1)
        {
            _player.AddScore(_scoreValue);
            EnemyDeath();
        }
    }

        void EnemyShoot()
    {
        if (Time.time > _nextFire && _stopFiring == false)
        {
            _nextFire = Time.time + _fireRate;
            _audioSource.clip = _laserSound;
            _volume = .5f;
            _audioSource.volume = _volume;
            _audioSource.Play();
            Instantiate(_laser, transform.position + (transform.up * -.54f), Quaternion.identity);
        }
    }

    public void EnemyDeath()
    {
        StartCoroutine(EnemyDeathSequence(false));
    }

    IEnumerator EnemyDeathSequence(bool dead)
    {
        _playerDead = dead;
        TriggerExplosion(true);
        _stopFiring = true;
        _boxCollider1.enabled = false;
        _boxCollider2.enabled = false;
        if (_playerDead == false)
        {
            _audioSource.clip = _explosionSound;
            _volume = 1f;
            _audioSource.volume = _volume;
            _audioSource.Play();
        }
        yield return new WaitForSeconds(.55f);
        Destroy(this.gameObject);
    }

    void TriggerExplosion(bool triggered)
    {
        _animator.SetBool("expl", triggered);
    }
}
