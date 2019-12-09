﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace SuperGrate
{
    class UserTable
    {
        public class UserRow : Dictionary<ColumnType, string> { 
            public UserRow() { }
            public UserRow(UserRow TemplateRow)
            {
                foreach(KeyValuePair<ColumnType, string> column in TemplateRow)
                {
                    Add(column.Key, null);
                }
            }
        }
        public class UserRows : List<UserRow> { }
        public static UserRow CurrentHeaderRow = null;
        public static UserRow HeaderRowComputerSource = new UserRow()
        {
            { ColumnType.Tag, null },
            { ColumnType.NTAccount, "User Name" },
            { ColumnType.LastLogon, "Last Logon" },
            { ColumnType.Size, "Size" }
        };
        public static UserRow HeaderRowStoreSource = new UserRow()
        {
            { ColumnType.Tag, null },
            { ColumnType.NTAccount, "User Name" },
            { ColumnType.SourceComputer, "Source Computer" },
            { ColumnType.DestinationComputer, "Destination Computer" },
            { ColumnType.ImportedBy, "Imported By" },
            { ColumnType.ImportedOn, "Imported On" },
            { ColumnType.ExportedBy, "Exported By" },
            { ColumnType.ExportedOn, "Exported On" },
            { ColumnType.Size, "Size" }
        };
        static public void SetColumns(ListView Owner, UserRow Row)
        {
            Owner.Columns.Clear();
            foreach(KeyValuePair<ColumnType, string> Column in Row)
            {
                if (Column.Value != null)
                {
                    Owner.Columns.Add(Column.Value);
                }
            }
            CurrentHeaderRow = Row;
        }
        public enum ColumnType {
            NTAccount,
            SourceComputer,
            DestinationComputer,
            LastLogon,
            Size,
            ImportedBy,
            ImportedOn,
            ExportedBy,
            ExportedOn,
            Tag
        }
    }
}