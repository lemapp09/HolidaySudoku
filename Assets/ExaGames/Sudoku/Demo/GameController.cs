using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExaGames.SudokuGenerator;

/// <summary>
/// Demo game controller for a Sudoku board.
/// </summary>
/// <author>ExaGames</author>
/// <description>
/// 1. Create a new board:
/// 	SudokuBoard sudokuBoard = new SudokuBoard();
/// 2. Get a random puzzle for a SudokuBoard:
/// 	int[,] puzzle = sudokuBoard.GetPuzzle();
/// 3. Check solution: compare your player inputs with the sudokuBoard.Board property.
/// </description>
public class GameController : MonoSingleton<GameController> {
	/// <summary>
	/// Timer label.
	/// </summary>
	public TextMeshProUGUI Timer;
	/// <summary>
	/// Message label.
	/// </summary>
	public TextMeshProUGUI Message;
	// Arrays containing the input fields of the board.
	public InputField[]	Row1;
	public InputField[]	Row2;
	public InputField[]	Row3;
	public InputField[]	Row4;
	public InputField[]	Row5;
	public InputField[]	Row6;
	public InputField[]	Row7;
	public InputField[]	Row8;
	public InputField[]	Row9;

	public Slider difficultySlider;
	
	/// <summary>
	/// Main sudoku board.
	/// </summary>
	private SudokuBoard sudokuBoard;
	/// <summary>
	/// Unsolved puzzle, with zero's in those positions which should be empty.
	/// </summary>
	private int[,] puzzle;
	/// <summary>
	/// Array of input fields arrays, for easy access inside loops.
	/// </summary>
	private InputField[][] fields;

	private int currentSelectedNumber = 0;
	public int CurrentSelectedNumber => currentSelectedNumber;
	
	void Start() {
		// Store references to the input field arrays for easy access inside loops.
		fields = new InputField[9][];
		fields[0] = Row1;
		fields[1] = Row2;
		fields[2] = Row3;
		fields[3] = Row4;
		fields[4] = Row5;
		fields[5] = Row6;
		fields[6] = Row7;
		fields[7] = Row8;
		fields[8] = Row9;

		OnNewGameButtonPressed();
	}

	/// <summary>
	/// Creates a new sudoku board.
	/// </summary>
	public void OnNewGameButtonPressed() {
		StopCoroutine("countTime");
		
		sudokuBoard = new SudokuBoard();	// Create a sudoku board.
		puzzle = sudokuBoard.GetPuzzle((int)difficultySlider.value);	// Get a random puzzle for the created board.
		
		OnClearButtonPressed();
		
		StartCoroutine("countTime");
	}

	/// <summary>
	/// Compares the player inputs with the board.
	/// </summary>
	public void OnCheckButtonPressed() {
		bool solved = true;
		for(int i=0; i<9; i++) {
			for(int j = 0; j<9; j++) {
				if(string.IsNullOrEmpty(fields[i][j].text) || Convert.ToInt32(fields[i][j].text) != sudokuBoard.Board[i, j]) {
					solved = false;
					break;
				}
			}
			if(!solved)
				break;
		}
		if(!solved) {
			Message.text = "Keep trying";
		} else {
			Message.text = "Congratulations!";
			StopCoroutine("countTime");
		}
		Message.gameObject.SetActive(true);
	}

	/// <summary>
	/// Copy the puzzle to the input fields.
	/// </summary>
	public void OnClearButtonPressed() {
		for(int i=0; i<9; i++) {
			for(int j = 0; j<9; j++) {
				if(puzzle[i, j] > 0) {
					fields[i][j].text = puzzle[i, j].ToString();
					fields[i][j].interactable = false;
					fields[i][j].textComponent.color = Color.red;
				} else {
					fields[i][j].text = string.Empty;
					fields[i][j].interactable = true;
					fields[i][j].textComponent.color = Color.black;
				}
			}
		}
		Message.gameObject.SetActive(false);
	}

	/// <summary>
	/// Copy the entire board to the input fields.
	/// </summary>
	public void OnSolveButtonPressed() {
		StopCoroutine("countTime");
		for(int i=0; i<9; i++) {
			for(int j = 0; j<9; j++) {
				if(puzzle[i, j] > 0) {
					fields[i][j].text = puzzle[i, j].ToString();
					fields[i][j].interactable = false;
					fields[i][j].textComponent.color = Color.red;
				} else {
					fields[i][j].text = sudokuBoard.Board[i, j].ToString();
					fields[i][j].interactable = false;
					fields[i][j].textComponent.color = Color.black;
				}
			}
		}
		Message.gameObject.SetActive(false);
	}

	/// <summary>
	/// Coroutine to count time and print it in the message label.
	/// </summary>
	/// <returns>The time.</returns>
	private IEnumerator countTime() {
		int timer = 0;
		Timer.text = string.Format("Time: {0:D2}:{1:D2}", 0, 0);
		while(true) {
			yield return new WaitForSeconds(1f);
			timer++;
			TimeSpan t = TimeSpan.FromSeconds(timer);
			Timer.text = string.Format("Time: {0:D2}:{1:D2}", t.Minutes, t.Seconds);
		}
	}

	public void ChangeSelectedNumber(int newSelectedNumber) {
		currentSelectedNumber = newSelectedNumber;
	}
}