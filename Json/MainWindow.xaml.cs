using System;
using System.IO;
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
using System.Xml.Serialization;
using System.Text.Json;
using Json.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
namespace Json
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

		List<Person> persons = new List<Person>();

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			Person person = new Person();
			person.Id = int.Parse(txtId.Text);
			person.Name = txtName.Text;
			person.Car = cbxCar.IsChecked.Value;
			persons.Add(person);
			dgvDisplay.ItemsSource = persons;
			dgvDisplay.Items.Refresh();
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			int id = int.Parse(txtId.Text);
			Person person = persons.FirstOrDefault(x => x.Id == id);
			if (person != null)
			{
				person.Name = txtName.Text;
				person.Car = cbxCar.IsChecked.Value;
				MessageBox.Show("Add Successful");
				dgvDisplay.ItemsSource = persons;
				dgvDisplay.Items.Refresh();
			}
			else return;
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			int id = int.Parse(txtId.Text);
			Person person = persons.FirstOrDefault(x => x.Id == id);
			if (person != null)
			{
				persons.Remove(person);
				MessageBox.Show("Remove Successful");
				dgvDisplay.ItemsSource = persons;
				dgvDisplay.Items.Refresh();
			}
			else return;
		}

		private void btnLoad_Click(object sender, RoutedEventArgs e)
		{
			//var openFileDialog = new OpenFileDialog() { Filter = "XML File(*.xml) | *.xml | All File(*.*)|*.*" };
			//var rs = openFileDialog.ShowDialog();
			//if (rs == false) return;
			//var xml = new XmlSerializer(typeof(List<Person>));
			//using Stream s2 = File.OpenRead(openFileDialog.FileName);
			//persons = (List<Person>)xml.Deserialize(s2);
			//dgvDisplay.ItemsSource = persons;
			//s2.Close();

			var abc = new OpenFileDialog() { Filter = "Json File(*.json) | *.json | All File(*.*)|*.*" };
			var rs = abc.ShowDialog();
			if (rs == false) return;
			string filepath = abc.FileName;
			using (Stream stream = File.OpenRead(filepath))
			{
				try
				{
					persons = JsonConvert.DeserializeObject<List<Person>>(new StreamReader(stream).ReadToEnd());
					dgvDisplay.ItemsSource = persons;
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Lỗi khi Deserialize JSON: {ex.Message}");
				}

				stream.Close();
			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			//var abc = new SaveFileDialog() { Filter = "XML File(*.xml) | *.xml | All File(*.*)|*.*" };
   //         var rs = abc.ShowDialog();
   //         if (rs == false) return;
   //         var a = new XmlSerializer(typeof(List<Person>));
   //         using Stream s1 = File.Create(abc.FileName);
   //         a.Serialize(s1, persons);
   //         s1.Close();

			var abc = new SaveFileDialog() { Filter = "Json File(*.json) | *.json | All File(*.*)|*.*" };
			var rs = abc.ShowDialog();
			if (rs == false) return;
			string filepath = abc.FileName;
			using (Stream stream = File.Create(filepath))
			{
                System.Text.Json.JsonSerializer.SerializeAsync(stream, persons).Wait();
				stream.Close();
			}
		}
	}
}