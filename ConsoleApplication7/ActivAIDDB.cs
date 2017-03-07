using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ActivAIDDB
    {
        private SqlConnection conn;
        private string dblocation;
        // private int elementCounter;
        public ActivAIDDB()
        {
            dblocation = "Server=.\\SQLEXPRESS;Database=ActivAID DB;Integrated Security=true";
            // elementCounter = 0;
        }

        public void insertIntoFiles(string filepath, string keywords)
        {
            using (conn = new SqlConnection(dblocation))
            {
                string fileQuery = "INSERT INTO Files (filePath, keyWords) VALUES (@file, @key)";
                SqlCommand cmd = new SqlCommand(fileQuery, conn);
                cmd.Parameters.AddWithValue("@file", filepath);
                cmd.Parameters.AddWithValue("@key", keywords);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void insertIntoHyperlinks(string parentpath, string filepath)
        {
            int parentId = getFileId(parentpath);
            using (conn = new SqlConnection(dblocation))
            {
                string hyperQuery = "INSERT INTO Hyperlinks (fileId, filePath) VALUES (@id, @path)";
                SqlCommand cmd = new SqlCommand(hyperQuery, conn);
                cmd.Parameters.AddWithValue("@id", parentId);
                cmd.Parameters.AddWithValue("@path", filepath);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void insertIntoElements(string parentpath, int block, string data)
        {
            // incElementCounter();
            int parentId = getFileId(parentpath);
            using (conn = new SqlConnection(dblocation))
            {
                string elementQuery = "INSERT INTO Elements (fileId, blockNumber, data) VALUES (@id, @block, @dat)";
                SqlCommand cmd = new SqlCommand(elementQuery, conn);
                cmd.Parameters.AddWithValue("@id", parentId);
                cmd.Parameters.AddWithValue("@block", block);
                cmd.Parameters.AddWithValue("@dat", data);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void insertIntoImages(int elid, string elpath)
        {
            using (conn = new SqlConnection(dblocation))
            {
                string imageQuery = "INSERT INTO Images (elementId, elementImg) VALUES (@id, @path)";
                SqlCommand cmd = new SqlCommand(imageQuery, conn);
                cmd.Parameters.AddWithValue("@id", elid);
                cmd.Parameters.AddWithValue("@path", elpath);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Utility Methods
        private int getFileId(string filepath)
        {
            int fileid;
            using (conn = new SqlConnection(dblocation))
            {
                string getid = "SELECT fildid FROM Files WHERE filePath=@path";
                SqlCommand cmd = new SqlCommand(getid, conn);
                cmd.Parameters.AddWithValue("@path", filepath);
                conn.Open();
                using (SqlDataReader fReader = cmd.ExecuteReader())
                {
                    fReader.Read();
                    fileid = fReader.GetInt32(0);
                }
                conn.Close();
            }
            return fileid;
        }
        /*
        private int getElementId()
        {
            returns the element counter once
            a boolean flag indicating it is an image element is 
            detected. Boolean flag would be passed from the Parser.
        }

        private void incElementCounter()
        {
            increments the element counter according to how
            each element id is incremented in the database table.
            Called everytime an element is added into the database.
        }

                
        */
    }
}
