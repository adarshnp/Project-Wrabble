using UnityEngine;
[CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/Cell", order = 2)]
public class CellDataScriptable : ScriptableObject
{
    public enum CellType { Normal, Double_letter, Triple_Letter,Double_Word,Triple_Word }
    public CellType type;
    public TileDataScriptable droppedTile;
}
