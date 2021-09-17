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

        public Matrix(Matrix toCopy)
        {
            rows = toCopy.rows;
            columns = toCopy.columns;
            data = toCopy.data;
        }

        public static Matrix operator +(Matrix a) => a;

        public static Matrix operator -(Matrix a)
        {
            Matrix result = new Matrix(a);

            for (int i = 0; i < result.rows; ++i)
                for (int j = 0; j < result.columns; ++j)
                    result.data[i, j] = -result.data[i, j];

            return result;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a);

            for (int i = 0; i < result.rows; ++i)
                for (int j = 0; j < result.columns; ++j)
                    result.data[i, j] += b.data[i, j];

            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b) => a + (-b);

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.columns == b.rows)
                return new Matrix();

            Matrix result = new Matrix(a.rows, b.columns);

            return result;
        }

        public Matrix GetInverted()
        {
            Matrix result = new Matrix(this);

            return result;
        }

        public static Matrix operator /(Matrix a, Matrix b) => a * b.GetInverted();
    }
}
