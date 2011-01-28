/********************************************************
 * ADO.NET 2.0 Data Provider for SQLite Version 3.X
 * Written by Robert Simpson (robert@blackcastlesoft.com)
 * 
 * Released to the public domain, use at your own risk!
 ********************************************************/

namespace System.Data.SQLite
{
  using System;
  using System.Data;
  using System.Data.Common;

  /// <summary>
  /// SQLite implementation of DbTransaction.
  /// </summary>
  public sealed class SQLiteTransaction : DbTransaction
  {
    /// <summary>
    /// The connection to which this transaction is bound
    /// </summary>
    internal SQLiteConnection _cnn;

    /// <summary>
    /// Constructs the transaction object, binding it to the supplied connection
    /// </summary>
    /// <param name="connection">The connection to open a transaction on</param>
    /// <param name="deferredLock">TRUE to defer the writelock, or FALSE to lock immediately</param>
    internal SQLiteTransaction(SQLiteConnection connection, bool deferredLock)
    {
      _cnn = connection;
      if (_cnn._transactionLevel++ == 0)
      {
        try
        {
          using (SQLiteCommand cmd = _cnn.CreateCommand())
          {
            if (!deferredLock)
              cmd.CommandText = "BEGIN IMMEDIATE";
            else
              cmd.CommandText = "BEGIN";

            cmd.ExecuteNonQuery();
          }
        }
        catch (SQLiteException)
        {
          _cnn._transactionLevel--;
          _cnn = null;
          throw;
        }
      }
    }

    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    public override void Commit()
    {
      IsValid();

      if (--_cnn._transactionLevel == 0)
      {
        try
        {
          using (SQLiteCommand cmd = _cnn.CreateCommand())
          {
            cmd.CommandText = "COMMIT";
            cmd.ExecuteNonQuery();
          }
        }
        finally
        {
          _cnn = null;
        }
      }
      else
      {
        _cnn = null;
      }
    }

    /// <summary>
    /// Returns the underlying connection to which this transaction applies.
    /// </summary>
    public new SQLiteConnection Connection
    {
      get { return _cnn; }
    }

    /// <summary>
    /// Forwards to the local Connection property
    /// </summary>
    protected override DbConnection DbConnection
    {
      get { return Connection; }
    }

    /// <summary>
    /// Disposes the transaction.  If it is currently active, any changes are rolled back.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (_cnn != null) 
        Rollback();

      base.Dispose(disposing);
    }

    /// <summary>
    /// Gets the isolation level of the transaction.  SQLite only supports Serializable transactions.
    /// </summary>
    public override IsolationLevel IsolationLevel
    {
      get { return IsolationLevel.Serializable; }
    }

    /// <summary>
    /// Rolls back the active transaction.
    /// </summary>
    public override void Rollback()
    {
      IsValid();

      try
      {
        using (SQLiteCommand cmd = _cnn.CreateCommand())
        {
          cmd.CommandText = "ROLLBACK";
          cmd.ExecuteNonQuery();
        }
        _cnn._transactionLevel = 0;
      }
      finally
      {
        _cnn = null;
      }
    }

    internal void IsValid()
    {
      if (_cnn == null)
        throw new ArgumentNullException("No connection associated with this transaction");

      if (_cnn._transactionLevel == 0)
      {
        _cnn = null;
        throw new SQLiteException((int)SQLiteErrorCode.Misuse, "No transactions active on this connection");
      }
    }
  }
}
