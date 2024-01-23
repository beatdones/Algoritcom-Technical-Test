using UnityEngine;

public class Parabola : MonoBehaviour
{
    public Transform startPoint;
    private Transform endPoint;

    // Time to move from hand to basketboard position, in seconds.
    [SerializeField] private float ballFlyingTime;

    private float currentFlyingTime;


    [SerializeField] private float helperVelocity;

    // The time at which the animation started.
    private float startTime;

    // Distance between points
    private float distance;

    private bool isKinematic;
    private bool distanceCalculated;
    private bool shoot;
    private bool setStartTime;

    private Rigidbody rb;

    void Start()
    {
        // Note the time at the start of the animation.
        startTime = Time.time;

        //startPoint = GameObject.Find("Custom Right Hand Model").transform;
        endPoint = GameObject.Find("EndPoint").transform;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (distanceCalculated) CalculateDistance(startPoint, endPoint);
        if (shoot) MoveBall();
    }

    private void CalculateDistance(Transform startPoint, Transform endPoint)
    {
        distanceCalculated = false;
        distance = Vector3.Distance(startPoint.position, endPoint.position);
        //distance = (startPoint.position - endPoint.position).sqrMagnitude; // mas rápido de calcular y menos pesado

        currentFlyingTime = ballFlyingTime * distance;
    }

    private void MoveBall()
    {
        if (isKinematic)
        {
            if (!setStartTime) startTime = Time.time;
            setStartTime = true;

            // The center of the arc
            Vector3 center = (startPoint.position + endPoint.position) * 0.5F;

            // move the center a bit downwards to make the arc vertical
            center -= new Vector3(0, .1f, 0);

            // Interpolate over the arc relative to center
            Vector3 riseRelCenter = startPoint.position - center;
            Vector3 setRelCenter = endPoint.position - center;

            // The fraction of the animation that has happened so far is
            // equal to the elapsed time divided by the desired time for
            // the total flying.
            float fracComplete = (Time.time - startTime) / currentFlyingTime;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;

            var currentDistance = (this.gameObject.transform.position - endPoint.position).sqrMagnitude;

            if (currentDistance <= 0.1f)
            {
                KinematicMovement(false);
                rb.velocity = new Vector3(RandomInt(), -2, RandomInt());
                Shooting(false);
            }
        }
        
    }

    public void CalculateFlyingTime() => distanceCalculated = true;
    public void KinematicMovement(bool kitematic) => isKinematic = kitematic;
    public void Shooting(bool shoot) => this.shoot = shoot;

    private int RandomInt()
    {
        int result = Random.Range(-1, 1);
        return result;
    }

    public void FakeBasket()
    {
        LeanTween.move(this.gameObject, endPoint.position, helperVelocity);
    }
}
