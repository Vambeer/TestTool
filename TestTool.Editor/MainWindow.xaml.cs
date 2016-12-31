using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestTool.DAL;

namespace TestTool.Editor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

		}

		private QuestionStore store;

		private void btnCreate_Click(object sender, RoutedEventArgs e)
		{
			store = new QuestionStore();
			tbStoreName.Text = store.Name;
			lbQuestions.ItemsSource = store.Questions;
		}

		private void btnOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".xml";
			ofd.Filter = "XML Files (*.xml)|*.xml";
			Nullable<bool> result = ofd.ShowDialog();
			if (result == true)
			{
				string fileName = ofd.FileName;
				store = QuestionStore.LoadStore(fileName);
				tbStoreName.Text = store.Name;
				//lbQuestions.DataContextChanged += LbQuestions_DataContextChanged;
				lbQuestions.ItemsSource = store.Questions;
				

			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog ofd = new SaveFileDialog();
			ofd.DefaultExt = ".xml";
			ofd.Filter = "XML Files (*.xml)|*.xml";
			Nullable<bool> result = ofd.ShowDialog();
			if (result == true)
			{
				string fileName = ofd.FileName;
				store.SaveStore(fileName);

			}
		}

		private void btnCreateQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (store != null)
			{
				//store.AddQuestion("Текст вопроса", new string[] { "Отчет1", "Ответ 2" }, 0);
				//lbQuestions.ItemsSource = store.Questions;
				ShowQuestion questionWindow = new ShowQuestion();
				var res = questionWindow.ShowDialog();
				
				if (res.Value)
				{
					string text = questionWindow.Text;
					store.AddQuestion(text, new string[] { }, -1);
					lbQuestions.ItemsSource = store.Questions;
				}
			}
		}

		private void btnDeleteQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (store != null && lbQuestions.SelectedIndex > -1)
			{
				store.RemoveQuestion(lbQuestions.SelectedIndex);
				lbQuestions.ItemsSource = store.Questions;
			}
		}

		private void btnCreateAnswer_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnDeleteAnswer_Click(object sender, RoutedEventArgs e)
		{

		}

		private void lbQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lbQuestions.SelectedItem != null)
			{
				lbAnswers.ItemsSource = (lbQuestions.SelectedItem as Question).Replies;
			}
		}

	}
}
