using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace MultiFaceRec
{
    public partial class Student_Entry : Form
    {
        //Delecaring Veribles and object
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        public Student_Entry()
        {
            InitializeComponent();
        }

        //Will reset the full form data
        //------------------------------
      public void Reset()
        {
        txtPresentAbsent.Text = "";
        txtStudentName.Text = "";
        txtSubject.Text = "";
        txtTerm.Text = "";
        txtYear.Text = "";
        
        txtID.Text = "";
        txtStudentName.Focus();
        //btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        //btnDelete.Enabled = false;
        //Picture.Image = Properties.Resources.photo;
        
        }
     
  
        //delect The all records
        //----------------------
      
      
        //Close the form
        //--------------     
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Will generate the auto serial number for the student
        //---------------------------------------------------
      
          
        
        //Update the data 
        //---------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
             
                

                
                  
                  cc.con = new SqlConnection(cs.DBConn);
                  cc.con.Open();
                  string cb = "Update Attendance set Year=@d1,Term=@d2,Subject=@d3,Teacher=@d4,Name=@d5,Attendance=@d6";
                  cc.cmd = new SqlCommand(cb);
                  cc.cmd.Connection = cc.con;
                  cc.cmd.Parameters.AddWithValue("@d1", txtYear.Text);
                  cc.cmd.Parameters.AddWithValue("@d2", txtTerm.Text);
                  cc.cmd.Parameters.AddWithValue("@d3", txtSubject.Text);
                  cc.cmd.Parameters.AddWithValue("@d4", txtTeacher.Text);
                  cc.cmd.Parameters.AddWithValue("@d5", txtStudentName.Text);
                  cc.cmd.Parameters.AddWithValue("@d6", txtPresentAbsent.Text);
                  
                  
                 
                  cc.cmd.ExecuteReader();
                  cc.con.Close();
                  
                btnUpdate.Enabled = false;
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
           /*
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

    
        //New entry
        //----------
        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }


        //Will get the phoot from the computer
        //------------------------------------
        private void Browse_Click(object sender, EventArgs e)
        {
            
                var _with1 = openFileDialog1;

                _with1.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                _with1.FilterIndex = 4;
                //Reset the file name
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Picture.Image = Image.FromFile(openFileDialog1.FileName);
                }

            /*
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }


       
      

        private void BRemove_Click(object sender, EventArgs e)
        {
          //  Picture.Image = Properties.Resources.photo;
        }


        //show all data that is saved to the data base
        //---------------------------------------------
       

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStudentName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}