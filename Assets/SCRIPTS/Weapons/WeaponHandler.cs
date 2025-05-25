using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Transform weaponHolder; // Donde se emparentan las armas
    [SerializeField] private float detectionRange = 5f; // Rango de detección de armas
    [SerializeField] private LayerMask weaponLayer; // Capa de las armas
    [SerializeField] private TextMeshProUGUI ammoText; // Texto para mostrar la munición (opcional)
    [SerializeField] private Transform pointray;


    private List<Weapon> weaponList = new List<Weapon>(); // Lista de armas recogidas
    private int currentWeaponIndex = 0;
    private Weapon actualWeapon;
    private Action Shoot;

    RaycastHit hit;

    private void Start()
    {
        if (weapons != null && weapons.Length < 1)
        {
            Debug.LogWarning("No hay armas asignadas al WeaponHandler.");
        }
    }

    private void Update()
    {
        HandleWeaponSwitch();
        HandleWeaponPickup();

        Shoot?.Invoke();
        actualWeapon?.Reload(); //recarga del arma actual
    }

    private void HandleWeaponSwitch()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            int previousIndex = currentWeaponIndex;
            if (scroll > 0)
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            else
                currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;

            if (previousIndex != currentWeaponIndex)
                EquipWeapon(currentWeaponIndex);
        }
    }

    private void EquipWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == index);
        }
        actualWeapon = weapons[index];


        switch (actualWeapon.fireType)
        {
            case Weapon.FireType.Automatic:
                Shoot = AutomaticShoot;
                break;
            case Weapon.FireType.SemiAutomatic:
                Shoot = SemiAutomaticShoot;
                break;
        }

        ammoText.text = $"{actualWeapon.currentAmmo}/{actualWeapon.ammo}";
    }

    private void AutomaticShoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            actualWeapon.Shoot();
        }
    }

    private void SemiAutomaticShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            actualWeapon.Shoot();
        }
    }


    private void HandleWeaponPickup()
    {
        if (DetectionWeapon() && Input.GetKeyDown(KeyCode.E))
        {
            Weapon pickedWeapon = hit.collider.GetComponent<Weapon>();

            if (pickedWeapon != null && !weaponList.Contains(pickedWeapon))
            {
                // Agrfegar el arma recogida a la lista
                weaponList.Add(pickedWeapon);
                weapons = weaponList.ToArray();

                // Emparentar el arma recogida al holder
                pickedWeapon.transform.SetParent(weaponHolder);
                pickedWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                pickedWeapon.GetComponent<Collider>().enabled = false;

                // Establecer el arma recogida como la actual
                currentWeaponIndex = weapons.Length - 1;
                EquipWeapon(currentWeaponIndex);
            }
        }
    }

    private bool DetectionWeapon()
    {
        return Physics.Raycast(pointray.position, transform.forward, out hit, detectionRange, weaponLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pointray.position, transform.forward * detectionRange);
    }
}