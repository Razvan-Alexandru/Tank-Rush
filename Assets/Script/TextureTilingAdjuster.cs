using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTilingAdjuster : MonoBehaviour
{
    void Awake() {

        Material materialInstance = new Material(GetComponent<Renderer>().sharedMaterial);
        GetComponent<Renderer>().material = materialInstance;

        Vector2 randomOffset = new Vector2(Random.Range(0f, 5f), Random.Range(0f, 5f));
        materialInstance.mainTextureOffset = randomOffset;

        float tilingFactorX = transform.localScale.x;
        float tilingFactorY = transform.localScale.y;
        Vector2 tiling = new Vector2(tilingFactorX, tilingFactorY);


        materialInstance.mainTextureScale = tiling;
    }
}
