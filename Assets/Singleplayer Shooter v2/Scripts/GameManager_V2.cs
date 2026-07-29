using UnityEngine;

public class GameManager_V2 : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            _gun.Use();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gun.Reload();
        }
    }
}
