namespace MatrixCalc
{
    public class Matrix
    {
        public int rows { get; private set; }
        public int columns { get; private set; }
        public double[,] data { get; set; }

        public Matrix(int _rows = 0, int _columns = 0)
        {
            if (_columns == 0)
                _columns = _rows;

            rows = _rows;
            columns = _columns;
            data = new double[rows, columns];
        }

        public static Matrix operator +(Matrix a) => a;

        public static Matrix operator -(Matrix a)
        {
            Matrix result = a;

            for (int i = 0; i < result.rows; ++i)
                for (int j = 0; j < result.columns; ++j)
                    result.data[i, j] = -result.data[i, j];

            return result;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix result = a;

            for (int i = 0; i < result.rows; ++i)
                for (int j = 0; j < result.columns; ++j)
                    result.data[i, j] += b.data[i, j];

            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b) => a + (-b);

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.columns != b.rows)
                return new Matrix();

            Matrix result = new Matrix(a.rows, b.columns);

            double cellValue = 0;
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < b.columns; j++)
                {
                    for (int n = 0; n < a.columns; n++)
                    {
                        cellValue += b.data[i, n] * b.data[n, j];
                    }
                    result.data[i, j] = cellValue;
                    cellValue = 0;
                }
            }

            return result;
        }

        public Matrix GetInverted()
        {
            Matrix result = this;

            return result;
        }

        public Matrix GetTransposed()
        {
            Matrix result = new Matrix(columns, rows);

            for (int i = 0; i < result.rows; ++i)
                for (int j = 0; j < result.columns; ++j)
                    result.data[i, j] = data[j, i];

            return result;
        }

        public static Matrix operator /(Matrix a, Matrix b) => a * b.GetInverted();
    }
}
