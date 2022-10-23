using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public CameraFlowing linkCF;
    public ChunkSystem linkCS;

    public LineRenderer lineRenderer;
    public Transform Point1;
    public Transform Point2;

    private Vector2 tempCenter;
    private Vector2 vectCalc;
    public bool clockDirect = false;
    private bool active1Point = true;

    [SerializeField]
    float radius = 2f, angularSpeed = 2f;
    float targetRadius;
    float miniScale = 0.8f;

    public float angle = 0f;

    private void Awake()
    {
        targetRadius = radius;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (targetRadius > 1f)
            {
                targetRadius -= 1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            targetRadius = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (targetRadius < 4f)
            {
                targetRadius += 1f;
            }
        }
        if (targetRadius - radius > 0.1f)
        {
            radius += 0.03f;
        }
        else if (radius - targetRadius > 0.1f)
        {
            radius -= 0.03f;
        }

        SpinAround();

        MiddleLine();
    }

    private void SpinAround() 
    {
        tempCenter = active1Point ? Point1.transform.position : Point2.transform.position;

        vectCalc.x = tempCenter.x + Mathf.Cos(angle) * radius;
        vectCalc.y = tempCenter.y + Mathf.Sin(angle) * radius;

        if (active1Point)
        {
            Point2.transform.position = vectCalc;
        }
        else
        {
            Point1.transform.position = vectCalc;
        }

        lineRenderer.SetPosition(active1Point ? 0 : 2, vectCalc);

        angle = angle + Time.deltaTime * angularSpeed * (clockDirect ? 1 : -1);
        if (angle >= 6.28f)
        {
            angle -= 6.28f;
        }
        if (angle <= -6.28f) 
        {
            angle += 6.28f;
        }
    }

    public void ChangeCenter(bool isClockDirect)
    {
        linkCS.Move(Point1.transform.position);
        clockDirect = isClockDirect;
        active1Point = !active1Point;
        angle -= 3.14f;
        linkCF.changeTarget(active1Point ? Point1 : Point2);
        Point1.localScale = active1Point ? Vector3.one * miniScale : Vector3.one;
        Point2.localScale = active1Point ? Vector3.one : Vector3.one * miniScale;
    }

    private void MiddleLine() 
    {
        lineRenderer.SetPosition(1, (lineRenderer.GetPosition(0) + lineRenderer.GetPosition(2))/2);
    }
}