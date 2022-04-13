using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] private GameManager gameManager;
    public float orbitSpeed = 1.0f;

    private void Update()
    {
        if (Preset == null)
            return;

            TimeOfDay += Time.deltaTime * orbitSpeed;
            //TimeOfDay %= 24;

        if(TimeOfDay > 24)
        {
            TimeOfDay = 0;
            gameManager.Day++;
        }
        gameManager.TimeOfDay = TimeOfDay;
        UpdateLighting();
        
    }


    private void UpdateLighting()
    {
        float timePercent = TimeOfDay / 24f;
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    private void OnValidate()
    {
        UpdateLighting();
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
