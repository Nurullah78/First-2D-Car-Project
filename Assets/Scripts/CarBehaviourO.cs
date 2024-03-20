using UnityEngine;
using UnityEngine.SceneManagement;

public class CarBehaviourO : MonoBehaviour
{
    public Transform[] wayPoints, wayPoints2, temp;
    public int target;
    public float carSpeed, timeToChangeLane;
    private float _counter;
    
    void Start()
    {
        temp = wayPoints;
        _counter = timeToChangeLane;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");
        }
    }
    
    void Update()
    {
        transform.position =
            Vector3.MoveTowards(
                transform.position, temp[target].position, carSpeed * Time.deltaTime);

        if (transform.position == temp[target].position)
        {
            if (target == temp.Length - 1)
            {
                target = 0;
                transform.Rotate(0f, 0f, 45f);
            }
            else
            {
                target++;
                transform.Rotate(0f, 0f, 45f);
            }
        }
        
        if (_counter > 0)
        {
            if (temp[target] == wayPoints[target] && _counter < 1.1f)
            {
                temp[target] = wayPoints2[target];
                temp = wayPoints2;
                _counter -= Time.deltaTime;
            }
            else if (temp[target] == wayPoints2[target] && _counter < 2.1f)
            {
                temp[target] = wayPoints[target];
                temp = wayPoints;
                _counter -= Time.deltaTime;
            }
            else
            {
                _counter -= Time.deltaTime;
            }
        }

        if (_counter <= 0)
        {
            _counter = timeToChangeLane;
        }
    }
}
