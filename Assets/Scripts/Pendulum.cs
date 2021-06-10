using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Quaternion start, end;

    [SerializeField, Range(0.0f, 360f)]
    private float angle = 90.0f;

    [SerializeField, Range(0.0f, 5.0f)]
    private float speed = 2.0f;

    [SerializeField, Range(0.0f, 10.0f)]
    private float startTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        start = RotationPendulum(angle);
        end = RotationPendulum(-angle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        startTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1.0f) / 2.0f);
    }

    private Quaternion RotationPendulum(float angle) {
        var rotationPendulum = transform.rotation;
        var angleX = rotationPendulum.x + angle;

        if (angleX > 180)
            angleX -= 360;
        else if (angleX < -100)
            angleX += 360;

        rotationPendulum.eulerAngles = new Vector3(angleX, rotationPendulum.eulerAngles.y, rotationPendulum.eulerAngles.z);
        return rotationPendulum;
    }
}
