using System.Data;
using System.Data.SqlClient;
using HotelMamagement.Models;

namespace HotelMamagement.DAL
{
    public class RoomDAL
    {
        private readonly string _connectionString;
        public RoomDAL()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllRooms", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rooms.Add(new Room
                    {
                        RoomId = Convert.ToInt32(reader["RoomId"]),
                        RoomNumber = reader["RoomNumber"].ToString(),
                        RoomType = reader["RoomType"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return rooms;
        }

        public void InsertRooms(Room room)
        {
            //List <Room> rooms = new List<Room> ();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertRoom", conn);
                cmd.CommandType= CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                cmd.Parameters.AddWithValue("@Price", room.RoomNumber);
                cmd.Parameters.AddWithValue("@Status", room.Status);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public Room GetRoomById(int id)
        {
            Room room = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetRoomById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoomId", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    room = new Room
                    {
                        RoomId = Convert.ToInt32(reader["RoomId"]),
                        RoomNumber = reader["RoomNumber"].ToString(),
                        RoomType = reader["RoomType"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Status = reader["Status"].ToString()
                    };
                }
            }
            return room;
        }

        public void UpdateRoom(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateRoom", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@RoomId", room.RoomId);
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                cmd.Parameters.AddWithValue("@Price", room.Price);
                cmd.Parameters.AddWithValue("@Status", room.Status);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Delete(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteRoom", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@RoomId", room.RoomId);

                conn.Open();
                cmd.ExecuteNonQuery();
                //conn.Close();

            }
        }
    }
}
