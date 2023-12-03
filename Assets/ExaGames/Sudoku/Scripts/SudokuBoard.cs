using ExaGames.Common;

namespace ExaGames.SudokuGenerator {
	/// <summary>
	/// Sudoku board generator.
	/// </summary>
	/// <author>ExaGames</author>
	public class SudokuBoard {
		private const int SIZE = 9;
		public int[,] Board { get; private set; }
	
		/// <summary>
		/// Creates a new random Sudoku board.
		/// </summary>
		public SudokuBoard() {
			Board = new int[SIZE, SIZE];
			bool created = false;
			while(!created) {
				Board = new int[SIZE, SIZE];
				created = place(1);
			}
		}
	
		/// <summary>
		/// Gets a random puzzle for this sudoku board.
		/// </summary>
		/// <remarks>
		/// Puzzles are 9x9 matrices with just 3 values filled for every row, column,
		/// and quadrant. Every "empty" value contains a zero.
		/// </remarks>
		/// <returns>The puzzle.</returns>
		public int[,] GetPuzzle(int NumberFilled) {
			int[,] puzzle = new int[SIZE, SIZE];
			RandomList<int> random = initRandomList(false);
			for(int i = 0; i < SIZE; i++) {
				if(random.IsEmpty()) {
					random = initRandomList(false);
				}
				for(int j = 0; j < NumberFilled; j++) {
					int column = random.GetRandom();
					puzzle[i, column] = Board[i, column];
				}
			}
			return puzzle;
		}
	
		/// <summary>
		/// Places the specified value in all rows of the board.
		/// </summary>
		/// <param name="value">Value to be placed, from 1 to 9.</param>
		private bool place(int value) {
			if(value > SIZE) { // Check values from 1 to 9 (SIZE == 9)
				return true;
			} else {
				bool placed = false; // Could this value be placed in all rows?
				if(place(value, 0, initRandomList(false))) {
					placed = place(value + 1);
				}
				return placed;
			}
		}
	
		/// <summary>
		/// Place the specified value in the specified row, selecting a random column.
		/// </summary>
		/// <param name="value">Value to place.</param>
		/// <param name="row">Row to place the value in.</param>
		/// <param name="availableColumns">List of available columns.</param>
		private bool place(int value, int row, RandomList<int> availableColumns) {
			if(row == SIZE) { // Check rows from 0 to 8 (8<SIZE; SIZE == 9)
				return true;
			} else {
				bool placed = false;
				uint excludeFromRandom = 0;
				while(!placed && !availableColumns.IsEmpty(excludeFromRandom)) {
					int column = availableColumns.GetRandom(excludeFromRandom, true);
					if(canPlace(value, row, column)) {
						Board[row, column] = value;
						placed = place(value, row + 1, availableColumns);
						if(!placed) {
							//							excludeFromRandom = 0;
							Board[row, column] = 0;
						}
					} 
					if(!placed) {
						availableColumns.Append(column);
						excludeFromRandom++;	
					}
				}
				return placed;
			}
		}
	
		/// <summary>
		/// Checks whether the value can be placed in the specified row and column of the board.
		/// </summary>
		/// <returns><c>true</c>, if place was caned, <c>false</c> otherwise.</returns>
		/// <param name="value">Value.</param>
		/// <param name="inRow">In row.</param>
		/// <param name="inColumn">In column.</param>
		private bool canPlace(int value, int inRow, int inColumn) {
			int quadrantVert = inRow / 3;
			int quadrantHor = inColumn / 3;
			// Checks if the position is free.
			if(Board[inRow, inColumn] > 0) {
				return false;
			}
			// Checks the quadrant so far;
			for(int i = 3 * quadrantVert; i < inRow; i++) {
				for(int j = 3 * quadrantHor; j < 3 * (quadrantHor + 1); j++) {
					if(Board[i, j] == value) {
						return false;
					}
				}
			}
			return true;
		}
	
		private RandomList<int> initRandomList(bool offsetByOne = true) {
			int offset = offsetByOne ? 1 : 0;
			RandomList<int> randomList = new RandomList<int>();
			for(int i = offset; i < SIZE + offset; i++) {
				randomList.Append(i);
			}
			return randomList;
		}
	
		public override string ToString() {
			return Board.MatrixToString();
		}
	}
}