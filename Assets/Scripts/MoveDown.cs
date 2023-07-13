using UnityEngine;


public class MoveDown : MonoBehaviour
{
    [Tooltip("The speed at which the object moves down.")]
    [SerializeField] 
    private float _speed = 3f;


    void Update()
    {
        // Move the object down using the private speed variable and Time.deltaTime
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }
    }
}