using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool _isSpeedBoostActive = false;
    [SerializeField]
    bool _isTripleShotActive = false;
    [SerializeField]
    bool _isShieldActive = false;
    [SerializeField]
    float _powerupLifetime = 5.0f;
    [SerializeField]
    float _speed = 10f;
    bool _canMove = false;
    [SerializeField]
    float _boostSpeed = 15f;
    [SerializeField]
    float _resetSpeed = 10f;
    [SerializeField]
    GameObject _laser;
    [SerializeField]
    GameObject _tripleShotLaser;
    [SerializeField]
    float _fireRate = .5f;
    float _nextFire;
    int _maxHealth = 3;
    int _currentHealth;
    bool _stopFiring = true;
    SpawnManager _spawnManager;
    UIManager _uiManager;
    [SerializeField]
    GameObject _shield;
    int _score = 0;
    private Animator _animator;
    [SerializeField]
    GameObject _components;
    [SerializeField]
    Transform _componentsTransform;
    [SerializeField]
    BoxCollider2D _boxCollider1;
    [SerializeField]
    BoxCollider2D _boxCollider2;
    [SerializeField]
    AudioSource _audioSource1;
    [SerializeField] 
    AudioSource _audioSource2;
    float _volume = 1;
    [SerializeField]
    AudioClip _laserSound;
    [SerializeField]
    AudioClip _tripleShotSound;
    [SerializeField]
    AudioClip _damageSound;
    [SerializeField]
    AudioClip _powerupSound;
    [SerializeField]
    AudioClip _explosionSound;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,-1.62f,0);
        _currentHealth = _maxHealth;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerBoundries();
        PlayerShoot();
    }

    public void StartPlayerMovement()
    {
        _canMove = true;
        _stopFiring = false;
    }

    void PlayerMovement()
    {
        if (_canMove == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

            if (_isSpeedBoostActive == true)
            {
                _speed = _boostSpeed;
            }

            transform.Translate(direction * _speed * Time.deltaTime);
        }

    }

    void PlayerBoundries()
    {
        float _upLimit = 5.7f;
        float _downlLimit = -3.7f;
        float _leftlLimit = -11.3f;
        float _rightlLimit = 11.3f;

        if (transform.position.y >= _upLimit)
        {
            transform.position = new Vector3(transform.position.x, _upLimit, 0);
        }
        else if (transform.position.y <= _downlLimit)
        {
            transform.position = new Vector3(transform.position.x, _downlLimit, 0);
        }

        if (transform.position.x >= _rightlLimit)
        {
            transform.position = new Vector3(_leftlLimit, transform.position.y, 0);
        }
        else if (transform.position.x <= _leftlLimit)
        {
            transform.position = new Vector3(_rightlLimit, transform.position.y, 0);
        }
    }

    void PlayerShoot()
    {

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire && _stopFiring == false)
        {
            _nextFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotLaser, transform.position, Quaternion.identity);
                _audioSource1.clip = _tripleShotSound;
                _volume = .2f;
                _audioSource1.volume = _volume;
                _audioSource1.Play();
            }
            else
            {
                Instantiate(_laser, transform.position + (transform.up * 1.34f), Quaternion.identity);
                _audioSource1.clip = _laserSound;
                _volume = .2f;
                _audioSource1.volume = _volume;
                _audioSource1.Play();
            }

        }
    }

    IEnumerator TripleShot()
    {
        _isTripleShotActive = true;
        yield return new WaitForSeconds (_powerupLifetime);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoost()
    {
        _isSpeedBoostActive = true;
        yield return new WaitForSeconds(_powerupLifetime);
        _isSpeedBoostActive = false;
        _speed = _resetSpeed;
    }

    IEnumerator ShieldPowerup()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
        yield return new WaitForSeconds(_powerupLifetime);
        _shield.SetActive(false);
        _isShieldActive = false;
    }

    public void AddScore(int add)
    {
        _score += add;
        _uiManager.PlayerScoreUpdate(_score);
    }

    public void TakeDamage()
    {
        Damage();
    }

    void Damage()
    {
        if (_isShieldActive == false)
        {
            _currentHealth--;

            _audioSource2.clip = _damageSound;
            _volume = .5f;
            _audioSource2.volume = _volume;
            _audioSource2.Play();
            DamageAnimations();

            if (_currentHealth < 1)
            {
                StartCoroutine(PlayerDeathSequence());
            }
        }

        _uiManager.PlayerLifeUpdate(_currentHealth);
    }

    void DamageAnimations()
    {
        if (_currentHealth == 2)
        {
            _componentsTransform.GetChild(3).gameObject.SetActive(true);
        }
        if (_currentHealth == 1)
        {
            _componentsTransform.GetChild(4).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.EnemyDeath();
            TakeDamage();
        }

        if (other.tag == "TripleShot")
        {
            _audioSource2.clip = _powerupSound;
            _volume = 1f;
            _audioSource2.volume = _volume;
            _audioSource2.Play();
            StopCoroutine(TripleShot());
            StartCoroutine(TripleShot());
            Destroy(other.gameObject);
        }

        if (other.tag == "SpeedBoost")
        {
            _audioSource2.clip = _powerupSound;
            _volume = 1f;
            _audioSource2.volume = _volume;
            _audioSource2.Play();
            StopCoroutine(SpeedBoost());
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject);
        }

        if (other.tag == "ShieldPowerup")
        {
            _audioSource2.clip = _powerupSound;
            _volume = 1f;
            _audioSource2.volume = _volume;
            _audioSource2.Play();
            StopCoroutine(ShieldPowerup());
            StartCoroutine(ShieldPowerup());
            Destroy(other.gameObject);
        }
    }

    IEnumerator PlayerDeathSequence()
    {
        _boxCollider1.enabled = false;
        _boxCollider2.enabled = false;
        _canMove = false;
        _stopFiring = true;
        _spawnManager.StopEnemySpawn();
        _components.SetActive(false);
        TriggerExplosion(true);
        _audioSource1.clip = _explosionSound;
        _volume = 1f;
        _audioSource1.volume = _volume;
        _audioSource1.Play();
        yield return new WaitForSeconds(.917f);
        Destroy(this.gameObject);
    }

    void TriggerExplosion(bool triggered)
    {
        _animator.SetBool("explosion", triggered);
    }
}
