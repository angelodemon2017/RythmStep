using UnityEngine;

public class CameraFlowing : MonoBehaviour
{
    public AnimationCurve StrenghtFollowing;
    public Transform target;
    private Vector2 tempVect;

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > 0.1f)
        {
            tempVect = Vector2.Lerp(transform.position, target.position, 0.01f);
            transform.position = new Vector3(tempVect.x, tempVect.y, -10f);
        }
    }

    public void changeTarget(Transform newTrg) 
    {
        target = newTrg;
    }
}
