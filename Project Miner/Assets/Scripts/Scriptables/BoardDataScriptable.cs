using UnityEngine;
[CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/Board", order = 1)]
public class BoardDataScriptable : ScriptableObject
{
    public int CellCount;
    public float CellWidth;
    public float CellPadding;
    public float WallsWidth;
}
