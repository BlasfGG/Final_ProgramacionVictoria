using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMA : MonoBehaviour
{

    [SerializeField] float velocidadBala;
    [SerializeField] GameObject balaPrefab;
    [SerializeField] Transform puntoTiro;

    [SerializeField] public int municionActual = 100;
    


    private void Start()
    {
       
    }

    void Update()
    {
        AccionarArma();
    }
    public void AccionarArma()
    {
        if (JalaGatillo())
        {
            Disparar();

        }
    }
    bool JalaGatillo()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    public void Disparar()
    {
        if (JalaGatillo() && municionActual > 0)
        {
            //AudioManager.AudioInstance.Play("Disparo");
            GameObject clone = Instantiate(balaPrefab, puntoTiro.position, puntoTiro.rotation);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(puntoTiro.forward * velocidadBala, ForceMode.Impulse);
            Destroy(clone, 3);
            municionActual--;
           
        }
      
    }

   

}
