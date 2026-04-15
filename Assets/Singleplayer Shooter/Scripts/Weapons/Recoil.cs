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

    public float _currentGunRecoilRate;


    private void Start()
    {
        WeaponManager.OnGunChange += HandleGunChange;
        Gun.OnGunShoot += ApplyRecoil;
    }

    private void OnDestroy()
    {
        WeaponManager.OnGunChange -= HandleGunChange;
        Gun.OnGunShoot -= ApplyRecoil;
    }

    private void HandleGunChange(WeaponData data)
    {
        _currentGunRecoilRate = data.recoilRate;
    }

    void Update()
    {
        // Return to neutral
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);

        // Smooth follow
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        if (targetRotation != Vector3.zero || currentRotation != Vector3.zero)

        //transform.localRotation = Quaternion.Euler(currentRotation);

        if(Input.GetKeyDown(KeyCode.R))
            ApplyRecoil();
    }

    public void ApplyRecoil()
    {
        targetRotation += new Vector3(
            -recoilX,
            Random.Range(-_currentGunRecoilRate, _currentGunRecoilRate),
            Random.Range(-recoilZ, recoilZ)
        );
    }
    public Quaternion GetRecoilRotation()
    {
        return Quaternion.Euler(currentRotation);
    }
}