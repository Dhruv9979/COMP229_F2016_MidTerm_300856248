using COMP229_F2016_MidTerm_300856248.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP229_F2016_MidTerm_300856248
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodos();
            }
        }

        protected void GetTodos()
        {
            // populate the form with existing data from db
            int TodoId = Convert.ToInt32(Request.QueryString["TodoID"]);
            // connect to the EF DB
            using (TodoContext db = new TodoContext())
            {
                // populate a todo object instance with the todoID from 
                // the URL parameter
                Todo updatedTodo = (from Todo in db.Todos
                                    where Todo.TodoID == TodoId
                                    select Todo).FirstOrDefault();
                // map the todo properties to the form control
                if (updatedTodo != null)
                {
                    TodoDescriptionTextBox.Text = updatedTodo.TodoDescription;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                    TodoIDTextBox.Text = updatedTodo.TodoID.ToString();
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to the Todo page
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to conect to the server
            using (TodoContext db = new TodoContext())
            {
                // use the Todo model to create a new Todo object and 
                // save a new record

                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // our URL has a TodoID in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current student from EF db
                    newTodo = (from Todo in db.Todos
                                  where Todo.TodoID == TodoID
                                  select Todo).FirstOrDefault();
                }

                // add form data to the new Todo record
                //newTodo.TodoID = Convert.ToInt32(TodoIDTextBox.Text);
                newTodo.TodoDescription = TodoDescriptionTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
                newTodo.Completed = CompletedCheckBox.Checked;

                // use LINQ to ADO.NET to add / insert new Todo into the db

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated Todos page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}