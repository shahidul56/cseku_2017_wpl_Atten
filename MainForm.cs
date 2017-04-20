
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;
using System.Data.Sql;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;





namespace MultiFaceRec
{

    //======================================================================================================================
    //             <<<<< WEB PROGRAMMING PROJECT-2017 ON Education Platform >>> Fcae Recognation Based Attendance System
    //======================================================================================================================
    public partial class FrmPrincipal : Form
    {
        //All the variables, vector, and the haarcascade 
        //----------------------------------------------

        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
       // string name, names = null;
        public static string name ="";
        public static string names = "";
        string atten = "PRESENT";
        public static string Stringpart="";
        public static string Numberpart="";

        string Studentname;
        string Roll;


        //starting the 
        //------------
        public FrmPrincipal()
        {
            InitializeComponent();
            btnPrsent.Enabled = false;

            face = new HaarCascade("haarcascade_frontalface_default.xml");
            //eye = new HaarCascade("haarcascade_eye.xml");
            try
            {
                //Loading all the previous trained face data
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;

                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                MessageBox.Show("Nothing in binary database, please add at least a face(Simply train the prototype with the Add Face Button).", "Triained faces load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }


        //start the cemera process
        //------------------------
        public void button1_Click(object sender, EventArgs e)
        {

            btnPrsent.Enabled = true;
            //Straing the cemera and will capture frame by frame 
            grabber = new Capture();
            grabber.QueryFrame();

            Application.Idle += new EventHandler(FrameGrabber);
           
            button1.Enabled = false; //cemra start button
      
           
            
              
                  
            
        }




       




        //Matching captured image with the TrainedFace
        //--------------------------------------------
       public void FrameGrabber(object sender, EventArgs e)
        {
            //label3.Text = "0";
            //label5.Text = "0";
            //label8.Text = "0";

            NamePersons.Add("");


            // capture a frame form  device both face and all things on the image
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);


            gray = currentFrame.Convert<Gray, Byte>();

            //(TestImageBox.Image = currentFrame);

            //Result of haarCascade will be on the "MCvAvgComp"-facedetected (is it face or not )
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
          face,
          1.2,
          10,
          Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
          new Size(20, 20));


            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2); //Frame detect colour is 'read'


                if (trainingImages.ToArray().Length != 0)
                {
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);


                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                       trainingImages.ToArray(),
                       labels.ToArray(),
                       3000,
                       ref termCrit);

                    Studentname = recognizer.Recognize(result); // detected name of the face is been saved  to the 'name'-variable

                    //the colour of  the face label name 
                    currentFrame.Draw(Studentname, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));

                }

                NamePersons[t - 1] = Studentname;
                NamePersons.Add("");



               // label3.Text = facesDetected[0].Length.ToString();



            }
            t = 0;


            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
            }

            imageBoxFrameGrabber.Image = currentFrame;
        
           // label6.Text = names;

          
            string cell = names;
            //int a = getIndexofNumber(cell);

            cell = cell.Trim();
            if (cell.Length == 1)
            {
                if (cell.Contains(","))
                    cell = "";
            }

            string Stringpart = string.IsNullOrEmpty(cell)?cell:cell.Substring(0,cell.IndexOf("-"));

            string Numberpart = string.IsNullOrEmpty(cell)?cell:cell.Substring(cell.IndexOf("-") + 1, cell.IndexOf(",") - cell.IndexOf("-") - 1);

            label5.Text = Stringpart;
            label8.Text = Numberpart;
            
          
            
           
            label1.Text = IntryData.year1;
            label2.Text = IntryData.term1;
            label3.Text = IntryData.sub1;
         
            
           
            names = "";

            NamePersons.Clear();

        }

       public int getIndexofNumber(string cell)
       {
           int a = -1, indexofNum = 10000;
           a = cell.IndexOf("0"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("1"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("2"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("3"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("4"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("5"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("6"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("7"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("8"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }
           a = cell.IndexOf("9"); if (a > -1) { if (indexofNum > a) { indexofNum = a; } }

           if (indexofNum != 10000)
           { return indexofNum; }
           else
           { return 0; }

           
       }
      
    
       


        //==========================================================
        //data base saving process ids done here
        //==========================================================

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;

        public void Reset()
        {
           
        }


        //auto  generate id
        //------------------
        public void auto()
        {

           
        }

        public void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }


     


        //Showing Alll of the  Attendance Data
        //-------------------------------------

        private void btnGetData_Click(object sender, EventArgs e)
        {
           

            Student_Entry_Record frm = new Student_Entry_Record();
            frm.Reset();
          
            frm.Show();
        }





        //Present Button and Present Will taken by it
        //-------------------------------------------

        public void btnPrsent_Click(object sender, EventArgs e)
        {
           

            //Main place to save data to the database

            cc.con = new SqlConnection(cs.DBConn);
            cc.con.Open();
            string cb = "insert into Attendance(Year,Term,Subject,Teacher,Name,Roll,Attendance,DateTime) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";
            cc.cmd = new SqlCommand(cb);
            cc.cmd.Connection = cc.con;
          
            cc.cmd.Parameters.AddWithValue("@d1", IntryData.year1);
            cc.cmd.Parameters.AddWithValue("@d2", IntryData.term1);
            cc.cmd.Parameters.AddWithValue("@d3", IntryData.sub1);
            cc.cmd.Parameters.AddWithValue("@d4", IntryData.teacher1);

           /* cc.cmd.Parameters.AddWithValue("@d5", Stringpart);
            cc.cmd.Parameters.AddWithValue("@d6", Numberpart);*/
            cc.cmd.Parameters.AddWithValue("@d5", label5.Text);
            cc.cmd.Parameters.AddWithValue("@d6", label8.Text);

            cc.cmd.Parameters.AddWithValue("@d7", atten);
            cc.cmd.Parameters.AddWithValue("@d8", IntryData.date1);
           
            
           
            
            
       
            cc.cmd.ExecuteReader();
            cc.con.Close();
            
            
            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

        
        }



        //Timer Method
        //------------
        public void timer1_Tick(object sender, EventArgs e)
        {

            
                //this.Hide();
            

        }



        //Excel data backUp
        //----------------
        private void ExelBackUp_Click(object sender, EventArgs e)
        {
            SqlConnection cnn;

            string connectionString = null;

            string sql = null;

            string data = null;

            int i = 0;

            int j = 0;



            Excel.Application xlApp;

            Excel.Workbook xlWorkBook;

            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;



            xlApp = new Excel.Application();

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);



            
            connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\EDUCATION\THEIRD YEAR\3rd year 2nd term\LAB\CSE-Web Programming Project-3200\FaceRecProOV (1)\FaceRecProOV\FaceRecProOV\FaceRecProOV\Attendance.mdf;Integrated Security=True";

            cnn = new SqlConnection(connectionString);

            cnn.Open();

            sql = "SELECT * FROM Student ";

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



            MessageBox.Show("Excel file created , you can find the file c:\\(document folder)>>KHULNA-UNIVERSITY-CLASS-ATTENDANCE");

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

         private void button2_Click(object sender, EventArgs e)
         {
             Training trainshow = new Training();
             trainshow.Show();

         }

         private void panel2_Paint(object sender, PaintEventArgs e)
         {

         }



    }

       






    }



    