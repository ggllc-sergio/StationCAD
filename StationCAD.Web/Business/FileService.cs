using StationCAD.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StationCAD.Web.Business
{

    static internal class UserRegistrationUploadFileColumns
    {
        public const string LastName = "Last Name";
        public const string FirstName = "First Name";
        public const string Email = "Email";
        public const string IdentificationNumber = "IDNumber";
    }
    public class FileColumn
    {
        public string Name;
        public Type DataType;
        public int Sequence;
    }

    public static class FileService
    {

        private static List<FileColumn> _validColumns;
        private static List<FileColumn> ValidFileColumns
        {
            get
            {
                if (_validColumns == null)
                {
                    _validColumns = new List<FileColumn>();
                    _validColumns.Add(new FileColumn { Name = UserRegistrationUploadFileColumns.IdentificationNumber, DataType = typeof(string) });
                    _validColumns.Add(new FileColumn { Name = UserRegistrationUploadFileColumns.LastName, DataType = typeof(string) });
                    _validColumns.Add(new FileColumn { Name = UserRegistrationUploadFileColumns.FirstName, DataType = typeof(string) });
                    _validColumns.Add(new FileColumn { Name = UserRegistrationUploadFileColumns.Email, DataType = typeof(string) });
                }
                return _validColumns;
            }
        }

        public static List<UserRegistration> ParseFileStream(Stream fileContents)
        {
            List<UserRegistration> results = new List<UserRegistration>();

            if (fileContents.Length == 0)
            {
                throw new InvalidDataException("The selected file is empty.");
            }

            using (StreamReader sr = new StreamReader(fileContents))
            {
                int lineCnt = 0;
                bool header = true;
                string fileText = sr.ReadToEnd();
                string[] fileArray = fileText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string currentLine in fileArray)
                {
                    if (header)
                    {
                        string errMsg = String.Empty;

                        if (!ValidateColumns(currentLine, out errMsg))
                        {
                            throw new InvalidDataException("The following columns are missing from the file: " + errMsg);
                        }
                        lineCnt++;
                        header = false;
                    }
                    else
                    {
                        results.Add(ConvertLinetoEntity(currentLine));
                        lineCnt++;
                    }
                }
                if (lineCnt <= 1)
                { throw new InvalidDataException("The selected file does not contain any user data."); }                
            }
            return results;
        }


        private static bool ValidateColumns(string headerContents, out string missingColumnNames)
        {
            // We'll assume that there are no missing columns to start. 
            missingColumnNames = string.Empty;

            string[] columns = headerContents.Split(',');
            foreach (FileColumn col in ValidFileColumns)
            {
                int i = 0;
                bool found = false;
                foreach (string item in columns)
                {
                    if (item == col.Name)
                    {
                        found = true;
                        col.Sequence = i;
                        break;
                    }
                    // track the column sequence for later
                    i++;
                }

                if (!found)
                {
                    missingColumnNames += col.Name + ", ";
                }
            }

            if (missingColumnNames.EndsWith(", "))
            {
                missingColumnNames = missingColumnNames.Substring(0, missingColumnNames.Length - 2);
            }

            return missingColumnNames == String.Empty;
        }


        private static UserRegistration ConvertLinetoEntity(string currentLine)
        {
            string[] lineParts = currentLine.Split(',');
            UserRegistration result = new UserRegistration();
            foreach (FileColumn col in ValidFileColumns)
            {
                switch (col.Name)
                {
                    case UserRegistrationUploadFileColumns.IdentificationNumber:
                        result.IdentificationNumber = lineParts[col.Sequence].ToString();
                        break;
                    case UserRegistrationUploadFileColumns.LastName:
                        result.LastName = lineParts[col.Sequence].ToString();
                        break;
                    case UserRegistrationUploadFileColumns.FirstName:
                        result.FirstName = lineParts[col.Sequence].ToString();
                        break;
                    case UserRegistrationUploadFileColumns.Email:
                        result.Email = lineParts[col.Sequence].ToString();
                        break;
                    default:
                        // we don't care about the rest of the columns provided.
                        break;
                }
            }
            return result;
        }
    }
}