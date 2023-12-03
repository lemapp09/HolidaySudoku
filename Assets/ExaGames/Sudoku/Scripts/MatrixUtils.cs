using System.Text;

namespace ExaGames.SudokuGenerator {
	public static class MatrixUtils {
		public static string MatrixToString<T>(this T[,] matrix) {
			StringBuilder result = new StringBuilder();
			for(int i = 0; i < matrix.GetLength(0); i++) {
				for(int j = 0; j < matrix.GetLength(1); j++) {
					result.Append(matrix[i, j].ToString()).Append(",");
				}
				result.Remove(result.Length - 1, 1).Append("\n");
			}
			return result.ToString();
		}
	}
}
