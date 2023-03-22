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
            Console.WriteLine("mySQL Test\n");

            testMySQL();

            Console.WriteLine("\nPress <enter> to quit...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        private static void DisplayData(System.Data.DataTable table)
        {
            foreach (System.Data.DataRow row in table.Rows)
            {
                //Console.WriteLine("<" + row.ToString() + ">");
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                    //Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("============================");
            }
            Console.WriteLine("\n");
        }

        private static int ExecuteQuery(MySqlConnection conn, string query)
        {
            MySqlConnector.MySqlCommand myCommand = new MySqlConnector.MySqlCommand(query);
            myCommand.Connection = conn;
            conn.Open();
            int n = myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();

            return n;
        }

        static void testMySQL()
        {
            MySqlConnection conn;

            string myConnectionString = "server=gsw.dnsalias.net;uid=brandtransparency;" +
                                        "pwd=db23/M3rm2EA;database=brandtransparency";

            try
            {
                conn = new MySqlConnection(myConnectionString);
                //conn.Open();    // <- is it necessary ??? it just take time...
                // ...
                Console.WriteLine("Connection to MySQL successful!\n");
                // ...

                //DataTable table = conn.GetSchema("MetaDataCollections");
                //DataTable table = conn.GetSchema();
                //DisplayData(table);

                // Query test...

                // Remove table
                ExecuteQuery(conn, "DROP TABLE test_table");

                Console.WriteLine("\nPress <enter> to continue...");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                // Create table
                ExecuteQuery(conn, "CREATE TABLE IF NOT EXISTS `brandtransparency`.`test_table` " +
                                   "(`index` INT NOT NULL,`name` VARCHAR(64) NULL, PRIMARY KEY (`index`)) ENGINE = InnoDB");

                // Insert rows
                ExecuteQuery(conn, "INSERT INTO test_table VALUES (1, 'Pelle' )");
                ExecuteQuery(conn, "INSERT INTO test_table VALUES (2, 'Bertil' )");

                // Rename row name
                ExecuteQuery(conn, "UPDATE test_table SET name = 'Hugo' WHERE name = 'Pelle'");


                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
                //MessageBox.Show(ex.Message);
            }            
        }        
    }

}
