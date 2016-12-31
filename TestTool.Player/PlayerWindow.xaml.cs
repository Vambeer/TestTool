using System;
using System.Collections.Generic;
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
	/// Interaction logic for PlayerWindow.xaml
	/// </summary>
	public partial class PlayerWindow : Window
	{
		public PlayerWindow()
		{
			InitializeComponent();
		}

		private QuestionStore _store;
		private int currentQuestion, maxCount;
		private List<int> selectedIndex = new List<int>();
		private string _fullName;
		private DateTime _startTime;

		public PlayerWindow(QuestionStore store, string fullName)
		{
			InitializeComponent();
			_store = store;
			this._fullName = fullName;
			this._startTime = DateTime.Now;
			currentQuestion = 0;
			maxCount = store.Questions.Count;
			for (int i =0; i< maxCount; i++)
			{
				selectedIndex.Add(-1);
			}
		}

		private void btnPrev_Click(object sender, RoutedEventArgs e)
		{
			selectedIndex[currentQuestion] = lbAnswers.SelectedIndex;

			if (currentQuestion >  0)
			{
				currentQuestion--;
			}
			tbQuestionName.Text = _store.Questions[currentQuestion].Text;
			lbAnswers.ItemsSource = _store.Questions[currentQuestion].Replies;
			if (selectedIndex[currentQuestion] > -1)
			{
				lbAnswers.SelectedIndex = selectedIndex[currentQuestion];
            }
			if (currentQuestion != maxCount - 1)
			{
				btnNext.Content = "Следующий вопрос";
			}

			tbStatus.Text = String.Format("Вопрос {0} из {1}", currentQuestion + 1, maxCount);
			lbAnswers.Focus();
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			selectedIndex[currentQuestion] = lbAnswers.SelectedIndex;

			if (currentQuestion < maxCount - 1)
			{
				currentQuestion++;
			}
			else
			{
				//Тут завершение теста
				Result rWindow = new Result(_store, selectedIndex, _fullName, _startTime);
				

				App.Current.MainWindow = rWindow;
				this.Close();
				rWindow.Show();

			}
			if (currentQuestion == maxCount - 1)
			{
				btnNext.Content = "Завершить тест";
			}
			tbQuestionName.Text = _store.Questions[currentQuestion].Text;
			lbAnswers.ItemsSource = _store.Questions[currentQuestion].Replies;
			if (selectedIndex[currentQuestion] > -1)
			{
				lbAnswers.SelectedIndex = selectedIndex[currentQuestion];
			}
			tbStatus.Text = String.Format("Вопрос {0} из {1}", currentQuestion + 1, maxCount);
			lbAnswers.Focus();
		}

		private void lbAnswers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			btnNext_Click(this, null);
        }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			tbQuestionName.Text = _store.Questions[0].Text;
			lbAnswers.ItemsSource = _store.Questions[0].Replies;
			tbStatus.Text = String.Format("Вопрос {0} из {1}", currentQuestion + 1, maxCount);
        }
	}
}
