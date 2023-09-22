using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CARTEL_FINAL : MonoBehaviour
{
    private PAJARO scriptPajaro;
    private MOHO_ELIMINAR scriptPez;
    private InteraccionCarpincho scriptCarpincho;
    private AGUA2_ENTREGAR scriptabeja;

    public GameObject cartel;
    public GameObject cartelTapado;

    public AudioClip musicaFinal;
    public AudioSource audioSource;
    private bool musicaReproducida = false;

    private bool desactivarNiebla = false;

    public Material skyboxMaterial;
    public Camera miCamara;

    public Light myLight;          // Referencia a la luz que deseas modificar.
    public float aumentoIntensidad = 2.0f; // Aumento temporal de la intensidad.
    public float duracionAumento = 2.0f;  // Duración del aumento en segundos.
    private float intensidadOriginal; // Almacena la intensidad original de la luz.
    public Light luzGeneral;



    void Start()
    {
        scriptPajaro = FindObjectOfType<PAJARO>();
        scriptPez = FindObjectOfType<MOHO_ELIMINAR>();
        scriptCarpincho = FindObjectOfType<InteraccionCarpincho>();
        scriptabeja = FindObjectOfType<AGUA2_ENTREGAR>();
        cartel.SetActive(false);

        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.03f;

        luzGeneral.intensity = 0.2f;

        myLight.enabled = false;
        intensidadOriginal = myLight.intensity;

    }

    // Update is called once per frame
    void Update()
    {
        if (scriptPajaro.pajaroCompletado && scriptPez.pezCompletado && scriptCarpincho.carpinchoCompletado && scriptabeja.abejacompletado)
        {
            if (!musicaReproducida)
            {
                cartel.SetActive(true);
                cartelTapado.SetActive(false);
                audioSource.PlayOneShot(musicaFinal);
                musicaReproducida = true;
                desactivarNiebla = true;
                StartCoroutine(AumentarIntensidadPorTiempo());
            }
            
            
        }

        if (desactivarNiebla)
        {
            
            StartCoroutine(updateTheFog());

            if (miCamara != null && skyboxMaterial != null)
            {
                // Cambia el tipo de fondo a Skybox.
                miCamara.clearFlags = CameraClearFlags.Skybox;

                // Asigna el material de Skybox.
                RenderSettings.skybox = skyboxMaterial;
            }
            else
            {
                Debug.LogError("La cámara principal o el material de Skybox no están configurados.");
            }
        }

        
        
    }

    private IEnumerator AumentarIntensidadPorTiempo()
    {
        myLight.enabled = true;

        float tiempoTranscurrido = 0.0f;
        float intensidadInicial = 2.0f; // Cambia la intensidad inicial a 2f.

        while (tiempoTranscurrido < duracionAumento)
        {
            // Calcula la intensidad gradualmente en función del tiempo utilizando una interpolación suavizada.
            float t = tiempoTranscurrido / duracionAumento;
            float smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
            myLight.intensity = Mathf.Lerp(intensidadInicial, intensidadInicial + aumentoIntensidad, smoothT);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        // Asegúrate de que la intensidad sea exactamente igual a intensidadInicial.
        myLight.intensity = intensidadInicial;
        luzGeneral.intensity = 2.0f;

        yield break;

    }

    IEnumerator updateTheFog()
    {
        float targetFogDensity = 0.0f; // El valor al que deseas que la niebla desaparezca.
        float fogChangeRate = 0.001f;   // La velocidad a la que la niebla disminuirá.

        while (RenderSettings.fogDensity > targetFogDensity)
        {
            // Disminuye gradualmente la densidad de la niebla.
            RenderSettings.fogDensity -= fogChangeRate;

            // Espera 3 segundos antes de continuar con la siguiente iteración.
            yield return new WaitForSeconds(3);
        }
        // Puedes usar "yield break;" para detener el bucle si es necesario.
    }
}
