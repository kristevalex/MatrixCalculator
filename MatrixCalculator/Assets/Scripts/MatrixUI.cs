using UnityEngine;

public abstract class MatrixUI : MonoBehaviour
{
    public MatrixCalc.Matrix matrix { get; protected set; }

    abstract public void ResizeMatrix(int rows, int colomns);

    abstract public void UpdateMatrix(MatrixCalc.Matrix _matrix);

    public void ResizeAndUpdateMatrix(MatrixCalc.Matrix _matrix)
    {
        ResizeMatrix(_matrix.rows, _matrix.columns);
        UpdateMatrix(_matrix);
    }
}
