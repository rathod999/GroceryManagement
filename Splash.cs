﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryManagement
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }

      
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
        int startPos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startPos += 1;
            myprogress.Value = startPos;
            percentageb1.Text = startPos + "%";
            if (myprogress.Value == 100)
            {
                myprogress.Value = 0;
                timer1.Stop();
                
                Login log = new Login();
                log.Show();
                this.Hide();
            }


        }

       
    }
}
