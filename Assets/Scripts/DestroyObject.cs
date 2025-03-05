using UnityEngine;

public class DestroyObject : MonoBehaviour
{


    /** This is a simple destroy script. It will destroy object that it is attached to after a time interval.
    It can also be called from sub events in inspector if destroy on start is false. */
    public bool _destroyOnStart = true;
    [Tooltip("The time to wait for the object to be destroyed.")]
    public float waitTime = 3f; // wait time in sec

    // Start is called before the first frame update
    void Start()
    {
        if(_destroyOnStart == true){
            DestroyThis(waitTime); // destroys object after wait time has passed.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyThis(float waitTime)
    {
        Destroy(gameObject, waitTime);
    }
}
