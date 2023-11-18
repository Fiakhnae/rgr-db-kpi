using Npgsql;
using System.Security.Cryptography;

namespace WebApplication1.Models {
    public static class Database {
        private static string ConnectionString = "Server=localhost;Port=5432;User ID=postgres;Password=bd2109#;Database=db_lab1;";
        public static void GenerateRestaurant() {
            for (int i = 0; i < 10; i++)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"restaurants\"(\"name\", \"adress\", \"cuisinetype\")\r\nValues((SELECT (chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int)\r\n     )),\r\n    (SELECT (chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) \r\n     )),\r\n    (SELECT (chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int)\r\n     )))", connection);
                    command.ExecuteNonQuery();
                }
               
            }

          
        }
        public static void GenerateTable() {
            for(int i = 0;i < 10; i++)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tables\"(\"restaurantid\", \"tablenumber\", \"seats\")\r\nValues((Select max(\"tables\".\"restaurantid\") from \"tables\"),\r\n    (SELECT random() * 20 + 1 AS RAND_1_11),\r\n    (SELECT random() * 5 + 1 AS RAND_1_11))", connection);
                    command.ExecuteNonQuery();
                }
            }

        }
        public static void AddRestaurant(Restaurant restaurant) {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"restaurants\"(\"name\", \"adress\", \"cuisinetype\") VALUES('{restaurant.name}', '{restaurant.adress}', '{restaurant.cuisineType}')", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void EditRestaurant(int id, Restaurant restaurant) {
            // Оновлення існуючого ресторану в базі даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE \"restaurants\" SET \"name\" = '{restaurant.name}', \"adress\" = '{restaurant.adress}', \"cuisinetype\" = '{restaurant.cuisineType}' where \"id\" = {id}", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void AddTable(Table table) {
            // Додавання нового столика в базу даних
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tables\"(\"tablenumber\", \"seats\", \"restaurantid\") VALUES({table.tableNumber}, {table.seats}, {table.restaurantId})", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void EditTable(int id, Table table) {
            // Оновлення існуючого столика в базі даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE \"tables\" SET \"tablenumber\" = {table.tableNumber}, \"seats\" = {table.seats}, \"restaurantid\" = '{table.restaurantId}' where \"id\" = {id}", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void AddTableReservation(TableReservation reservation) {
            // Додавання нового бронювання столика в базу даних
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tablereservations\"(\"tableid\", \"userid\", \"reservationdatetime\") VALUES({reservation.tableId}, {reservation.userId}, '{reservation.reservationDateTime}')", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void EditTableReservation(int id, TableReservation reservation) {
            // Оновлення існуючого бронювання столика в базі даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE \"tablereservations\" SET \"userid\" = {reservation.userId}, \"tableid\" = {reservation.tableId}, \"reservationdatetime\" = '{reservation.reservationDateTime}' where \"id\" = {id}", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteTableReservation(int id) {
            // Видалення бронювання столика з бази даних за ID
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM \"tablereservations\" WHERE \"id\" = {id}", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
