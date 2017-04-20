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

    public partial class IntryData : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        public static string year1;
        public static string term1;
        public static string sub1;
        public static string teacher1;
        public static DateTime date1;
        public IntryData()
        {
            InitializeComponent();
            FillYear();
            FillTerm();
            FillSubject();
            FillTeacherName();

        }

        private void IntryData_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtYear1.Text == "")
            {
                MessageBox.Show("Please enter Year", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYear1.Focus();
                return;
            }
            if (txtTerm1.Text == "")
            {
                MessageBox.Show("Please enter Term", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTerm1.Focus();
                return;
            }
            if (txtSubject1.Text == "")
            {
                MessageBox.Show("Please enter Sub name.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject1.Focus();
                return;
            }


            //Main place to save data to the database
            ConnectionString cs = new ConnectionString();
            CommonClasses cc = new CommonClasses();
            clsFunc cf = new clsFunc();
           // string st1;
           //string st2;

            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            string cb = "insert into Entry(Year,Term,Subject,Teacher,DateTime) VALUES (@d1,@d2,@d3,@d4,@d5)";
            cc.cmd = new SqlCommand(cb);
            cc.cmd.Connection = cc.con;
            cc.cmd.Parameters.AddWithValue("@d1", txtYear1.Text);
            cc.cmd.Parameters.AddWithValue("@d2", txtTerm1.Text);
            cc.cmd.Parameters.AddWithValue("@d3", txtSubject1.Text);
            cc.cmd.Parameters.AddWithValue("@d4", txtTeacherName1.Text);
            cc.cmd.Parameters.AddWithValue("@d5", dateTimePicker1.Value);
            

            year1 = txtYear1.Text;
            term1 = txtTerm1.Text;
            sub1 = txtSubject1.Text;
            teacher1 = txtTeacherName1.Text;
            date1 = dateTimePicker1.Value;
           
            /*MemoryStream ms = new MemoryStream();
            Bitmap bmpImage = new Bitmap(Picture.Image);
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] data = ms.GetBuffer();
            SqlParameter p = new SqlParameter("@d8", SqlDbType.Image);
            */
           /* p.Value = data;
            cc.cmd.Parameters.Add(p);*/
            
            cc.cmd.ExecuteReader();
            cc.con.Close();
            
            /*st1 = lblUser.Text;
            st2 = "added the new Student'" + name + "' having Student id '" + txtStudentID.Text + "'";
            cf.LogFunc(st1, System.DateTime.Now, st2);
            btnSave.Enabled = false;*/
            
            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

            /*  catch (Exception ex)
              {
                  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }*/

            Training train = new Training();
            train.Show();
            this.Hide();
        }

        private void newEntry_Click(object sender, EventArgs e)
        {
            NewEntry open = new NewEntry();
            open.Show();
            this.Hide();
        }



     public void FillYear()
        {

            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            string ct = "select RTRIM(Year) from fillYear order by Year";
            cc.cmd = new SqlCommand(ct);
            cc.cmd.Connection = cc.con;
            cc.rdr = cc.cmd.ExecuteReader();
            while (cc.rdr.Read())
            {
                txtYear1.Items.Add(cc.rdr[0]);
            }
            cc.con.Close();


        }

     public void FillTerm()
     {

         cc.con = new SqlConnection(cs.DBConn);
         cc.con.Open();
         string ct = "select RTRIM(NewTerm) from fillTerm order by NewTerm";
         cc.cmd = new SqlCommand(ct);
         cc.cmd.Connection = cc.con;
         cc.rdr = cc.cmd.ExecuteReader();
         while (cc.rdr.Read())
         {
             txtTerm1.Items.Add(cc.rdr[0]);
         }
         cc.con.Close();


     }

     public void FillSubject()
     {

         cc.con = new SqlConnection(cs.DBConn);
         cc.con.Open();
         string ct = "select RTRIM(NewSubject) from fillSubject order by NewSubject";
         cc.cmd = new SqlCommand(ct);
         cc.cmd.Connection = cc.con;
         cc.rdr = cc.cmd.ExecuteReader();
         while (cc.rdr.Read())
         {
             txtSubject1.Items.Add(cc.rdr[0]);
         }
         cc.con.Close();


     }

     public void FillTeacherName()
     {

         cc.con = new SqlConnection(cs.DBConn);
         cc.con.Open();
         string ct = "select RTRIM(NewTeacherName) from fillTeacherName order by NewTeacherName";
         cc.cmd = new SqlCommand(ct);
         cc.cmd.Connection = cc.con;
         cc.rdr = cc.cmd.ExecuteReader();
         while (cc.rdr.Read())
         {
             txtTeacherName1.Items.Add(cc.rdr[0]);
         }
         cc.con.Close();


     }

    
     
    }
}
