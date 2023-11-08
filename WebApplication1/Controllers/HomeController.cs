using Npgsql;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using System.Globalization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static string ConnectionString = "Server=localhost;Port=5432;User ID=postgres;Password=bd2109#;Database=db_lab1;";
        private static string message = "";
        string date = "";

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddRestaurant() {
            ViewData["Message"] = message;
            message = "";
            return View("AddRestaurant");
        }

       public IActionResult GenerateRestaurant()
        {
            string restaurantName = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT md5(random()::text);", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    restaurantName = reader.GetValue(0).ToString();
                }
            }

            string cuisineType = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT md5(random()::text);", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    cuisineType = reader.GetValue(0).ToString();
                }
            }

            string adress = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT md5(random()::text);", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    adress = reader.GetValue(0).ToString();
                }
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"restaurants\"(\"name\", \"adress\", \"cuisinetype\") VALUES('{restaurantName}', '{cuisineType}', '{adress}')", connection);
                command.ExecuteNonQuery();
            }

            ViewData["Message"] = "Successfully generated";
            return View("Generate");
        }

        public IActionResult GenerateTable()
        {
            string tablenumber = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT Floor(random() * 4 + 2020)::int", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    tablenumber = reader.GetValue(0).ToString();
                }
            }

            string seats = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT Floor(random() * 4 + 2020)::int", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    seats = reader.GetValue(0).ToString();
                }
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tables\"(\"tablenumber\", \"seats\", \"restaurantid\") VALUES({tablenumber}, {seats}, 1)", connection);
                command.ExecuteNonQuery();
            }

            ViewData["Message"] = "Successfully generated";
            return View("Generate");
        }

        public IActionResult OnAddRestaurant(Restaurant restaurant)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"restaurants\"(\"name\", \"adress\", \"cuisinetype\") VALUES('{restaurant.name}', '{restaurant.adress}', '{restaurant.cuisineType}')", connection);
                command.ExecuteNonQuery();
            }
            message = "Successfuly added";
            return RedirectToAction("AddRestaurant");
        }
        public IActionResult EditRestaurant() {
            ViewData["Message"] = message;
            message = "";
            return View("EditRestaurant");
        }
        public IActionResult OnEditRestaurant(int id, Restaurant restaurant)
        {
            // Оновлення існуючого ресторану в базі даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE \"restaurants\" SET \"name\" = '{restaurant.name}', \"adress\" = '{restaurant.adress}', \"cuisinetype\" = '{restaurant.cuisineType}' where \"id\" = {id}", connection);
                command.ExecuteNonQuery();
            }
            message = "Successfully edited";
            return RedirectToAction("EditRestaurant");
        }


        public IActionResult AddTable()
        {
            ViewData["Message"] = message;
            message = "";
            return View("AddTable");
        }
        public IActionResult OnAddTable(Table table) {
            // Додавання нового столика в базу даних
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tables\"(\"tablenumber\", \"seats\", \"restaurantid\") VALUES({table.tableNumber}, {table.seats}, {table.restaurantId})", connection);
                command.ExecuteNonQuery();
            }
            message = "Successfully added";
            return RedirectToAction("AddTable");
        }

        public IActionResult EditTable()
        {
            ViewData["Message"] = message;
            message = "";
            return View("EditTable");
        }
        public IActionResult OnEditTable(int id, Table table) {
            // Оновлення існуючого столика в базі даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE \"restaurants\" SET \"name\" = {table.tableNumber}, \"adress\" = {table.seats}, \"cuisinetype\" = '{table.restaurantId}' where \"id\" = {id}", connection);
                command.ExecuteNonQuery();
            }
            message = "Successfully edited";
            return RedirectToAction("EditTable");
        }
        public IActionResult AddTableReservation()
        {
            ViewData["Message"] = message;
            message = "";
            return View("AddTableReservation");
        }
        public IActionResult OnAddTableReservation(TableReservation reservation) {
            // Додавання нового бронювання столика в базу даних
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tablereservations\"(\"tableid\", \"userid\", \"reservationdatetime\") VALUES({reservation.tableId}, {reservation.userId}, '{reservation.reservationDateTime}')", connection);
                command.ExecuteNonQuery();
            }
            message = "Successfully added";
            return RedirectToAction("AddTableReservation");
        }

        public IActionResult EditTableReservation()
        {
            ViewData["Message"] = message;
            message = "";
            return View("EditTableReservation");
        }
        public IActionResult OnEditTableReservation(int id, TableReservation reservation) {
            // Оновлення існуючого бронювання столика в базі даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE \"tablereservations\" SET \"userid\" = {reservation.userId}, \"tableid\" = {reservation.tableId}, \"reservationdatetime\" = '{reservation.reservationDateTime}' where \"id\" = {id}", connection);
                command.ExecuteNonQuery();
            }
            message = "Successfully edited";
            return RedirectToAction("EditTableReservation");
        }

        public IActionResult DeleteTableReservation(int id)
        {
            ViewData["Message"] = message;
            message = "";
            return View("DeleteTableReservation");
        }
        public IActionResult OnDeleteTableReservation(int id) {
            // Видалення бронювання столика з бази даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM \"tablereservations\" WHERE \"id\" = {id}", connection);
                command.ExecuteNonQuery ();
            }
            message = "Successfully deleted";
            return RedirectToAction("DeleteTableReservation");
        }
    }
}
