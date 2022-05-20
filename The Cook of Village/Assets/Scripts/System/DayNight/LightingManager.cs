using UnityEngine;

public class LightingManager : MonoBehaviour, IObserver<GameDataManager>
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    private void UpdateLighting(float time)
    {
        float timePercent = time / 1440f;
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent); // 씬의 주변 조명의 색상 변경
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent); // 안개 색상 변경

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent); // 빛 색상 변경
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            // DirectionalLight 회전시켜 태양의 움직임 변화
        }
    }

    private void changeLight()
    {
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
    public void AddObserver(IGameDataOb o)
    {
        GameDataManager.Instance.AddObserver(this);
    }

    public void RemoveObserver(IGameDataOb o)
    {
        GameDataManager.Instance.RemoveObserver(this);
    }

    public void Change(GameDataManager obj)
    {
        if (obj is GameDataManager)
        {
            var GameData = obj;
            changeLight();
            UpdateLighting(GameData.TimeOfDay);
        }
    }
}
