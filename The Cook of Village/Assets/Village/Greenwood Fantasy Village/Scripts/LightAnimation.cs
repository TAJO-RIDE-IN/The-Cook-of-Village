using UnityEngine;
using System.Collections;

public class LightAnimation : MonoBehaviour {
    private Transform _transform;
	private Light LightIntensity;
	private float StartIntensity;

    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private float _speed = 5f;
	[Range(0, 1)]
	[SerializeField] private float _randomIntensity = 0.2f;

    void Awake () {
		LightIntensity = GetComponent<Light> ();
        _transform = transform;
    }

    void Start () {
        StartCoroutine (FlameAnimation ());
    }

    IEnumerator FlameAnimation () {
        Vector3 startPosition = _transform.position;
		StartIntensity = LightIntensity.intensity;
        
        while (true) {
            Vector3 randomPosition = startPosition + Random.insideUnitSphere * _radius;
            Vector3 lastPosition = _transform.position;
			float lastIntensity = LightIntensity.intensity;
			float randomIntensity = StartIntensity * Random.Range (1 - _randomIntensity, 1 + _randomIntensity);

            float time = 0f;

            while (time < 1f) {
                _transform.position = Vector3.Lerp (lastPosition, randomPosition, time);
				LightIntensity.intensity = Mathf.Lerp (lastIntensity, randomIntensity, time);

                time += Time.deltaTime * _speed;
                yield return null;
            }

            yield return null;
        }
    }
}
