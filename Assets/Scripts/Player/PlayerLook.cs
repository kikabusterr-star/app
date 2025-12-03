using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sens = 120f;
    public Transform cam;

    private float xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xRot -= my;
        xRot = Mathf.Clamp(xRot, -85f, 85f);

        cam.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mx);
    }
}
