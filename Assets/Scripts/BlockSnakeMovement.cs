using UnityEngine;

public class BlockSnakeMovement : MonoBehaviour
{
    public Transform[] blocks;          // Array of 7 blocks
    public float waveAmplitude = 2f;    // How high each block moves
    public float waveSpeed = 2f;        // Speed of wave movement
    public float waveLength = 1f;       // Distance between each block's wave phase

    private Vector3[] initialPositions;

    private void Start()
    {
        // Store starting positions of each block
        initialPositions = new Vector3[blocks.Length];
        for (int i = 0; i < blocks.Length; i++)
        {
            initialPositions[i] = blocks[i].position;
        }
    }

    private void Update()
    {
        float time = Time.time * waveSpeed;

        for (int i = 0; i < blocks.Length; i++)
        {
            // Offset phase for each block to create wave effect
            float phaseOffset = i * waveLength;
            float yOffset = Mathf.Sin(time - phaseOffset) * waveAmplitude;

            Vector3 pos = initialPositions[i];
            pos.y += yOffset;
            blocks[i].position = pos;
        }
    }
}
