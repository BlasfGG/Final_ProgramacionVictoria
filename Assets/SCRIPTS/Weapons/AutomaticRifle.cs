using UnityEngine;

    public class AutomaticRifle : Weapon
    {

        private void Awake()
        {
            fireType = FireType.Automatic;
        }

        public override void Shoot()
        {
            if (Time.time >= nextFireTime && currentAmmo > 0)
            {
                if (isReloading) return; // Si el arma esta recargando no dispara

                nextFireTime = Time.time + 1f / fireRate;
                currentAmmo--;
                Bullet(); 
                Debug.Log($"Disparando: {currentAmmo}/{ammo}");
                Ammotext();
            }
        }
    }
