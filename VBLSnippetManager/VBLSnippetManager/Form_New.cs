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
    public partial class Form_New : Form
    {
        string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        public Form_New()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtDescription.Text == "" || txtSnippet.Text == "")
            {
                MessageBox.Show("each new snippet must contain a Name, Description, and Snippet Content");
            }
            else
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
                    new XElement("title", txtName.Text),
                    new XElement("description",txtDescription.Text),
                    new XElement("content", txtSnippet.Text));

                XDocument y = XDocument.Parse(doc3.OuterXml);

                y.Element("snippets").Add(contacts);
                y.Save(_filePath + "\\snippets.xml");

                ReplaceReservedCharacters();
                ClearControls();
                this.Close();
            }
        }

        private void ClearControls()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtSnippet.Text = "";
        }

        private void ReplaceReservedCharacters()
        {
            txtName.Text.Replace("&", "*");
            txtDescription.Text.Replace("&", "*");
            txtSnippet.Text.Replace("&", "*");
        }
    }
}
