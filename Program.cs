using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ConsoleApp1
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;

    internal class Program
    {
        static void Main(string[] args)
        {
            testMySQL();

            Console.WriteLine("\nPress <enter> to quit...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        static void testMySQL()
        {
            SqlManager sql = new SqlManager();

            if (! sql.Connect("server=gsw.dnsalias.net;uid=brandtransparency;" +
                              "pwd=db23/M3rm2EA;database=brandtransparency"))
            {
                Console.WriteLine("Failed to connect to the database!");
                return;
            }
            Console.WriteLine("Connect to the database was successful.\n");

            string tableName = "jg_table";           // <- change this to whatever you like...

            // Create table
            sql.ExecuteQuery("CREATE TABLE IF NOT EXISTS `brandtransparency`.`" + tableName + "` " +
                             "(`index` INT NOT NULL,`name` VARCHAR(64) NULL, PRIMARY KEY (`index`)) ENGINE = InnoDB");

            // Delete all rows in the table
            sql.ExecuteQuery("DELETE FROM " + tableName);

            // Insert rows
            sql.ExecuteQuery("INSERT INTO " + tableName + " VALUES (1, 'Pelle' )");
            sql.ExecuteQuery("INSERT INTO " + tableName + " VALUES (2, 'Bertil' )");
            sql.ExecuteQuery("INSERT INTO " + tableName + " VALUES (3, 'Sven' )");
            sql.ExecuteQuery("INSERT INTO " + tableName + " VALUES (4, 'Pelle' )");

            // Rename a rows name with Pelle to Hugo...
            sql.ExecuteQuery("UPDATE " + tableName + " SET name = 'Hugo' WHERE name = 'Pelle'");


            // Read and list table contents...
            Console.WriteLine("1: list all\n");

            try
            {
                MySqlCommand cmd = sql.GetSqlCommand("SELECT * FROM " + tableName);

                sql.Open();
    
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
                //MessageBox.Show(ex.Message);
            }


            Console.WriteLine("\n2: filtered list on the name 'Hugo'\n");

            try
            {
                MySqlCommand cmd = sql.GetSqlCommand("SELECT * FROM " + tableName + " WHERE name = 'Hugo'");

                //sql.Open();   // <- not necessary since we open above...

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
                //MessageBox.Show(ex.Message);
            }

            Console.WriteLine("\nTest done...");
        }

    }
}