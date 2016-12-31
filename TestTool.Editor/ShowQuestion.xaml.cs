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

namespace TestTool.Editor
{
	/// <summary>
	/// Interaction logic for ShowQuestion.xaml
	/// </summary>
	public partial class ShowQuestion : Window
	{
		public ShowQuestion()
		{
			InitializeComponent();
		}

		public string Text { get; set; }

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			this.Text = tbText.Text;
			DialogResult = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Text = "";
			DialogResult = false;
			this.Close();
		}
	}
}
