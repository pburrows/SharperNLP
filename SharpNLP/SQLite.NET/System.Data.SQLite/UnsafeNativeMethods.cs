/********************************************************
 * ADO.NET 2.0 Data Provider for SQLite Version 3.X
 * Written by Robert Simpson (robert@blackcastlesoft.com)
 * 
 * Released to the public domain, use at your own risk!
 ********************************************************/

namespace System.Data.SQLite
{
  using System;
  using System.Security;
  using System.Runtime.InteropServices;

#if !PLATFORM_COMPACTFRAMEWORK
  [SuppressUnmanagedCodeSecurity]
#endif
  internal sealed class UnsafeNativeMethods
  {
#if !USE_INTEROP_DLL
    private const string SQLITE_DLL = "System.Data.SQLite.DLL";
#else
    private const string SQLITE_DLL = "SQLite.Interop.DLL";
#endif

    private UnsafeNativeMethods()
    {
    }

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_sleep_interop(uint dwMilliseconds);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_libversion_interop(out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_free_interop(IntPtr p);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_open_interop(byte[] utf8Filename, out IntPtr db);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_interrupt_interop(IntPtr db);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_close_interop(IntPtr db);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_exec_interop(IntPtr db, byte[] strSql, IntPtr pvCallback, IntPtr pvParam, out IntPtr errMsg, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_errmsg_interop(IntPtr db, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_changes_interop(IntPtr db);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_busy_timeout_interop(IntPtr db, int ms);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_prepare_interop(IntPtr db, IntPtr pSql, int nBytes, out IntPtr stmt, out IntPtr ptrRemain, out int nRemain);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_blob_interop(IntPtr stmt, int index, Byte[] value, int nSize, IntPtr nTransient);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_double_interop(IntPtr stmt, int index, ref double value);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_int_interop(IntPtr stmt, int index, int value);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_int64_interop(IntPtr stmt, int index, ref long value);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_null_interop(IntPtr stmt, int index);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_text_interop(IntPtr stmt, int index, byte[] value, int nlen, IntPtr pvReserved);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_parameter_count_interop(IntPtr stmt);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_bind_parameter_name_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_bind_parameter_index_interop(IntPtr stmt, byte[] strName);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_column_count_interop(IntPtr stmt);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_name_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_decltype_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_step_interop(IntPtr stmt);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_column_double_interop(IntPtr stmt, int index, out double value);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_column_int_interop(IntPtr stmt, int index);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_column_int64_interop(IntPtr stmt, int index, out long value);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_text_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_blob_interop(IntPtr stmt, int index);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_column_bytes_interop(IntPtr stmt, int index);

    [DllImport(SQLITE_DLL)]
    internal static extern TypeAffinity sqlite3_column_type_interop(IntPtr stmt, int index);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_finalize_interop(IntPtr stmt);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_reset_interop(IntPtr stmt);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_create_collation_interop(IntPtr db, byte[] strName, int nType, int nArgs, SQLiteCollation func, out IntPtr nCookie);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_create_function_interop(IntPtr db, byte[] strName, int nArgs, int nType, SQLiteCallback func, SQLiteCallback fstep, SQLiteCallback ffinal, out IntPtr nCookie);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_function_free_callbackcookie(IntPtr nCookie);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_aggregate_count_interop(IntPtr context);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_value_blob_interop(IntPtr p);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_value_bytes_interop(IntPtr p);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_value_double_interop(IntPtr p, out double value);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_value_int_interop(IntPtr p);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_value_int64_interop(IntPtr p, out Int64 value);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_value_text_interop(IntPtr p, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern TypeAffinity sqlite3_value_type_interop(IntPtr p);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_result_blob_interop(IntPtr context, byte[] value, int nSize, IntPtr pvReserved);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_result_double_interop(IntPtr context, ref double value);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_result_error_interop(IntPtr context, byte[] strErr, int nLen);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_result_int_interop(IntPtr context, int value);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_result_int64_interop(IntPtr context, ref Int64 value);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_result_null_interop(IntPtr context);

    [DllImport(SQLITE_DLL)]
    internal static extern void sqlite3_result_text_interop(IntPtr context, byte[] value, int nLen, IntPtr pvReserved);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_aggregate_context_interop(IntPtr context, int nBytes);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_table_column_metadata_interop(IntPtr db, byte[] dbName, byte[] tblName, byte[] colName, out IntPtr ptrDataType, out IntPtr ptrCollSeq, out int notNull, out int primaryKey, out int autoInc, out int dtLen, out int csLen);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_database_name_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_database_name16_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_table_name_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_table_name16_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_origin_name_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_origin_name16_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_text16_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
    internal static extern int sqlite3_open16_interop(string utf16Filename, out IntPtr db);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_errmsg16_interop(IntPtr db, out int len);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
    internal static extern int sqlite3_prepare16_interop(IntPtr db, IntPtr pSql, int sqlLen, out IntPtr stmt, out IntPtr ptrRemain, out int len);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
    internal static extern int sqlite3_bind_text16_interop(IntPtr stmt, int index, string value, int nlen, int nTransient);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_name16_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_column_decltype16_interop(IntPtr stmt, int index, out int len);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
    internal static extern int sqlite3_create_collation16_interop(IntPtr db, string strName, int nType, int nArgs, SQLiteCollation func, out IntPtr nCookie);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
    internal static extern int sqlite3_create_function16_interop(IntPtr db, string strName, int nArgs, int nType, SQLiteCallback func, SQLiteCallback funcstep, SQLiteCallback funcfinal, out IntPtr nCookie);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_value_text16_interop(IntPtr p, out int len);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
    internal static extern void sqlite3_result_error16_interop(IntPtr context, string strName, int nLen);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
    internal static extern void sqlite3_result_text16_interop(IntPtr context, string strName, int nLen, IntPtr pvReserved);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int sqlite3_encryptfile(string fileName);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int sqlite3_decryptfile(string fileName);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int sqlite3_encryptedstatus(string fileName, out int fileStatus);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int sqlite3_compressfile(string fileName);

    [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int sqlite3_decompressfile(string fileName);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_key_interop(IntPtr db, byte[] key, int keylen);

    [DllImport(SQLITE_DLL)]
    internal static extern int sqlite3_rekey_interop(IntPtr db, byte[] key, int keylen);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_update_hook_interop(IntPtr db, SQLiteUpdateCallback func);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_commit_hook_interop(IntPtr db, SQLiteCommitCallback func);

    [DllImport(SQLITE_DLL)]
    internal static extern IntPtr sqlite3_rollback_hook_interop(IntPtr db, SQLiteRollbackCallback func);
  }
}
