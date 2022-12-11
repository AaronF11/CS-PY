using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_PY
{
    public partial class FrmMain : Form
    {
        //------------------------------------------------------------------------------
        // Attributes
        //------------------------------------------------------------------------------
        private TextBox TextBox;
        private Button Button;
        
        //------------------------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------------------------
        public FrmMain()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------------
        // Loading ProgressBar
        //------------------------------------------------------------------------------
        private void ProgressBar()
        {
            for (int i = 0; i <= 100; i++)
            {
                PbStatus.Value = i;
                System.Threading.Thread.Sleep(10);
            }
            if (PbStatus.Value == 100)
            {
                PbStatus.Value = 0;
            }
        }

        //------------------------------------------------------------------------------
        // Declare a ScriptEngine object
        //------------------------------------------------------------------------------
        private dynamic Script()
        {
            // Create an instance of the ScriptRuntime
            ScriptRuntime Py = Python.CreateRuntime();
            // Create a new scope
            string Path = Application.StartupPath + "\\script.py";
            // Execute the script
            dynamic Script = Py.UseFile(Path);
            return Script;
        }

        //------------------------------------------------------------------------------------
        // 1. Write & Read text by textbox to send a py function and return the same string
        //------------------------------------------------------------------------------------
        private void writeAndReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call the ProgressBar
            ProgressBar();

            // Creating a new components
            TextBox = new TextBox //-- Textbox --
            {
                Height = 15,
                Width = 90,
                Location = new Point(this.Width / 3, this.Height / 2 - 20),
            };
            Button = new Button
            {
                Height = 32,
                Width = 64,
                Location = new Point(this.Width / 3, this.Height / 2 + 20),
                BackColor = Color.White,
                Text = "Send"
            };

            // Add the components to the form
            Controls.Add(TextBox);
            Controls.Add(Button);

            // Events
            Button.Click += delegate
            {
                // Call the function
                string name = Script().Func_Write(TextBox.Text, true).ToString();

                if (name != "")
                {
                    // Display the result
                    MessageBox.Show(name);
                }
            };
        }

        private void BtnMain_ButtonClick(object sender, EventArgs e)
        {
            Controls.Remove(TextBox);
            Controls.Remove(Button);
        }
    }
}
