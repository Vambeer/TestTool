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


namespace TestTool.Player
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

		private void btnSelectFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".xml";
			ofd.Filter = "XML Files (*.xml)|*.xml";
			Nullable<bool> result = ofd.ShowDialog();
			if (result == true)
			{
				string fileName = ofd.FileName;
				store = QuestionStore.LoadStore(fileName);
				cbCount.Items.Clear();
				for (int i=1; i<= store.Questions.Count; i++)
				{
					cbCount.Items.Add(i);
				}

				cbCount.IsEnabled = true;
			}
		}
		
		private void btnStartTest_Click(object sender, RoutedEventArgs e)
		{
			int count = (int)cbCount.SelectedItem;
			
			//Выбираем случайное количесво вопросов
			QuestionStore st = new QuestionStore();
			st.Name = store.Name;
			st.Questions.AddRange(store.Questions);
			Random rdn = new Random();
			for (int i =0; i< store.Questions.Count - count; i++)
			{
				st.Questions.RemoveAt(rdn.Next(st.Questions.Count - 1));
			}
			

			PlayerWindow plWindow = new PlayerWindow(st, tbFullName.Text);
			
			App.Current.MainWindow = plWindow;
			this.Close();
			plWindow.Show();
		}

		private void cbCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selection = cbCount.SelectedItem;
			try
			{
				var cnt = (Int32)selection;
				tbFullName.IsEnabled = true;
			}
			catch { }

		}

		private void tbFullName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!String.IsNullOrEmpty(tbFullName.Text))
			{
				btnStartTest.IsEnabled = true;
			}
			else
			{
				btnStartTest.IsEnabled = false;
			}
		}
	}
}
