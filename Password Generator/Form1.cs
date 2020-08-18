using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Password_Generator
{
    public partial class Form1 : Form
    {
        string path = @"settings.xml";
        public Form1()
        {
            InitializeComponent();
            button1.TabIndex = 0;

            FileInfo fileInf = new FileInfo(path);
            if (!fileInf.Exists)
            {
                XDocument xdoc = new XDocument(new XElement("data",
                new XElement("input",
                    new XAttribute("value", Length.Text)),
                new XElement("checkbox1",
                    new XAttribute("value", checkBox1.Checked)),
                new XElement("checkbox2",
                    new XAttribute("value", checkBox2.Checked)),
                new XElement("checkbox3",
                    new XAttribute("value", checkBox3.Checked)),
                new XElement("checkbox4",
                    new XAttribute("value", checkBox4.Checked))));
                xdoc.Save(path);
            }
            else
            {
                XDocument xdoc = XDocument.Load(path);
                Length.Text = xdoc.Element("data").Element("input").Attribute("value").Value;
                checkBox1.Checked = Boolean.Parse(xdoc.Element("data").Element("checkbox1").Attribute("value").Value);
                checkBox2.Checked = Boolean.Parse(xdoc.Element("data").Element("checkbox2").Attribute("value").Value);
                checkBox3.Checked = Boolean.Parse(xdoc.Element("data").Element("checkbox3").Attribute("value").Value);
                checkBox4.Checked = Boolean.Parse(xdoc.Element("data").Element("checkbox4").Attribute("value").Value);
            }
        }

        char[] littleLetter = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char[] bigLetter = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        char[] symbols = { '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '{', '}', ':', ';', '\"', '\'', ',', '.', '/', '<', '>', '?' };
        int prevA = -1;

        Random rnd = new Random();
        Random rnd1 = new Random();
        Random rnd2 = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            r1.Text = "";
            r2.Text = "";
            r3.Text = "";
            r4.Text = "";
            r5.Text = "";
            r6.Text = "";
            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked && Int32.Parse(Length.Text) > 0)
            {
                this.r1.Width = Int32.Parse(Length.Text) * 10;
                this.r2.Width = Int32.Parse(Length.Text) * 10;
                this.r3.Width = Int32.Parse(Length.Text) * 10;
                this.r4.Width = Int32.Parse(Length.Text) * 10;
                this.r5.Width = Int32.Parse(Length.Text) * 10;
                this.r6.Width = Int32.Parse(Length.Text) * 10;

                randText(r1);
                randText(r2);
                randText(r3);
                randText(r4);
                randText(r5);
                randText(r6);
            }
            else
            {
                MessageBox.Show("Вы указали неверное значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void randText(TextBox textBox)
        {
            for (int i = 0; i < Int32.Parse(Length.Text); i++)
            {
                int a = rnd.Next(1, 5);
                if (prevA == a)
                {
                    --i;
                    continue;
                }

                prevA = a;

                switch (a)
                {
                    case 1:
                        if (!checkBox1.Checked)
                        {
                            i--;
                            break;
                        }
                        textBox.Text += littleLetter[rnd1.Next(0, 26)];
                        break;
                    case 2:
                        if (!checkBox2.Checked)
                        {
                            i--;
                            break;
                        }
                        textBox.Text += bigLetter[rnd1.Next(0, 26)];
                        break;
                    case 3:
                        if (!checkBox3.Checked)
                        {
                            i--;
                            break;
                        }
                        textBox.Text += numbers[rnd1.Next(0, 10)];
                        break;
                    case 4:
                        if (!checkBox4.Checked)
                        {
                            i--;
                            break;
                        }
                        textBox.Text += symbols[rnd1.Next(0, 27)];
                        break;
                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            button1_Click(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XDocument xdoc = new XDocument(new XElement("data",
                new XElement("input",
                    new XAttribute("value", Length.Text)),
                new XElement("checkbox1",
                    new XAttribute("value", checkBox1.Checked)),
                new XElement("checkbox2",
                    new XAttribute("value", checkBox2.Checked)),
                new XElement("checkbox3",
                    new XAttribute("value", checkBox3.Checked)),
                new XElement("checkbox4",
                    new XAttribute("value", checkBox4.Checked))));
            xdoc.Save(path);
        }
    }
}
