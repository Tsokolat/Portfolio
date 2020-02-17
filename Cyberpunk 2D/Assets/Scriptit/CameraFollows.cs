using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{

    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public float smoothing = 5;
    public float yPosRestriction = -1;

    float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadpos;

    //Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - target.position;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    void Update()
    {
        if (target == null)
            return;
        //only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
        if (updateLookAheadTarget)
        {
            lookAheadpos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            lookAheadpos = Vector3.MoveTowards(lookAheadpos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + lookAheadpos + Vector3.forward * offsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

        newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y,yPosRestriction,Mathf.Infinity), newPos.z);

        transform.position = newPos;
        lastTargetPosition = target.position;
    }


   /* void FixedUpdate()
   {
        Vector3 targetCamPos = target.position + offset;
       transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
   }*/
}
