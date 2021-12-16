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
        List<string> _itemList;

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
            _itemList = listBoxSnippets.Items.Cast<string>().ToList();
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

            //call new form and passback values
            Form_New entryForm = new Form_New();
            //clear form content
            entryForm.ShowDialog();

            LoadSnippetsFromXmlFile();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            //var itemList = listBoxSnippets.Items.Cast<string>().ToList();
            if (_itemList.Count > 0)
            {
                //clear the items from the list
                listBoxSnippets.Items.Clear();

                //filter the items and add them to the list
                listBoxSnippets.Items.AddRange(
                    _itemList.Where(i => i.Contains(txtFilter.Text)).ToArray());
            }
        }
    }
}
