using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestTool.DAL;

namespace TestTool.Player
{
	/// <summary>
	/// Interaction logic for Result.xaml
	/// </summary>
	public class QuestionResult
	{
		public string Text { get; set; }
		public string CorrectAnswer { get; set; }
		public string ProvidedAnswer { get; set; }

		public bool IsCorrect { get; set; }

		public Color BgColor { get; set; }
	}
	public partial class Result : Window
	{
		public Result()
		{
			InitializeComponent();
		}

		private List<QuestionResult> result;
		private QuestionStore _store;
		private string _fullName;
		private DateTime _startTime;
		int correctAnswers;
		public Result(QuestionStore store, List<int> selectedAnswers, string fulllName, DateTime startTime)
		{
			InitializeComponent();

			this._store = store;
			this._startTime = startTime;
			this._fullName = fulllName;

			int maxCount = store.Questions.Count;
			correctAnswers = 0;
			result = new List<QuestionResult>();
			for (int i = 0; i < store.Questions.Count; i++)
			{
				QuestionResult r = new QuestionResult();
				try
				{
					r.CorrectAnswer = store.Questions[i].Replies[store.Questions[i].Replies.FindIndex(p => p.IsCorrect)].Text;
				}
				catch
				{
					r.CorrectAnswer = "Не указан";
				}

				r.ProvidedAnswer = selectedAnswers[i] >= 0 ? store.Questions[i].Replies[selectedAnswers[i]].Text : "Не выбрали ответ";
				r.IsCorrect = store.Questions[i].CheckResult(selectedAnswers[i]);
				r.Text = store.Questions[i].Text;
				if (r.IsCorrect)
				{
					correctAnswers++;
					r.Text = "ПРАВИЛЬНО. " + r.Text;
					r.BgColor = Color.FromArgb(120, 0, 200, 20);
					//r.ProvidedAnswer = "Вы ответили. " + r.ProvidedAnswer;
					r.CorrectAnswer = "";
				}
				else
				{
					r.Text = "НЕПРАВИЛЬНО. " + r.Text;
					r.BgColor = Color.FromArgb(120, 200, 20, 20);
					r.ProvidedAnswer = "Ваш ответ. " + r.ProvidedAnswer;
					r.CorrectAnswer = "Правильный ответ. " + r.CorrectAnswer;
				}
				result.Add(r);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			tbName.Text = String.Format("Тест по теме {0}, испытуемый {1}", _store.Name, _fullName);
			tbResult.Text = String.Format("Результат: {0} правильных из {1} ({2}% правильных). Время начала {3}, окончания {4}", 
				correctAnswers, 
				_store.Questions.Count, 
				(float)correctAnswers * 100 / _store.Questions.Count,
				_startTime.ToString("dd.MM.yyyy hh:mm"),
				DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
			lbAnswers.ItemsSource = result;

			StringBuilder builder = new StringBuilder();
			builder.AppendLine("<html>");
			builder.AppendLine("<body>");
			builder.AppendFormat("<h1>Тест по теме {0}, испытуемый {1}</h1>", _store.Name, _fullName);
			builder.AppendFormat("<h2>Результат: {0} правильных из {1} ({2}% правильных)</h2>", correctAnswers, _store.Questions.Count, (float)correctAnswers * 100 / _store.Questions.Count);
			builder.AppendFormat("<h2>Время начала - {0}, время окончания - {1}</h2>", _startTime.ToString("dd.MM.yyyy hh:mm"), DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
            builder.AppendLine("<hr />");
			builder.AppendLine("<ul>");
			foreach (var r in result)
			{
				if (String.IsNullOrEmpty(r.CorrectAnswer))
				{
					builder.AppendFormat("<li>{0}<ul><li>{1}</li></ul></li>", r.Text, r.ProvidedAnswer);
				}
				else
				{
					builder.AppendFormat("<li>{0}<ul><li>{1}</li><li>{2}</li></ul></li>", r.Text, r.ProvidedAnswer, r.CorrectAnswer);
				}
			}

			builder.AppendLine("</ul>");
			builder.AppendLine("</body>");
			builder.AppendLine("</html>");


			string html = builder.ToString();
			TextWriter writer = new StreamWriter(String.Format("{0}-{1}.html", _fullName, DateTime.Now.ToString("dd-MM-yyyy-hh-mm")),  false, Encoding.UTF8);
			writer.Write(html);
			writer.Close();
		}
	}
}