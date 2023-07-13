using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    float _speed = 10f;
    [SerializeField]
    float _laserLifeTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        DestroyLaser();
    }

    void DestroyLaser()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject, _laserLifeTime);
        }

        Destroy(gameObject, _laserLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage();
            Destroy(this.gameObject);
        }
    }

}
