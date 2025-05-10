using UnityEngine;

public class BlockSnakeMovement : MonoBehaviour
{
    public Transform[] blocks;      
    public float moveDistance = 2f; 
    public float delayBetweenMoves = 0.2f; 
    public float moveSpeed = 2f;    
    public float pauseTime = 0.5f;  

    private void Start()
    {
        StartCoroutine(MoveBlocksLoop());
    }

    private System.Collections.IEnumerator MoveBlocksLoop()
    {
        while (true)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                yield return StartCoroutine(MoveBlock(blocks[i], Vector3.up * moveDistance));
                yield return new WaitForSeconds(delayBetweenMoves);
            }

            yield return new WaitForSeconds(pauseTime);

            for (int i = blocks.Length - 1; i >= 0; i--)
            {
                yield return StartCoroutine(MoveBlock(blocks[i], Vector3.down * moveDistance));
                yield return new WaitForSeconds(delayBetweenMoves);
            }

            yield return new WaitForSeconds(pauseTime);
        }
    }

    private System.Collections.IEnumerator MoveBlock(Transform block, Vector3 offset)
    {
        Vector3 startPos = block.position;
        Vector3 targetPos = startPos + offset;
        float elapsedTime = 0;

        while (elapsedTime < 0)
        {
            block.position = Vector3.Lerp(startPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        block.position = targetPos;
    }

}
