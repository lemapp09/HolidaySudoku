using System;

namespace ExaGames.Common {
	/// <summary>
	/// Allows to randomly select an item from a list, discard that item, and repeat with the remaining items.
	/// </summary>
	/// <author>ExaGames</author>
	public class RandomList<T> {
		/// <summary>
		/// Doubly Linked List Node.
		/// </summary>
		private class Node {
			public T Data;
			public Node Previous;
			public Node Next;
		
			public Node(T data) {
				Data = data;
				Previous = null;
				Next = null;
			}
		
			public override string ToString() {
				return Data.ToString();
			}
		}
	
		/// <summary>
		/// Start of the list.
		/// </summary>
		private Node start;
		/// <summary>
		/// End of the list.
		/// </summary>
		private Node end;
		/// <summary>
		/// Item count in the random list.
		/// </summary>
		private int count;
	
		public RandomList() {
		}

		public RandomList(T[] items) {
			if(items == null || items.Length < 1) {
				return;
			} else {
				for(int i = 0; i < items.Length; i++) {
					Append(items[i]);
				}
			}
		}
	
		public void Append(T data) {
			Node newNode = new Node(data);
			if(count == 0) {
				start = end = newNode;
			} else {
				newNode.Previous = end;
				end.Next = newNode;
				end = newNode;
			}
			count++;
		}
	
		/// <summary>
		/// Retrieves a random item from the list and removes it from the list.
		/// </summary>
		/// <returns>A random item from the list.</returns>
		/// <param name="exclude">Items to exclude at the end of the list.</param>
		/// <param name="throwErrorOnEmpty">If set to <c>true</c> throw error when list is empty, otherwise returns the data default value.</param>
		public T GetRandom(uint exclude = 0, bool throwErrorOnEmpty = false) {
			if(count > 0 && count > exclude) {
				Node p = start;
				int index = UnityEngine.Random.Range(0, count - (int)exclude);
				for(int i = 1; i <= index; i++) {
					if(p == null || p.Next == null) {
						throw new Exception("RandomList is corrupted.");
					}
					p = p.Next;
				}
				remove(p);
				return p.Data;
			} else {
				if(throwErrorOnEmpty) {
					throw new Exception("No values available");
				} else {
					return default(T);
				}
			}
		}
	
		/// <summary>
		/// Determines whether this random list, excluding the specified number of items, is empty.
		/// </summary>
		/// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
		/// <param name="exclude">Number of items to exclude.</param>
		public bool IsEmpty(uint exclude = 0) {
			return count - exclude <= 0;
		}
	
		private void remove(Node node) {
			if(node == start) {
				start = start.Next;
				if(start != null) {
					start.Previous = null;
				} else {
					end = null;
				}
			} else if(node == end) {
				end = end.Previous;
				end.Next = null;
			} else {
				node.Next.Previous = node.Previous;
				node.Previous.Next = node.Next;
			}
			node.Next = node.Previous = null;
			count--;
		}
	
		public override string ToString() {
			if(IsEmpty()) {
				return "Empty";
			} else {
				return string.Format("List with {0} items.", count);
			}
		}
	}
}