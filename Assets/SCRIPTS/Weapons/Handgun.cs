using UnityEngine;

public class Handgun : Weapon
{
    public override void Shoot()
    {
        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            if (isReloading) return; // Si el arma esta recargando no dispara
            nextFireTime = Time.time / fireRate; //tiempo entre disparos
            currentAmmo--;
            Bullet();
            Debug.Log($"Disparando: {currentAmmo}/{ammo}");
            Ammotext();
        }
    }
}