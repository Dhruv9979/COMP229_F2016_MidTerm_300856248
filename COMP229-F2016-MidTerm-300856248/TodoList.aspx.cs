using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.ModelBinding;
using COMP229_F2016_MidTerm_300856248.Models;

namespace COMP229_F2016_MidTerm_300856248
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the Todo data
                this.GetTodos();
            }
        }

        private void GetTodos()
        {
            // connect to EF DB
            using (TodoContext db = new TodoContext())
            {
                // query the Todo Table using EF and LINQ
                var Todos = (from allTodos in db.Todos
                             select allTodos);

                // bind the result to the Todo GridView
                TodoGridView.DataSource = Todos.ToList();
                TodoGridView.DataBind();
            }
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was selected
            int selectedRow = e.RowIndex;

            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            using (TodoContext db = new TodoContext())
            {
                // create object ot the Todo class and store the query inside of it
                Todo deletedTodo = (from todoRecords in db.Todos
                                    where todoRecords.TodoID == TodoID
                                    select todoRecords).FirstOrDefault();

                // remove the selected Todo from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the db
                db.SaveChanges();

                // refresh the grid
                this.GetTodos();
            }
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort
            Session["SortColumn"] = e.SortExpression;
            //refresh the grid view
            this.GetTodos();
            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(IsPostBack)
            {
                if(e.Row.RowType == DataControlRowType.Header)// if header row has been clicked
                {
                    LinkButton linkButton = new LinkButton();

                    for(int index = 0;index < TodoGridView.Columns.Count -1;index++)
                    {
                        if(Session["SortDirection"].ToString() == "ASC")
                        {
                            linkButton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                        }
                        else
                        {
                            linkButton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                        }

                        e.Row.Cells[index].Controls.Add(linkButton);
                    }
                }
            }
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);
            // refresh the grid view
            this.GetTodos();
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;
            // refresh the grid view
            this.GetTodos();
        }
    }
}