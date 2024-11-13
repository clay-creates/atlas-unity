using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [Header("Ammo Configuration")]
    public int maxAmmo = 5;
    private int currentAmmo;

    private void Start()
    {
        ResetAmmo();
    }

    public void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    public void ResetAmmo()
    {
        currentAmmo = maxAmmo;
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }
}
