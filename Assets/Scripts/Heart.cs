using UnityEngine;

public class Heart : MonoBehaviour
{
    public Transform a;       
    public Vector3 offset;    

    void LateUpdate()
    {
        transform.position = new Vector3(a.position.x + offset.x, a.position.y + offset.y, transform.position.z);
    }
}

