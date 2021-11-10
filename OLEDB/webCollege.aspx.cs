using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace OLEDB
{
    public partial class webCollege : System.Web.UI.Page
    {
        
        static OleDbConnection myCon;
        OleDbDataReader myReader;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                panCourse.Visible = panPrograms.Visible = false;                                //it will map the path to your database
                myCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + Server.MapPath("~/App_Data/College2.mdb"));
                myCon.Open();
                OleDbCommand myCmd = new OleDbCommand("SELECT refSchool, Title from Schools order by Title", myCon);
                myReader = myCmd.ExecuteReader();

                radlistSchool.DataTextField = "Title";
                radlistSchool.DataValueField = "refSchool";
                radlistSchool.DataSource = myReader;
                radlistSchool.DataBind();

                /*while (myReader.Read())
                {
                    radlistSchool.Items.Add(new ListItem(myReader["Title"].ToString(), myReader["refSchool"].ToString()));

                }*/
                myReader.Close();
            }
        }

        protected void radlistSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            panPrograms.Visible = true;
            OleDbCommand myCmd = new OleDbCommand("SELECT Title, refProgram from Programs WHERE referSchool=@refSchool ORDER BY Title", myCon);
            myCmd.Parameters.AddWithValue("refSchool", radlistSchool.SelectedItem.Value);
            myReader = myCmd.ExecuteReader();

            radlstPrograms.DataTextField = "Title";
            radlstPrograms.DataValueField = "refProgram";
            radlstPrograms.DataSource = myReader;
            radlstPrograms.DataBind();

            myReader.Close();
            chklstCourses.Items.Clear();
            panCourse.Visible = false;
            gridStudents.DataSource = null;
            gridStudents.DataBind();
        }

        protected void radlstPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            panCourse.Visible = true;
            OleDbCommand myCmd = new OleDbCommand("SELECT [Number], RefCourse from Courses WHERE referProgram=@refProgram ORDER BY [Number]", myCon);
            myCmd.Parameters.AddWithValue("refProgram", radlstPrograms.SelectedItem.Value);
            myReader = myCmd.ExecuteReader();
            chklstCourses.DataTextField = "Number";
            chklstCourses.DataValueField = "RefCourse";
            chklstCourses.DataSource = myReader;
            chklstCourses.DataBind();
            /*while (rdCourses.Read())
            {
                chklstCourses.Items.Add(new ListItem(rdCourses["Number"].ToString(), rdCourses["RefCourse"].ToString()));
            }*/
            myReader.Close();
            gridStudents.DataSource = null;
            gridStudents.DataBind();
        }

        protected void chklstCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if there is a course selected, displays the grid Student
            if (chklstCourses.SelectedIndex > -1)
            {
                string sql = "SELECT StudentName as [NAMES], BirthDate as [Birth Dates], Telephone, Average, Email FROM Students WHERE ReferCourse = " + chklstCourses.SelectedItem.Value;

                foreach(ListItem item in chklstCourses.Items)
                {
                    if (item.Selected)
                    {
                        sql += " OR ReferCourse = " + item.Value;
                    }
                }
                sql += " ORDER BY StudentName";
                OleDbCommand myCmd = new OleDbCommand(sql, myCon);
                myReader = myCmd.ExecuteReader();
                gridStudents.DataSource = myReader;
                gridStudents.DataBind();
            }
            else //if there is no course selected, grid Student disappears 
            {
                gridStudents.DataSource = null;
                gridStudents.DataBind();
            }

            //My solution
            /*//Clear the grid view everytime listCourse selected index changed (surtout when the user deselect all courses)
            gridStudents.DataSource = null;
            gridStudents.DataBind();
            
            //Count the selected course
            int selCount = 0;
            for (int i = 0; i < chklstCourses.Items.Count; i++) if (chklstCourses.Items[i].Selected) selCount++;
    
            //If the user has selected at least 1 course
            if (selCount!= 0)
            {
                string sql = "SELECT StudentName as [Names], BirthDate as [Birth Dates], Telephone, Average, Email from Students WHERE ReferCourse=@refCourse0";
                //start from 1 because we have already write the first RefCourse 
                for (int i = 1; i < selCount; i++)
                {
                    sql += " OR ReferCourse=@refCourse" + i.ToString();
                }
                //the result should be:
                //"SELECT StudentName as [Names], BirthDate as [Birth Dates],
                //Telephone, Average, Email from Students WHERE ReferCourse=@refCourse0"
                // OR ReferCourse=@refCourse1 etc.
                sql += " Order By StudentName";
                OleDbCommand myCmd = new OleDbCommand(sql, myCon);

                //loop through all the selected course and add with value
                int j = 0;
                foreach (ListItem item in chklstCourses.Items)
                {
                    if (item.Selected)
                    {
                        myCmd.Parameters.AddWithValue("refCourse" + j.ToString(), item.Value);
                        j++;
                    }
                }

                OleDbDataReader rdStudents = myCmd.ExecuteReader();
                gridStudents.DataSource = rdStudents;
                gridStudents.DataBind();
                rdStudents.Close();

            }*/
        }

        protected void gridStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}