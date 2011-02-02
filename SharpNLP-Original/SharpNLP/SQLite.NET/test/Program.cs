using System;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Data.SQLite;

namespace test
{
  class Program
  {
    static void Main(string[] args)
    {
      DbProviderFactory fact;
      fact = DbProviderFactories.GetFactory("System.Data.SQLite");

      System.IO.File.Delete("test.db3");
      using (DbConnection cnn = fact.CreateConnection())
      {
        cnn.ConnectionString = "Data Source=test.db3";
        cnn.Open();

        //cnn.Update += new SQLiteUpdateEventHandler(cnn_Updated);
        //cnn.Commit += new SQLiteCommitHandler(cnn_Commit);
        //cnn.RollBack += new EventHandler(cnn_RollBack);

        TestCases.Run(fact, cnn);
      }

      Console.ReadKey();
    }

    static void cnn_RollBack(object sender, EventArgs e)
    {
    }

    static void cnn_Commit(object sender, CommitEventArgs e)
    {
    }

    static void cnn_Updated(object sender, UpdateEventArgs e)
    {
    }
  }
}
