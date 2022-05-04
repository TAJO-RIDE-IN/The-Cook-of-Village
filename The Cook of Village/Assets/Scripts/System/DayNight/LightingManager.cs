using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    public float orbitSpeed = 1.0f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    private void Update()
    {
        if (Preset == null)
            return;

            TimeOfDay += Time.deltaTime * orbitSpeed;
        if(TimeOfDay > 24)
        {
            TimeOfDay = 0;
            GameManager.Instance.Day++;
        }
        GameManager.Instance.TimeOfDay = TimeOfDay;
        UpdateLighting();
        
    }

    private void UpdateLighting()
    {
        float timePercent = TimeOfDay / 24f;
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent); // 씬의 주변 조명의 색상 변경
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent); // 안개 색상 변경

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent); // 빛 색상 변경
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            // DirectionalLight 회전시켜 태양의 움직임 변화
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
