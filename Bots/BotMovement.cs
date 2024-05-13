using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public TypeTransport typeTransport;
    [SerializeField] private float defaultSpeed = 50f;
    public float currentSpeed = 0f;
    [SerializeField] private float speedCoef = 0.0004f;

    public bool orientation = true;
    public bool isFrontTrigger = false; // срабатывает если перед ботом препятствие
    public bool isCrash = false;

    public MeshRenderer bodyTextue;
    public GameObject BotBody;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, currentSpeed * speedCoef, 0));
    }

    public void SetParams(Transform pos, bool orientation, float speed)
    {
        if (pos != null)
            BotBody.transform.position = pos.position;
        else
            Debug.Log("Не найдена позиция бота при спавне");

        if (!orientation)
        {
            BotBody.transform.rotation = Quaternion.Euler(new Vector3(0, BotBody.transform.rotation.eulerAngles.y + 180, 0));
            SetSpeed(-speed);
            defaultSpeed = -speed;
        }
        else
        {
            SetSpeed(speed);
            defaultSpeed = speed;
        }

    }

    public void SetSpeed(float speed)
    {
        //rb.angularVelocity = new Vector3(0.0f, -0.0002f * speed, 0.0f);
        currentSpeed = speed;
    }
  
    public void DestroySelf()
    {
        Destroy(this.gameObject);
        //if (trafficSpawner != null) 
         //   trafficSpawner.BotsCount--;
    }
}
