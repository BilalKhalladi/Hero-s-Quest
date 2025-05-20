using UnityEngine;

public class BlockSnakeWave : MonoBehaviour
{
    [Header("Bloques y Parámetros de Onda")]
    public Transform[] blocks;

    public float amplitude = 2f;

    [Min(0.0001f)]
    public float wavelength = 1f;     

    public float speed = 2f;
    public float blockSpacing = 1.5f;

    private Vector3[] basePositions;

    private void Awake()
    {
        int n = blocks.Length;
        basePositions = new Vector3[n];
        Vector3 origin = transform.position;

        for (int i = 0; i < n; i++)
        {
            basePositions[i] = origin + Vector3.right * (i * blockSpacing);
            blocks[i].position = basePositions[i];
        }
    }

    private void Update()
    {
        float λ = Mathf.Approximately(wavelength, 0f) ? 0.0001f : wavelength;

        for (int i = 0; i < blocks.Length; i++)
        {
            float phase = (2 * Mathf.PI / λ) * i;
            float theta = Time.time * speed - phase;
            float yOffset = Mathf.Sin(theta) * amplitude;
            blocks[i].position = basePositions[i] + Vector3.up * yOffset;
        }
    }
}
