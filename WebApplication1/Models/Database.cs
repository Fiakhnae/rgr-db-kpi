using Npgsql;
using System.Security.Cryptography;

namespace WebApplication1.Models {
    public static class Database {
        private static string ConnectionString = "Server=localhost;Port=5432;User ID=postgres;Password=bd2109#;Database=db_lab1;";
        public static void GenerateRestaurant() {
            string restaurantName = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT md5(random()::text);", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    restaurantName = reader.GetValue(0).ToString();
                }
            }

            string cuisineType = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT md5(random()::text);", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    cuisineType = reader.GetValue(0).ToString();
                }
            }

            string adress = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT md5(random()::text);", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    adress = reader.GetValue(0).ToString();
                }
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"restaurants\"(\"name\", \"adress\", \"cuisinetype\") VALUES('{restaurantName}', '{cuisineType}', '{adress}')", connection);
                command.ExecuteNonQuery();
            }
        }
        public static void GenerateTable() {
            string tablenumber = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT Floor(random() * 4 + 2020)::int", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    tablenumber = reader.GetValue(0).ToString();
                }
            }

            string seats = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT Floor(random() * 4 + 2020)::int", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    seats = reader.GetValue(0).ToString();
                }
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tables\"(\"tablenumber\", \"seats\", \"restaurantid\") VALUES({tablenumber}, {seats}, 1)", connection);
                command.ExecuteNonQuery();
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
