/********************************************************
 * ADO.NET 2.0 Data Provider for SQLite Version 3.X
 * Written by Robert Simpson (robert@blackcastlesoft.com)
 * 
 * Released to the public domain, use at your own risk!
 ********************************************************/

#if !PLATFORM_COMPACTFRAMEWORK
namespace System.Data.SQLite
{
  using System;
  using System.Data;
  using System.Data.Common;
  using System.Transactions;

  internal class SQLiteEnlistment : IEnlistmentNotification
  {
    private SQLiteTransaction _transaction;

    internal SQLiteEnlistment(SQLiteConnection cnn)
    {
      _transaction = cnn.BeginTransaction();
    }

    #region IEnlistmentNotification Members

    public void Commit(Enlistment enlistment)
    {
      _transaction.Connection._enlistment = null;
      try
      {
        _transaction.IsValid();
        _transaction.Connection._transactionLevel = 1;
        _transaction.Commit();

        enlistment.Done();
      }
      finally
      {
        _transaction = null;
      }
    }

    public void InDoubt(Enlistment enlistment)
    {
      enlistment.Done();
    }

    public void Prepare(PreparingEnlistment preparingEnlistment)
    {
      try
      {
        _transaction.IsValid();
      }
      catch(Exception e)
      {
        preparingEnlistment.ForceRollback(e);
        return;
      }
      preparingEnlistment.Prepared();
    }

    public void Rollback(Enlistment enlistment)
    {
      _transaction.Connection._enlistment = null;
      try
      {
        _transaction.Rollback();
        enlistment.Done();
      }
      finally
      {
        _transaction = null;
      }
    }

    #endregion
  }
}
#endif // !PLATFORM_COMPACT_FRAMEWORK