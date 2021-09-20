using UnityEngine;
using UnityEngine.UI;

public class MatrixDisplay : MatrixUI
{
    [SerializeField]
    GameObject displayCellPrefab;
    [SerializeField]
    GameObject forCells;

    Vector2 matrixSize;
    GridLayoutGroup grid;

    Text[,] matrixCells;

    void Start()
    {
        MatrixCalc.Matrix _matrix = new MatrixCalc.Matrix(2, 3);
        for (int i = 0; i < _matrix.rows; i++)
            for (int j = 0; j < _matrix.columns; j++)
                _matrix.data[i, j] = 10 * i + j;

        grid = forCells.GetComponent<GridLayoutGroup>();

        matrixSize = forCells.GetComponent<RectTransform>().sizeDelta;

        UpdateMatrix(_matrix);
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < matrix.rows; i++)
            for (int j = 0; j < matrix.columns; j++)
                matrixCells[i, j].text = matrix.data[i, j].ToString();
    }

    override public void ResizeMatrix(int rows, int colomns)
    {
        matrix = new MatrixCalc.Matrix(rows, colomns);

        foreach (Transform child in forCells.transform)
            GameObject.Destroy(child.gameObject);

        matrixCells = new Text[matrix.rows, matrix.columns];

        grid.constraintCount = matrix.columns;
        grid.cellSize = new Vector2(0.9f * (matrixSize.x - 2 * 10) / matrix.columns, 0.9f * (matrixSize.y - 2 * 10) / matrix.rows);
        grid.spacing = new Vector2(0.1f * (matrixSize.x - 2 * 10) / matrix.columns, 0.1f * (matrixSize.y - 2 * 10) / matrix.rows);
        grid.padding = new RectOffset(10, 10, 10, 10);

        for (int i = 0; i < matrix.rows; i++)
        {
            for (int j = 0; j < matrix.columns; j++)
            {
                GameObject tmpCell = Instantiate(displayCellPrefab);
                tmpCell.transform.SetParent(forCells.transform);
                tmpCell.name = "Cell (" + i + "," + j + ")";
                tmpCell.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                matrixCells[i, j] = tmpCell.GetComponentInChildren<Text>();
            }
        }

        UpdateDisplay();
    }

    override public void UpdateMatrix(MatrixCalc.Matrix _matrix)
    {
        if (matrix.rows != _matrix.rows || matrix.columns != _matrix.columns)
            return;

        matrix = _matrix;
        UpdateDisplay();
    }
}
