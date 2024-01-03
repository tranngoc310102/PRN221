using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN221
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			txtName.Text = "Trần Văn Ngọc";
			txtCode.Text = "HE163823";
			txtPass.Password = "12345678";
			txtEmail.Text = "ngoctvhe163823@fpt.edu.vn";
			rdMale.IsChecked = true;
			cbScholar.IsChecked = false;
			txtDate.SelectedDate = new DateTime(2002, 01, 31);
		}

		private void Btn_Submit_Click(object sender, RoutedEventArgs e)
		{
			String user = "Student Name: " + txtName.Text + "\n" + "StudentCode: " + txtCode.Text + "\n" +
				"Password: " + txtPass.Password + "\n";
			user += "Student Email: " + txtEmail.Text + "\n" + "Gender: ";
			if (rdMale.IsChecked == true) user += "Male";
			else user += "Female";
			user += "\t\t" + "Scholarship: ";
			if (cbScholar.IsChecked == true) user += "Yes";
			else user += "No" + "\n";
			user += "Date of birth: " + txtDate.Text + "\n" + "Narrow Specialization : " +
					cbxNarrow.Text + "\n" + "Favorite Color : " + cbxColor.Text;
			MessageBox.Show(user);
		}
	}
}
