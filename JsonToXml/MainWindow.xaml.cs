using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;


namespace JsonToXml
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
        string fileName;
        private void btn_load_json_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName; 
                    fileName = filePath;
                    if (string.Equals(System.IO.Path.GetExtension(filePath), ".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        btn_convert_xml.IsEnabled = false;
                        btn_convert_Json.IsEnabled = true;
                    }
                    else
                    {

                        btn_convert_xml.IsEnabled = true;
                        btn_convert_Json.IsEnabled = false;
                    }
                    string fileContent = File.ReadAllText(filePath);
                    txtData.Text = fileContent;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btn_convert_xml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = txtData.Text;
                JToken jToken = JToken.Parse(json);
                using (XmlWriter writer = XmlWriter.Create("output.xml"))
                {
                    writer.WriteStartDocument();
                    WriteJTokenToXml(jToken, writer);
                    writer.WriteEndDocument();
                }
                FormatXmlFile("output.xml");
                MessageBox.Show("Convert to xml succeccful!(" + System.IO.Path.GetFullPath("output.xml"));
                if (File.Exists("output.xml"))
                {
                    string fileContent = File.ReadAllText("output.xml");
                    fileName = "output.xml";
                    txtData.Text = fileContent;
                }
                btn_convert_xml.IsEnabled = false;
                btn_convert_Json.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void FormatXmlFile(string filePath)
        {          
            XDocument xmlDocument = XDocument.Load(filePath);         
            xmlDocument.Save(filePath);
        }

        static void WriteJTokenToXml(JToken jToken, XmlWriter writer)
        {
            switch (jToken.Type)
            {
                case JTokenType.Object:
                    //writer.WriteStartElement("Object");
                    foreach (JProperty property in jToken.Children<JProperty>())
                    {
                        if (property.Name.Contains("?")) continue;
                        writer.WriteStartElement(property.Name);
                        WriteJTokenToXml(property.Value, writer);
                        writer.WriteEndElement();
                    }
                    //writer.WriteEndElement();
                    break;
                case JTokenType.Array:
                    //writer.WriteStartElement("Array");
                    foreach (JToken item in jToken.Children())
                    {
                        WriteJTokenToXml(item, writer);
                    }
                    //writer.WriteEndElement();
                    break;
                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.String:
                case JTokenType.Boolean:
                case JTokenType.Null:
                    //writer.WriteStartElement(jToken.Type.ToString());
                    writer.WriteValue(jToken.ToString());
                    //writer.WriteEndElement();
                    break;
                default:
                    throw new ArgumentException($"Unsupported token type: {jToken.Type}");
            }
        }

        private void btn_convert_Json_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument xmlDocument = XDocument.Load(fileName);
                JObject jsonObject = JObject.FromObject(xmlDocument);
                File.WriteAllText("outputJson.json", jsonObject.ToString());
                MessageBox.Show("Convert to json successful! \n" + System.IO.Path.GetFullPath("outputJson.json"));
                if (File.Exists("outputJson.json"))
                {
                    string fileContent = File.ReadAllText("outputJson.json");
                    fileName = "outputJson.json";
                    txtData.Text = fileContent;
                }
                btn_convert_xml.IsEnabled = true;
                btn_convert_Json.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}
