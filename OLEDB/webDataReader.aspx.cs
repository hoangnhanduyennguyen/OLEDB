using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace OLEDB
{
    public partial class webDataReader : System.Web.UI.Page
    {
        //Connection, static so that the connection can't be modified 
        static OleDbConnection myCon;
        //Query
        OleDbCommand myCmd; 
        //Data Reader
        OleDbDataReader rdStudents;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                // @ is to ignore the backslash \
                //Server.MapPath method maps the specified relative or virtual path to the corresponding physical directory on the server
                // Using this method to prevent bringing the project to another machine because the machine name is different e.g: User/Cass -> User/ABC
                //Convert the blackslash to forward slash \ to /
                myCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + Server.MapPath("~/App_Data/College.mdb"));
                //Connection is to find the database
                //we need to open the database
                myCon.Open();

                //Sql Command
                // [] shows that this is a column, we can use it or not, with or without, both work
                myCmd = new OleDbCommand("SELECT [Number], RefCourse FROM Courses Order by [Number]", myCon);
                //Execute the command
                rdStudents = myCmd.ExecuteReader();
                //Display on the list box
                lstCourse.DataTextField = "Number";
                //refcourse connect course and student
                lstCourse.DataValueField = "RefCourse";
                lstCourse.DataSource = rdStudents;
                lstCourse.DataBind();

                //This is just to test the connection with hard code
                //                                   start
                string sql = "SELECT * FROM Courses WHERE Teacher =@teach and Duration <@dur";
                OleDbCommand myCmdTest = new OleDbCommand(sql, myCon);
                OleDbParameter myPar = new OleDbParameter("teach", "Houria Houmel");
                //get and set data from db without knowing datatype from db, do this to make it a string
                myPar.DbType = System.Data.DbType.String;
                myCmdTest.Parameters.Add(myPar);
                //add without creating parameter 
                myCmdTest.Parameters.AddWithValue("dur", 50);
                OleDbDataReader rdTest = myCmdTest.ExecuteReader();
                gridTest.DataSource = rdTest;
                gridTest.DataBind();

                //test gridview
                /*myCmd = new OleDbCommand("SELECT * FROM Courses WHERE RefCourse = 2", myCon);
                rdStudents = myCmd.ExecuteReader();
                gridTest.DataSource = rdStudents;
                gridTest.DataBind();
                rdStudents.Close();
                myCmd.Cancel();*/
            }
        }

        protected void lstCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCmd = new OleDbCommand("Select * From Courses WHERE RefCourse = @ref", myCon);
            myCmd.Parameters.AddWithValue("ref",lstCourse.SelectedItem.Value);
            OleDbDataReader rdCourse = myCmd.ExecuteReader();
            
            //if it found, condition is true
            if (rdCourse.Read())
            {
                txtNumber.Text = rdCourse["Number"].ToString();
                txtTitle.Text = rdCourse["Title"].ToString();
                txtDuration.Text = rdCourse["Duration"].ToString();
                txtTeacher.Text = rdCourse["Teacher"].ToString();
                txtDescription.Text = rdCourse["Description"].ToString();
            }
            rdCourse.Close();
            //if we create a new command, we can't have the course and student at the same time
            myCmd.CommandText = "SELECT StudentName as [Names], BirthDate as [Birth Dates], Telephone, Average, Email from Students where ReferCourse = @ref";
            rdCourse = myCmd.ExecuteReader();
            gridResult.DataSource = rdCourse;
            gridResult.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int refC = Convert.ToInt32(lstCourse.SelectedItem.Value);
            string sql = "Update Courses SET Duration=@dur, Teacher=@tea, Description=@des WHERE RefCourse=@courseId";
            OleDbCommand myCmd = new OleDbCommand(sql, myCon);
            myCmd.Parameters.AddWithValue("dur", Convert.ToInt32(txtDuration.Text));
            myCmd.Parameters.AddWithValue("tea", txtTeacher.Text);
            myCmd.Parameters.AddWithValue("des", txtDescription.Text);
            myCmd.Parameters.AddWithValue("courseId", refC);

            //when we don't need to return anything, insert, delete and update, use execute non query
            int result = myCmd.ExecuteNonQuery();
            //Javascipt
            Response.Write("<script>alert(\"Updated\");</script>");
        }
    }
}