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

        public Matrix(int _rows, int _columns, double _value)
        {
            rows = _rows;
            columns = _columns;
            data = new double[rows, columns];

            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    data[i, j] = _value;
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
                        cellValue += b.data[i, n] * b.data[n, j];

                    result.data[i, j] = cellValue;
                    cellValue = 0;
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix a, double b)
        {
            Matrix result = a;

            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < a.columns; j++)
                    result.data[i, j] *= b;

            return result;
        }

        public static Matrix operator /(Matrix a, Matrix b) => a * b.GetInverted();

        public static Matrix operator /(Matrix a, double b) => a * (1.0f / b);

        public Matrix GetInverted()
        {
            return GetInvertedUsingAdjointMaethod();
        }

        private Matrix GetCofactor(int p, int q)
        {
            Matrix result = new Matrix(rows - 1, columns - 1);
            int i = 0, j = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++) // Copy only those elements which are not in given row r and column c
                    if (r != p && c != q)
                    {
                        result.data[i, j++] = data[r, c]; // If row is filled increase row index and reset column index
                        if (j == columns - 1)
                        {
                            j = 0; 
                            i++;
                        }
                    }

            return result;
        }
    
        double GetDeterminant() 
        {
            if (rows != columns)
                return double.NaN;

            if (rows == 1)
                return data[0, 0];

            if (rows == 2)
                return data[0, 0] * data[1, 1] - data[0, 1] * data[1, 0];

            double determinant = 0;
            int sign = 1;
            for (int i = 0; i < rows; i++)
            {
                Matrix subMatrix = GetCofactor(0, i);
                determinant += sign * data[0, i] * subMatrix.GetDeterminant();
                sign = -sign;
            }

            return determinant;
        }

        private Matrix GetAdjoint() 
        {
            if (rows != columns)
                return new Matrix();

            if (rows == 1)
                return new Matrix(1, 1, 1); 
            int s = 1,
            t[N][N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    //To get cofactor of M[i][j]
                    getCfactor(M, t, i, j, N);
                    s = ((i + j) % 2 == 0) ? 1 : -1; //sign of adj[j][i] positive if sum of row and column indexes is even.
                    adj[j][i] = (s) * (DET(t, N - 1)); //Interchange rows and columns to get the transpose of the cofactor matrix
                }
            }
        }

        public Matrix GetInvertedUsingAdjointMaethod()
        {
            double det = GetDeterminant();
            if (det == double.NaN || det == 0)
                return new Matrix();

            return GetAdjoint() / det;            
        }

        public Matrix GetTransposed()
        {
            Matrix result = new Matrix(columns, rows);

            for (int i = 0; i < result.rows; ++i)
                for (int j = 0; j < result.columns; ++j)
                    result.data[i, j] = data[j, i];

            return result;
        }
    }
}
