using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TestTool.DAL
{
	public class QuestionStore
	{
		public	string Name { get; set; }
		public List<Question> Questions { get; set; }

		public QuestionStore()
		{
			this.Name = "Тема опросника";
			this.Questions = new List<Question>();
		}

		public void AddQuestion(string qText, string[] replies, int correctIndex)
		{
			this.Questions.Add(new Question(qText, replies, correctIndex));
		}
		public void RemoveQuestion(int index)
		{
			this.Questions.RemoveAt(index);
		}


		public void EditQuestion(int index, string qText, string[] replies, int correctIndex)
		{
			var newQuestion = new Question(qText, replies, correctIndex);
			this.Questions[index] = newQuestion;
		}
		public static QuestionStore LoadStore(string filePath)
		{
			XmlSerializer ser = new XmlSerializer(typeof(QuestionStore));
			TextReader reader = new StreamReader(filePath);
			var res = ser.Deserialize(reader);
			if (res is QuestionStore)
			{
				return (res as QuestionStore);
			}
			else
			{
				return null;
			}
		}

		public void SaveStore(string filePath)
		{
			XmlSerializer ser = new XmlSerializer(typeof(QuestionStore));
			TextWriter writer = new StreamWriter(filePath);
			ser.Serialize(writer, this);
			writer.Close();
        }
	}
}
