using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace VBLSnippetManager
{
    public partial class Form1 : Form
    {
        string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        public Form1()
        {
            InitializeComponent();
            LoadSnippetsFromXmlFile();
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(txtEditor.Text);
        }

        private void LoadSnippetsFromXmlFile()
        {
            listBoxSnippets.Items.Clear();
            XmlDocument xml = new XmlDocument();
            var contents = "";
            using (StreamReader streamReader = new StreamReader(_filePath+ "\\snippets.xml", Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            xml.LoadXml(contents);

            XmlNodeList xnList = xml.SelectNodes("/snippets/snippet");
            foreach (XmlNode xn in xnList)
            {
                string title = xn["title"].InnerText;
                //string lastName = xn["description"].InnerText;
                //txtEditor.Text += firstName + lastName;
                listBoxSnippets.Items.Add(title);
            }

        }

        private void listBoxSnippets_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show(listBoxSnippets.SelectedItem.ToString());
            string title = "";
            string description = "";
            string content = "";

            XmlDocument xml = new XmlDocument();
            var contents = "";
            using (StreamReader streamReader = new StreamReader(_filePath + "\\snippets.xml", Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            xml.LoadXml(contents);

            XmlNodeList xnList = xml.SelectNodes("/snippets/snippet");
            foreach (XmlNode xn in xnList)
            {
                if (listBoxSnippets.SelectedItem.ToString() == xn["title"].InnerText)
                {
                    title = xn["title"].InnerText;
                    description = xn["description"].InnerText;
                    content = xn["content"].InnerText;

                }
                txtName.Text = title;
                txtDescription.Text = description;
                txtEditor.Text = content.Replace("*","&");
                
            }
        }

        private void btnAddSnippet_Click(object sender, EventArgs e)
        {
            XmlDocument doc3 = new XmlDocument();
            var contents = "";
            using (StreamReader streamReader = new StreamReader(_filePath + "\\snippets.xml", Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            doc3.LoadXml(contents);
            XElement contacts =
            new XElement("snippet",
                new XElement("title", "Sample Title"),
                new XElement("description", "Sample Description"),
                new XElement("content","Sample Code Content"));

            XDocument y = XDocument.Parse(doc3.OuterXml);

            y.Element("snippets").Add(contacts);
            y.Save(_filePath + "\\snippets.xml");
            LoadSnippetsFromXmlFile();
        }
    }
}
