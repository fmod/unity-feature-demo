using UnityEngine;
using System.Collections;

public class LightRotator : MonoBehaviour
{
    public float speed = 0.05f;

    Quaternion initialRotation;
    Quaternion targetRotation;
    
	void Start ()
    {
        initialRotation = transform.rotation;
        targetRotation = GetNewRotation();
	}
	
	void Update ()
    {
        if (Quaternion.Angle(targetRotation, transform.rotation) <= 5.0f)
        {
            // Get new rotation to lerp towards
            targetRotation = GetNewRotation();
        }

        // Lerp rotation towards target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);
	}

    Quaternion GetNewRotation()
    {
        Quaternion temp = initialRotation * Quaternion.Euler(Random.Range(-45, 45), Random.Range(-45, 45), 0);
        return temp;
    }
}
