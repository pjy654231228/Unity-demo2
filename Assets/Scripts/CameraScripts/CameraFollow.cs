using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 lastTargetPOsition;
    private float offsetZ;
    public bool smoothDamp=false;
    private Vector3 currentVelocity;
    private float camerSpeed=0.3f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(MyTag.PLAYER_TAG).transform;

        lastTargetPOsition = target.position;
        offsetZ = (transform.position - target.position).z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aheadTargePos = target.position + offsetZ*Vector3.forward;

        if (aheadTargePos.x>=transform.position.x) {
            if (smoothDamp) {
                Vector3 currentPosition = Vector3.SmoothDamp(transform.position,aheadTargePos,ref currentVelocity,camerSpeed);
                transform.position = new Vector3(currentPosition.x,transform.position.y,currentPosition.z);
            }
            else {
                transform.position = new Vector3(aheadTargePos.x, transform.position.y,aheadTargePos.z) ;
                aheadTargePos = transform.position;
            }

        
        }
    }
}
