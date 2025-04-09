
using System.Collections;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] float velocidadBala;
    [SerializeField] GameObject balaPrefab;
    [SerializeField] Transform puntoTiro;
    [SerializeField] float tiempoEntreDisparos = 0.5f; // Tiempo entre disparos
    [SerializeField] private AudioSource disparo;


    private bool puedeDisparar = true; // Controla si se puede disparar

    private void Start()
    {
    }

    void Update()
    {
        AccionarArma();
    }

    public void AccionarArma()
    {
        if (JalaGatillo() && puedeDisparar)
        {
            StartCoroutine(DispararConRetraso());
            disparo.Play();
        }
    }

    bool JalaGatillo()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    private IEnumerator DispararConRetraso()
    {
        puedeDisparar = false; // Bloquea el disparo
        Disparar(); // Llama al método para disparar
        yield return new WaitForSeconds(tiempoEntreDisparos); // Espera 0.5 segundos
        puedeDisparar = true; // Permite disparar nuevamente
    }

    public void Disparar()
    {
        // Instanciar la bala y alinear su rotación con el punto de tiro
        GameObject clone = Instantiate(balaPrefab, puntoTiro.position, puntoTiro.rotation);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(puntoTiro.forward * velocidadBala, ForceMode.Impulse);
        Destroy(clone, 3); // Destruir la bala después de 3 segundos
    }


}
