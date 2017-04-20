using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
namespace MultiFaceRec
{
    public partial class Student_Entry_Record : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        public Student_Entry_Record()
        {
            InitializeComponent();
        }
       
        
        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT RTRIM(Year) as [YEAR],RTRIM(Term) as [TERM],RTRIM(Subject) as [SUBJECT],RTRIM(Teacher) as [TEACHER],RTRIM(DateTime) as [DATE],RTRIM(Name) as [NAME],RTRIM(Roll) as [Roll],RTRIM(Attendance) as [ATTENDANCE],Photo from Attendance order by name", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Attendance");
                dgw.DataSource = cc.ds.Tables["Attendance"].DefaultView;
                cc.con.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        
        //search
        private void txtGuestName_TextChanged(object sender, EventArgs e) 
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT RTRIM(Year) as [YEAR],RTRIM(Term) as [TERM],RTRIM(Subject) as [SUBJECT],RTRIM(Teacher) as [TEACHER],RTRIM(Name) as [NAME],RTRIM(Roll) as [ROLL],RTRIM(Attendance) as [ATTENDANCE],RTRIM(DateTime) as [DATE], Photo from Attendance WHERE roll like '" + txtStudentName.Text + "%' order by roll", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Attendance");
                dgw.DataSource = cc.ds.Tables["Attendance"].DefaultView;
                cc.con.Close();
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        
        
        
        
        public void Reset()
        {
            txtStudentName.Text = "";
            GetData();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        
        
        private void dgw_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dgw.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
               dgw.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

       
        
        
        
        private void dgw_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                
                /*
                    DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                   
                    Student_Entry frm = new Student_Entry();
                    frm.Show();

                    frm.txtYear.Text = dr.Cells[0].Value.ToString();
                    frm.txtTerm.Text = dr.Cells[1].Value.ToString();
                    frm.txtSubject.Text = dr.Cells[2].Value.ToString();
                    frm.txtTeacher.Text = dr.Cells[3].Value.ToString();
                    frm.txtStudentName.Text = dr.Cells[4].Value.ToString();
                    frm.txtPresentAbsent.Text = dr.Cells[5].Value.ToString();
                  
                   
                   
                    frm.btnUpdate.Enabled = true;
                    
                
                */

               
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

      
        private void frmStudentRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }

        
        private void ExelBackUp_Click(object sender, EventArgs e)
        {
            SqlConnection cnn ;

            string connectionString = null;

            string sql = null;

            string data = null;

            int i = 0;

            int j = 0; 



            Excel.Application xlApp ;

            Excel.Workbook xlWorkBook ;

            Excel.Worksheet xlWorkSheet ;

            object misValue = System.Reflection.Missing.Value;



            xlApp = new Excel.Application();

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);



           // connectionString = "data source=servername;initial catalog=databasename;user id=username;password=password;";
            connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\EDUCATION\THEIRD YEAR\3rd year 2nd term\LAB\CSE-Web Programming Project-3200\FaceRecProOV (1)\FaceRecProOV\FaceRecProOV\FaceRecProOV\Attendance.mdf;Integrated Security=True";

            cnn = new SqlConnection(connectionString);

            cnn.Open();

            sql = "SELECT * FROM Attendance ";

            SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);

            DataSet ds = new DataSet();

            dscmd.Fill(ds);



            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)

            {

                for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)

                {

                    data = ds.Tables[0].Rows[i].ItemArray[j].ToString();

                    xlWorkSheet.Cells[i + 1, j + 1] = data;

                }

            }



            xlWorkBook.SaveAs("KHULNA-UNIVERSITY-CLASS-ATTENDANCE.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);

            xlApp.Quit();



            releaseObject(xlWorkSheet);

            releaseObject(xlWorkBook);

            releaseObject(xlApp);



            MessageBox.Show("Excel file created , you can find the file c:\\(document folder)KHULNA-UNIVERSITY-CLASS-ATTENDANCE");

        }



        private void releaseObject(object obj)

        {

            try

            {

                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

                obj = null;

            }

            catch (Exception ex)

            {

                obj = null;

                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());

            }

            finally

            {

                GC.Collect();

            }

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            cc.cmd = new SqlCommand("SELECT RTRIM(Year) as [YEAR],RTRIM(Term) as [TERM],RTRIM(Subject) as [SUBJECT],RTRIM(Teacher) as [TEACHER],RTRIM(Name) as [NAME],RTRIM(Roll) as [ROLL],RTRIM(Attendance) as [ATTENDANCE],RTRIM(DateTime) as [DATE], Photo from Attendance WHERE  DateTime  between @d1 and @d2 order by DateTime", cc.con);

            cc.cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "Date").Value = dtpDateFrom.Value.Date;
            cc.cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "Date").Value = dtpDateTo.Value;

            cc.da = new SqlDataAdapter(cc.cmd);
            cc.ds = new DataSet();
            cc.da.Fill(cc.ds, "Attendance");
            dgw.DataSource = cc.ds.Tables["Attendance"].DefaultView;
            cc.con.Close();

        }



    }
}
