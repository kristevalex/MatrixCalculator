using UnityEngine;
using UnityEngine.UI;

public class MatrixInput : MatrixUI
{
    InputField[,] matrixCells;
    

    override protected void ResizeCells(int rows, int columns)
    {
        matrixCells = new InputField[rows, columns];
    }

    override protected void AssignToCell(int row, int column, GameObject toAssign)
    {
        matrixCells[row, column] = toAssign.GetComponentInChildren<InputField>();
    }

    override protected void UpdateDisplay()
    {
        for (int i = 0; i < matrix.rows; i++)
            for (int j = 0; j < matrix.columns; j++)
                matrixCells[i, j].text = matrix.data[i, j].ToString();
    }
}
