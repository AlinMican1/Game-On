using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceControl : MonoBehaviour
{
    [SerializeField] WeaponWheelManager wheelManageScript;
    [SerializeField] Color normalColor;
    [SerializeField] Color highlightColor;
    [SerializeField][Range(0,1)] float alpha = 1;

    Image pieceImage;

    void Start()
    {
        pieceImage = GetComponent<Image>();
        var tempcolor = pieceImage.color;
        tempcolor.a = alpha;
        pieceImage.color = tempcolor;
    }

    void Update()
    {
        var tempcolor = pieceImage.color;
        tempcolor.a = alpha;
        pieceImage.color = tempcolor;

        if (wheelManageScript.WhichPiece() == this.gameObject)
        {
            pieceImage.color = highlightColor;
        }
        else
        {
            pieceImage.color = normalColor;
        }
    }
}
