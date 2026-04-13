using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilX = 2f;
    public float recoilY = 2f;
    public float recoilZ = 1f;

    public float snappiness = 6f;
    public float returnSpeed = 2f;

    Vector3 currentRotation;
    Vector3 targetRotation;

    void Update()
    {
        // Return to neutral
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);

        // Smooth follow
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        if (targetRotation != Vector3.zero || currentRotation != Vector3.zero)

        transform.localRotation = Quaternion.Euler(currentRotation);

        if(Input.GetKeyDown(KeyCode.R))
            ApplyRecoil();
    }

    public void ApplyRecoil()
    {
        targetRotation += new Vector3(
            -recoilX,
            Random.Range(-recoilY, recoilY),
            Random.Range(-recoilZ, recoilZ)
        );
    }
}