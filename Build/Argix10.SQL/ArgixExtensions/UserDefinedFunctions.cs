using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions; //Match, Regex

public partial class UserDefinedFunctions
{
   
    /// <summary>
    /// Searches the input string for an occurrence
    /// of the regular expression supplied
    /// in a pattern parameter with matching options
    /// supplied in an options parameter.
    /// </summary>
    /// <param name="input">The string to be tested for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="options">A bitwise OR combination
    ///    of RegexOption enumeration values.</param>
    /// <returns>true - if inputted string matches
    /// to pattern, else - false</returns>
    /// <exception cref="System.ArgumentException">
    ///       Regular expression parsing error.</exception>
    [SqlFunction(Name = "RegexMatch", IsDeterministic = true, IsPrecise = true)]
     //[Microsoft.SqlServer.Server.SqlFunction]
    public static SqlBoolean RegexMatch(SqlString input,
                  SqlString pattern, SqlInt32 options)
    {
        if (input.IsNull)
        {
            return SqlBoolean.Null;
            //if input is NULL, return NULL
        }
        if (pattern.IsNull)
        {
            pattern = String.Empty;
            //""
        }
        if (options.IsNull)
        {
            options = 0;  //RegexOptions.None
        }

        try
        {
            Match match = Regex.Match((string)input,
                          (string)pattern, (RegexOptions)(int)options);
            if (match.Value != String.Empty)
            {
                return SqlBoolean.True;
            }
        }
        catch
        {
            throw;
        }

        return SqlBoolean.False;
    }
};

