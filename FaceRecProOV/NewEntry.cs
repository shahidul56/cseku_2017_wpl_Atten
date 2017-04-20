using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace MultiFaceRec
{
    public partial class NewEntry : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        IntryData data = new IntryData();
        string st1;
        string st2;
        public NewEntry()
        {
            InitializeComponent();

        }

        private void add1_Click(object sender, EventArgs e)
        {
            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            string cb = "insert into fillYear(Year) VALUES (@d1)";
            cc.cmd = new SqlCommand(cb);
            cc.cmd.Connection = cc.con;

            cc.cmd.Parameters.AddWithValue("@d1", txtNewYear.Text);
           

            cc.cmd.ExecuteReader();
            cc.con.Close();
            data.FillYear();


            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void add2_Click(object sender, EventArgs e)
        {
            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            string cb = "insert into fillTerm(NewTerm) VALUES (@d1)";
            cc.cmd = new SqlCommand(cb);
            cc.cmd.Connection = cc.con;

            cc.cmd.Parameters.AddWithValue("@d1", txtNewTerm.Text);
                
            cc.cmd.ExecuteReader();
            cc.con.Close();
            data.FillTerm();


            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void add3_Click(object sender, EventArgs e)
        {
            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            string cb = "insert into fillSubject(NewSubject) VALUES (@d1)";
            cc.cmd = new SqlCommand(cb);
            cc.cmd.Connection = cc.con;

            cc.cmd.Parameters.AddWithValue("@d1", txtNewSub.Text);
            

            cc.cmd.ExecuteReader();
            cc.con.Close();
            data.FillSubject();


            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void add4_Click(object sender, EventArgs e)
        {
            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            string cb = "insert into fillTeacherName(NewTeacherName) VALUES (@d1)";
            cc.cmd = new SqlCommand(cb);
            cc.cmd.Connection = cc.con;

            cc.cmd.Parameters.AddWithValue("@d1", txtNewTeacher.Text);
            
                   
            cc.cmd.ExecuteReader();
            cc.con.Close();
            data.FillTeacherName();


            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
           

        }
    }
}
