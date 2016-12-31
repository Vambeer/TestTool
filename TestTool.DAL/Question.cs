using System;
using System.Collections.Generic;
using System.Text;

namespace TestTool.DAL
{
	public class Question
	{
		public string Text { get; set; }
		public List<Reply> Replies { get; set; }
		public bool CheckResult(int index)
		{
			return Replies[index].IsCorrect;
		}
		public Question (string text, string[] replies, int correctIndex)
		{

			this.Text = text;
			this.Replies = new List<Reply>();
			for (int i =0; i < replies.Length; i++)
			{
				this.Replies.Add(new Reply { Text = replies[i], IsCorrect = (correctIndex == i) });
			}
		}
		public Question()
		{
			this.Text = "Вопрос не указан";
			this.Replies = new List<Reply>();
		}
	}

	//public class QuestionWithResult : Question
	//{
	//	public List<ReplySelection> Answers { get; set; }
	//}
}
