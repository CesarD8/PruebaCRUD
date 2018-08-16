using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using Test1.Models;

namespace Test1.Controllers
{
    public class CRUDController : Controller
    {
        string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=test1;Data Source=LAPTOP-T0EA7MPF\SQLEXPRESS";
        SqlConnection connection;
        SqlCommand cmd;


        public CRUDController()
        {
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;

        }
        // GET: CRUD
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Food food)
        {
            cmd.CommandText = "Insert into food values(@name, @price, @style)";
            cmd.Parameters.AddWithValue("@name", food.Name);
            cmd.Parameters.AddWithValue("@price", food.Price);
            cmd.Parameters.AddWithValue("@style", food.Style);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return View(@"..\Home\Index");
        }

        public ActionResult Update()
        {
            return View();
        }

        public ActionResult UpdateInDB(Food food)
        {
            cmd.CommandText = "Update food set Name=@name, Price=@price, Style=@style where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", food.ID);
            cmd.Parameters.AddWithValue("@name", food.Name);
            cmd.Parameters.AddWithValue("@price", food.Price);
            cmd.Parameters.AddWithValue("@style", food.Style);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return View(@"..\Home\Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult ConfirmDelete(Food element)
        {
            cmd.CommandText = "Delete from Food where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", element.ID);

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return View(@"..\Home\Index");
        }

        public ActionResult ReadForm()
        {
            return View();
        }

        public ActionResult Read(Food food)
        {
            ResultsModel model = new ResultsModel();

            if (food.Name == null)
            {
                cmd.CommandText = "Select * from food";
            }
            else
            {
                cmd.CommandText = "Select * from food where Name = @Name";
                cmd.Parameters.AddWithValue("@Name", food.Name);
            }

            model.results = new List<Food>();
            
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Food test = new Food();
                test.ID = (int)dataTable.Rows[i].ItemArray[0];
                test.Name = (string)dataTable.Rows[i].ItemArray[1];
                test.Price = (decimal)dataTable.Rows[i].ItemArray[2];
                test.Style = (string)dataTable.Rows[i].ItemArray[3];
                model.results.Add(test);
            }

            return View(model);
        }
    }
}