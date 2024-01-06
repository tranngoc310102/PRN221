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
using ConnectDB.Models;

namespace ConnectDB
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

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var nameDe = PRN211_1Context.ins.Departments.ToList();
			var student = PRN211_1Context.ins.Students.ToList();
			dgvDisplay.ItemsSource = student;
			cbxName.ItemsSource = nameDe;
			cbxName.DisplayMemberPath = "Name";
			cbxName.SelectedValuePath = "Id";
			cbxSearch.ItemsSource = nameDe;
			cbxSearch.DisplayMemberPath = "Name";
			cbxSearch.SelectedValuePath = "Id";
		}

		private void dgvDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DataGrid dt = sender as DataGrid;
			if(dt.SelectedItems.Count > 0)
			{
				var selectRow = (Student) dt.SelectedItems[0];
				txtId.Text = selectRow.Id.ToString();
				txtId.IsEnabled = false;
				txtName.Text = selectRow.Name.ToString();
				txtGpa.Text = selectRow.Gpa.ToString();
				txtDob.Text = selectRow.Dob.ToString();
				if (selectRow.Gender) cbGender.IsChecked = true;
				else cbGender.IsChecked = false;
				cbxName.SelectedValue = selectRow.DepartId;
				btnAdd.IsEnabled = false;
			}
        }

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int id = Int32.Parse(txtId.Text);
				Student student = PRN211_1Context.ins.Students.FirstOrDefault(x => x.Id == id);
				if (student != null)
				{
					PRN211_1Context.ins.Students.Remove(student);
					PRN211_1Context.ins.SaveChanges();
					MessageBox.Show("Delete Student have Id " + id + " successful");
					Window_Loaded(sender, e);
					ClearForm();
				}
				else
				{
					MessageBox.Show("Don't find student have Id :" + txtId.Text);
				}
			}catch(Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
			}
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int id = Int32.Parse(txtId.Text);
				Student student = PRN211_1Context.ins.Students.FirstOrDefault(x => x.Id == id);
				if (student != null)
				{
					student.Name = txtName.Text;
					student.Gender = true;
					if (cbGender.IsChecked == false) student.Gender = false;
					student.DepartId = cbxName.SelectedValue.ToString();
					student.Dob = txtDob.DisplayDate.Date;
					student.Gpa = Double.Parse(txtGpa.Text);
					PRN211_1Context.ins.Students.Update(student);
					PRN211_1Context.ins.SaveChanges();
					Window_Loaded(sender, e);
					ClearForm();
					MessageBox.Show("Update student successful!");
				}
				else
				{
					MessageBox.Show("Don't find student have Id :" + txtId.Text);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
			}
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int id = Int32.Parse(txtId.Text);
				Student studentCheck = PRN211_1Context.ins.Students.FirstOrDefault(x => x.Id == id);
				if (studentCheck == null)
				{
					Student student = new Student();
					student.Id = id;
					student.Name = txtName.Text;
					student.Gender = true;
					if (cbGender.IsChecked == false) student.Gender = false;
					student.DepartId = cbxName.SelectedValue.ToString();
					student.Dob = txtDob.DisplayDate.Date;
					student.Gpa = Double.Parse(txtGpa.Text);
					PRN211_1Context.ins.Students.Add(student);
					PRN211_1Context.ins.SaveChanges();
					Window_Loaded(sender, e);
					ClearForm();
					MessageBox.Show("Add student successful!");
				}
				else
				{
					MessageBox.Show("Student have Id:" + txtId.Text + " is exsit!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
			}
		}

		private void cbxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string departId = cbxSearch.SelectedValue.ToString();
			var listStudent = PRN211_1Context.ins.Students.Where(x => x.DepartId == departId).ToList();
			dgvDisplay.ItemsSource = listStudent;
		}
		private void ClearForm()
		{
			txtName.Text = string.Empty;
			txtId.Text = string.Empty;
			txtGpa.Text = string.Empty;
			txtDob.Text = string.Empty;
			cbxName.Text = string.Empty;
			cbGender.IsChecked = false;
		}
	}
}
