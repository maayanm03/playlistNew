﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GuessTheSongServer.DM;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PlayListNew.DB
{
    public class DataBaseHandler
    {
        static private DBConnection DBConnection;
        string path = @"PlayListNewLog.txt";

        public DataBaseHandler(string Server, string DatabaseName, string Password, string User)
        {
            DBConnection = DBConnection.Instance();
            DBConnection.DatabaseName = DatabaseName;
            DBConnection.Password = Password;
            DBConnection.Server = Server;
            DBConnection.User = User;

            DBConnection.Start();
        }

        public void EndDBConnection()
        {
            DBConnection.Close();
        }


        public int checkIfUserExist(string email)
        {
            // String query = "SELECT EXISTS(SELECT * from playlistGame.users WHERE email= 'wwe')";
            String query = "SELECT * from playlistGame.users WHERE email= @email";
            
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);
            command.Parameters.AddWithValue("@email", email);
            var reader = command.ExecuteReader();
            if (reader.HasRows){
                reader.Close();

                return 1;
            } else {
                reader.Close();


                return 0;
            }
        }

        // TODO - change
        public int checkIfUserExistAndPasswordRight(string email)
        {
            // String query = "SELECT EXISTS(SELECT * from playlistGame.users WHERE email= 'wwe')";
            String query = "SELECT * from playlistGame.users WHERE email= @email";

            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);
            command.Parameters.AddWithValue("@email", email);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();

                return 1;
            }
            else
            {
                reader.Close();


                return 0;
            }
        }

        public void SaveUserData(string email, string password, string fullName)
        {
            string query = "INSERT INTO playlistGame.users (Email, Password, Full_name)" +
                " Values(@email, @password, @fullName)";

            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);

            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@fullName", fullName);


            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at RunQuery function" + ex.Message + Environment.NewLine);
            }
        }



        

        /*
        public void SaveUserScore(int score)
        {
            string query;
            int id = 1;
            try
            {
                if (DBConnection.IsConnect())
                {
                    //check the user id:
                    query = "SELECT users.id FROM team12.users WHERE users.LastModified in " +
                            "(SELECT max(users.LastModified) FROM users)";
                    var cmd = new MySqlCommand(query, DBConnection.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = Int32.Parse(reader.GetString(0));
                    }
                    reader.Close();

                    //update user's score:
                    query = "UPDATE team12.users SET users.Score = @score " +
                            "WHERE users.id = @id";
                    MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);
                    command.Parameters.AddWithValue("@score", score);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at SaveUserScore function" + ex.Message + Environment.NewLine);
            }
        }

    */


        /*
      public ObservableCollection<Score> GetTopScores()
      {
          ObservableCollection<Score> scores = new ObservableCollection<Score>();
          try
          {
              if (DBConnection.IsConnect())
              {
                  string query = "SELECT firstname, lastname, score FROM team12.users" +
                                  " ORDER BY users.score DESC LIMIT 10";
                  var cmd = new MySqlCommand(query, DBConnection.Connection);
                  var reader = cmd.ExecuteReader();
                  while (reader.Read())
                  {
                      string firstName = reader.GetString(0);
                      string lastName = reader.GetString(1);
                      string userName = firstName + " " + lastName;
                      scores.Add(new Score() { name = userName, score = Int32.Parse(reader.GetString(2)) });
                  }
                  reader.Close();
              }
          }
          catch (Exception ex)
          {
              File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
          }
          return scores;
      }
      */


        /*
    public List<Genre> GetGenres()
    {
        List<Genre> res = new List<Genre>();
        try
        {
            if (DBConnection.IsConnect())
            {
                //id = 1 is 'Not Available'
                string query = "SELECT * FROM genres WHERE id != 1";
                var cmd = new MySqlCommand(query, DBConnection.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    res.Add(new Genre() { Id = Int32.Parse(reader.GetString(0)), Desc = reader.GetString(1) });
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(path, "Server DB Error at GetGenres function" + ex.Message + Environment.NewLine);
        }
        return res;
    }

    public List<Artist> GetArtists()
    {
        List<Artist> res = new List<Artist>();
        try
        {
            if (DBConnection.IsConnect())
            {
                string query = "SELECT t.id, t.artist FROM artists t";
                var cmd = new MySqlCommand(query, DBConnection.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    res.Add(new Artist() { Id = Int32.Parse(reader.GetString(0)), Desc = reader.GetString(1) });
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(path, "Server DB Error at GetArtists function" + ex.Message + Environment.NewLine);
        }
        return res;
    }

    public List<Song> GetSongs(string query)
    {
        List<Song> res = new List<Song>();
        try
        {
            if (DBConnection.IsConnect())
            {
                var cmd = new MySqlCommand(query, DBConnection.Connection);
                var reader = cmd.ExecuteReader();
                // turn the resulted rows to Song objects
                while (reader.Read())
                {
                    res.Add(new Song() { SongName = reader.GetString(0), ArtistID = Int32.Parse(reader.GetString(1)), Lyrics = reader.GetString(2) });
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(path, "Server DB Error at GetSongs function" + ex.Message + Environment.NewLine);
        }
        return res;
    }

    public string GetArtistById(int artistId)
    {
        string res = "";
        try
        {
            if (DBConnection.IsConnect())
            {
                string query = "SELECT t.artist FROM artists t WHERE t.id = " + artistId;
                var cmd = new MySqlCommand(query, DBConnection.Connection);
                var reader = cmd.ExecuteReader();
                reader.Read();
                res = reader.GetString(0);
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(path, "Server DB Error at GetArtistById function" + ex.Message + Environment.NewLine);
        }
        return res;
    }
    */
    }
}
