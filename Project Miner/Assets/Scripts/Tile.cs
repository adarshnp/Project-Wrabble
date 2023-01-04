using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _normalTileColor, _doubleLetterTileColor, _tripleLetterTileColor, _doubleWordTileColor, _tripleWordTileColor, _centerTileColor;
    [SerializeField] private SpriteRenderer _renderer;

    public void Init(GlobalVariables.TileType tileType)
    {
        switch (tileType)
        {
            case GlobalVariables.TileType.Normal:
                _renderer.color = _normalTileColor;
                break;
            case GlobalVariables.TileType.DoubleLetter:
                _renderer.color = _doubleLetterTileColor;
                break;
            case GlobalVariables.TileType.TripleLetter:
                _renderer.color = _tripleLetterTileColor;
                break; 
            case GlobalVariables.TileType.DoubleWord:
                _renderer.color = _doubleWordTileColor;
                break;
            case GlobalVariables.TileType.TripleWord:
                _renderer.color = _tripleWordTileColor;
                break;
            case GlobalVariables.TileType.Center:
                _renderer.color = _centerTileColor;
                break;
            default:
                _renderer.color = _normalTileColor;
                break;
        }
    }
}
