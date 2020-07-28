using UnityEditor;
using UnityEngine;

public class PostprocessImages : AssetPostprocessor {

    void OnPostprocessSprites(Texture2D texture, Sprite[] sprite) {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spritePixelsPerUnit = 4;
        textureImporter.spritePivot = Vector2.down;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
    }
}