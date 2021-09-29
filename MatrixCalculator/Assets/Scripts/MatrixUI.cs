using UnityEngine;
using UnityEngine.UI;

public abstract class MatrixUI : MonoBehaviour
{
    [SerializeField]
    GameObject cellObjectPrefab;
    [SerializeField]
    GameObject forCells;

    Vector2 matrixSize;
    GridLayoutGroup grid;

    public MatrixCalc.Matrix matrix { get; protected set; }


    void Start()
    {
        MatrixCalc.Matrix _matrix = new MatrixCalc.Matrix(2, 3);
        for (int i = 0; i < _matrix.rows; i++)
            for (int j = 0; j < _matrix.columns; j++)
                _matrix.data[i, j] = 10 * i + j;

        grid = forCells.GetComponent<GridLayoutGroup>();

        matrixSize = forCells.GetComponent<RectTransform>().sizeDelta;

        SetMatrixValue(_matrix);
    }

    abstract protected void ResizeCells(int rows, int columns);

    abstract protected void AssignToCell(int row, int column, GameObject toAssign);

    abstract protected void UpdateDisplay();

    public void ResizeMatrix(int rows, int columns)
    {
        matrix = new MatrixCalc.Matrix(rows, columns);

        foreach (Transform child in forCells.transform)
            GameObject.Destroy(child.gameObject);

        ResizeCells(rows, columns);

        grid.constraintCount = matrix.columns;
        grid.cellSize = new Vector2(0.9f * (matrixSize.x - 2 * 10) / matrix.columns, 0.9f * (matrixSize.y - 2 * 10) / matrix.rows);
        grid.spacing = new Vector2(0.1f * (matrixSize.x - 2 * 10) / matrix.columns, 0.1f * (matrixSize.y - 2 * 10) / matrix.rows);
        grid.padding = new RectOffset(10, 10, 10, 10);

        GameObject tmpCell;
        for (int i = 0; i < matrix.rows; i++)
        {
            for (int j = 0; j < matrix.columns; j++)
            {
                tmpCell = Instantiate(cellObjectPrefab);
                tmpCell.transform.SetParent(forCells.transform);
                tmpCell.name = "Cell (" + i + "," + j + ")";
                tmpCell.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                AssignToCell(i, j, tmpCell);
            }
        }

        UpdateDisplay();
    }

    public void SetMatrixValue(MatrixCalc.Matrix _matrix)
    {
        if (matrix == null || matrix.rows != _matrix.rows || matrix.columns != _matrix.columns)
            ResizeMatrix(_matrix.rows, _matrix.columns);

        matrix = _matrix;
        UpdateDisplay();
    }
}
