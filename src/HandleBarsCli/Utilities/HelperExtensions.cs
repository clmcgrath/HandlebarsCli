using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HandlebarsCli.Utilities
{
    public static class HelperExtensions
    {
        public static bool IsNumeric<T>(this T value)
        {
            var isNumericType = value is sbyte
                                || value is byte
                                || value is short
                                || value is ushort
                                || value is int
                                || value is uint
                                || value is long
                                || value is ulong
                                || value is float
                                || value is double
                                || value is decimal;

            if (isNumericType)
                return true;

            decimal number;
            return decimal.TryParse(value.ToString(), out number);
        }

        /// <summary>
        ///     Formats a number to a certain number of decimal places.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to format it to.</param>
        /// <returns>
        ///     Returns String.Empty if the number is null or empty or the formatted number otherwise. Defaults to 0 if
        ///     conversion fails.
        /// </returns>
        public static string FormatNumber(this string number, int decimalPlaces)
        {
            return string.IsNullOrEmpty(number) ? string.Empty : FormatNumber(ToDouble(number), decimalPlaces);
        }

        /// <summary>
        ///     Formats a number to a certain number of decimal places.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to format it to.</param>
        /// <returns>Returns a string containing the formatted number.</returns>
        public static string FormatNumber(this double number, int decimalPlaces)
        {
            var formatter = $"{{0:0.{decimalPlaces}}}";
            return decimalPlaces > -1 ? string.Format(formatter, number) : $"{number}";
        }

        /// <summary>
        ///     Formats a string number into a percentage. If the string is null or empty, an empty string will be returned. If the
        ///     string is not valid, 0% will be returned.
        /// </summary>
        /// <param name="number">The string containing the number to be parsed and formatted.</param>
        /// <param name="decimalPlaces">The number of decimal places to show.</param>
        public static string FormatPercent(this string number, int decimalPlaces = 0)
        {
            return string.IsNullOrEmpty(number) ? string.Empty : FormatPercent(ToDouble(number), decimalPlaces);
        }

        /// <summary>
        ///     Formats a number into a percentage.
        /// </summary>
        /// <param name="number">The number to be formatted.</param>
        /// <param name="decimalPlaces">The number of decimal places to show.</param>
        public static string FormatPercent(this double number, int decimalPlaces = 0)
        {
            var formatter = $"{{0:0.{new string('0', decimalPlaces)}}}%";
            return decimalPlaces > -1 ? string.Format(formatter, number) : $"{number}%";
        }

        /// <summary>
        ///     Converts an integer to a boolean. 1 = True, Anything else = False
        /// </summary>
        /// <param name="toBool">The integer to be converted to a boolean.</param>
        /// <returns>1 = True, Anything else = False</returns>
        public static bool IntToBool(this int toBool)
        {
            return toBool == 1;
        }

        /// <summary>
        ///     This function is used to convert a object, string or null to string.
        ///     <para>Returns: the string or String.Empty.</para>
        /// </summary>
        /// <param name="convert">The object to convert to string.</param>
        /// <returns>string</returns>
        public static string ObjectToString(this object convert)
        {
            return convert?.ToString() ?? string.Empty;
        }

        /// <summary>
        ///     Convert the'True' or 'False' that an MSSQL bit returns to an int
        ///     <para>Returns: 1 if 'True', otherwise 0</para>
        /// </summary>
        /// <param name="convert">The string boolean value to convert to 1.</param>
        /// <returns>1 if 'True', otherwise 0</returns>
        public static int StringBooleanToInt(this string convert)
        {
            return convert.Equals("True") ? 1 : 0;
        }

        /// <summary>
        ///     Converts a string to a boolean. Any format of the word "true" or the number "1" = true. Anything else = false
        /// </summary>
        /// <param name="toBool">The string to be converted to a boolean.</param>
        /// <returns>"true", "1" = True, Anything else = False</returns>
        public static bool StringToBool(this string toBool)
        {
            if (toBool != null)
                return toBool.ToLower().Equals("true") || toBool.Equals("1");
            return false;
        }

        /// <summary>
        ///     This function is used to convert a string to a double.
        ///     <para>Returns: the integer or 0 if invalid.</para>
        /// </summary>
        /// <param name="convert">The string to convert to double type.</param>
        /// <param name="defaultVal"></param>
        /// <returns>0 on error or the string as a double.</returns>
        public static double StringToDbl(this string convert, double defaultVal)
        {
            double num;
            try
            {
                if (convert == null || convert.Equals("")) return defaultVal;
                num = double.Parse(convert, NumberStyles.Any);
            }
            catch
            {
                num = defaultVal;
            }
            return num;
        }

        /// <summary>
        ///     Takes a validated phone number and removes all special characters and formats as 10 digit plain number
        /// </summary>
        /// <param name="sPhone">Validated phone number string</param>
        /// <param name="error"></param>
        /// <returns>Plain phone number "##########"</returns>
        public static string StripPhoneNumber(this string sPhone, ref bool error)
        {
            return StripPhoneNumber(sPhone, PhoneType.Plain, ref error);
        }

        /// <summary>
        ///     akes a validated phone number and removes all special characters and formats as specified
        /// </summary>
        /// <param name="sPhone">Validated phone number string</param>
        /// <param name="ptReturnType">The way the phone number will be formatted</param>
        /// <returns>Formatted phone number</returns>
        public static string StripPhoneNumber(this string sPhone, PhoneType ptReturnType)
        {
            var error = false;
            return StripPhoneNumber(sPhone, ptReturnType, ref error);
        }


        /// <summary>
        ///     akes a validated phone number and removes all special characters and formats as specified
        /// </summary>
        /// <param name="sPhone">Validated phone number string</param>
        /// <param name="ptReturnType">The way the phone number will be formatted</param>
        /// <param name="error"></param>
        /// <returns>Formatted phone number</returns>
        // ReSharper disable once RedundantAssignment
        public static string StripPhoneNumber(this string sPhone, PhoneType ptReturnType, ref bool error)
        {
            sPhone = Regex.Replace(sPhone, "[^0-9]", "");
            if (sPhone.Length != 10)
            {
                error = true;
                return "";
            }
            switch (ptReturnType)
            {
                case PhoneType.Dashed:
                    sPhone = $"{sPhone.Substring(0, 3)}-{sPhone.Substring(3, 3)}-{sPhone.Substring(6, 4)}";
                    break;

                case PhoneType.Styled:
                    sPhone = $"({sPhone.Substring(0, 3)}) {sPhone.Substring(3, 3)}-{sPhone.Substring(6, 4)}";
                    break;

                case PhoneType.Spaced:
                    sPhone = $"{sPhone.Substring(0, 3)} {sPhone.Substring(3, 3)} {sPhone.Substring(6, 4)}";
                    break;
                case PhoneType.Plain:
                    break;
            }
            error = false;
            return sPhone;
        }

        /// <summary>
        ///     This function is used to convert a string to a double.
        ///     <para>Returns: the integer or 0 if invalid.</para>
        /// </summary>
        /// <param name="convert">The string to convert to double type.</param>
        /// <returns>0 on error or the string as a double.</returns>
        public static double ToDouble(this string convert)
        {
            double num;
            try
            {
                if (convert == null || convert.Equals("")) return 0;
                num = double.Parse(convert, NumberStyles.Any);
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        /// <summary>
        ///     This function is used to convert a string to an integer.
        ///     <para>Returns: the integer or 0 if invalid.</para>
        /// </summary>
        /// <param name="convert">The string to convert to integer.</param>
        /// <param name="returnValue">Changes default return from 0 to specified number</param>
        /// <returns>0 on error or the string as an integer.</returns>
        public static int ToInt(this string convert, int returnValue = 0)
        {
            int num;
            try
            {
                if (convert == null || convert.Equals("")) return returnValue;
                num = int.Parse(convert, NumberStyles.Any);
            }
            catch
            {
                num = returnValue;
            }
            return num;
        }

        /// <summary>
        ///     This function is used to convert a string to an integer between a minimum and maximum value. If the value is
        ///     outside of the bounds, it will be constrained to the bound value.
        /// </summary>
        /// <param name="convert">The string to convert to integer.</param>
        /// <param name="minVal">The minimum value to constrain the converted integer to (inclusive).</param>
        /// <param name="maxVal">The maximum value to constrain the converted integer to (inclusive).</param>
        /// <param name="invalidValue">The value to return if <paramref name="convert" /> cannot be converted.</param>
        /// <returns>
        ///     An integer constrained within the minVal and maxVal (inclusive) or <paramref name="invalidValue" /> if it
        ///     cannot be parsed.
        /// </returns>
        public static int ToInt(this string convert, int minVal, int maxVal, int invalidValue)
        {
            int num;
            try
            {
                if (convert == null || convert.Equals("")) return invalidValue;
                num = int.Parse(convert);
                if (num > maxVal) num = maxVal;
                if (num < minVal) num = minVal;
            }
            catch
            {
                num = invalidValue;
            }
            return num;
        }

        /// <summary>
        ///     This function is used to convert a string to an integer.
        ///     <para>Returns: the integer or 0 if invalid.</para>
        /// </summary>
        /// <param name="convert">The string to convert to integer.</param>
        /// <param name="returnValue">Changes default return from 0 to specified number</param>
        /// <returns>0 on error or the string as an long.</returns>
        public static long ToLong(this string convert, int returnValue = 0)
        {
            long num;
            try
            {
                if (convert == null || convert.Equals(""))
                    return returnValue;
                num = long.Parse(convert, NumberStyles.Any);
            }
            catch
            {
                num = returnValue;
            }
            return num;
        }

        public static string DefaultIfEmpty(this string str, string defaultValue)
        {
            return string.IsNullOrEmpty(str) ? defaultValue : str;
        }

        /// <summary>
        ///     Truncates a string to the specified max length. If the string is shorter than the max length, it will be returned
        ///     as is.
        /// </summary>
        /// <param name="value">The string to truncate.</param>
        /// <param name="maxLength">The maximum number of characters you want in the string.</param>
        /// <returns>A truncated version of the string if applicable.</returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (value == null) return null;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        ///     Empty private constructor for static (only) class.
        /// </summary>
        /// <summary>
        ///     Validates whether the given vale falls into a range of values of the same type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In<T>(this T obj, params T[] values)
            where T : IComparable
        {
            return values != null && values.Any(o => o.CompareTo(obj) == 0);

            // if no values found then is false
        }

        /// <summary>
        ///     Checks to see if a string is a valid datetime or not.
        /// </summary>
        /// <param name="value">The string value to attempt to convert to a date time.</param>
        /// <param name="format">DateTime format that 'value' must comply with</param>
        /// <param name="datetimeobj">An out parameter which contains the parsed date time object.</param>
        /// <returns>True if the string is a valid date time.</returns>
        public static bool IsDateTime(this string value, string format, out DateTime? datetimeobj)
        {
            datetimeobj = TryParseDateTime(value);
            return DateTime.MinValue != datetimeobj;
        }

        public static bool IsNumeric(this string str)
        {
            bool isNumeric;
            {
                int val;
                isNumeric = int.TryParse(str, out val);
                if (isNumeric)
                    return true;
            }
            {
                float val;
                isNumeric = float.TryParse(str, out val);
                if (isNumeric)
                    return true;
            }
            {
                double val;
                isNumeric = double.TryParse(str, out val);
                if (isNumeric)
                    return true;
            }
            {
                long val;
                isNumeric = long.TryParse(str, out val);
                if (isNumeric)
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     This method checks data to see if it is valid.
        /// </summary>
        /// <param name="strVal">The string data to check.</param>
        /// <param name="typeValid">This specifies the type of validation to use.</param>
        /// <returns></returns>
        public static bool RegExCheck(this string strVal, ValidationType typeValid)
        {
            switch (typeValid)
            {
                case ValidationType.Name:
                    return RegExMatch(strVal, @"^[a-zA-Z ]+$");

                case ValidationType.NameNoSpace:
                    return RegExMatch(strVal, @"^[a-zA-Z]+$");

                case ValidationType.Email:
                    return RegExMatch(strVal.ToLower(),
                        @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[a-z]{2}|aero|asia|biz|cat|com|coop|info|int|jobs|mobi|museum|name|net|org|post|pro|tel|travel|xxx|edu|mil|gov)\b$");

                case ValidationType.Password:
                    return RegExMatch(strVal, @"^[a-zA-Z0-9 !#$@%&*+=?^_{|}~-]{8,}$"); // At least 8 characters
                case ValidationType.Postal:
                    return RegExMatch(strVal.ToUpper(),
                        @"^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] [0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]$");

                case ValidationType.PostalAlternate:
                    return RegExMatch(strVal.ToUpper(),
                        @"^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][ ]?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]$");

                case ValidationType.ZipCode:
                    return RegExMatch(strVal, @"^[0-9]{5}(-[0-9]{4})?$");

                case ValidationType.Phone:
                    return RegExMatch(strVal, @"^\d{3}-\d{3}-\d{4}$");

                case ValidationType.PhoneOpen:
                    return RegExMatch(strVal, @"^\(?\d{3}\)?[- \.]?\d{3}[- \.]?\d{4}$");

                case ValidationType.PhoneAlternate:
                    return RegExMatch(strVal, @"^\d{10}$");

                case ValidationType.PhoneExt:
                    return RegExMatch(strVal, @"^\d{1,5}$");

                default:
                    return false;
            }
        }

        /// <summary>
        ///     This method checks data to see if it is valid as well as allowing you to specify a minimum/maximum length.
        ///     (Overrides Password minimum length.)
        /// </summary>
        /// <param name="strVal">The string data to check.</param>
        /// <param name="typeValid">This specifies the type of validation to use.</param>
        /// <param name="minLength">This is the minimum length that the string must be to pass validation.</param>
        /// <param name="maxLength">This is the maximum length that the string must be to pass validation.</param>
        /// <returns></returns>
        public static bool RegExCheck(this string strVal, ValidationType typeValid, int minLength, int maxLength)
        {
            if (minLength == 0 && strVal.Length == 0)
                return true;
            if (strVal.Length < minLength || strVal.Length > maxLength)
                return false;
            if (typeValid == ValidationType.Password)
                return RegExMatch(strVal, $@"^[a-zA-Z0-9 !#$@%&*+=?^_{{|}}~-]{{{minLength},{maxLength}}}$");
            return RegExCheck(strVal, typeValid);
        }

        /// <summary>
        ///     Perform regular expression matching returning a count for occurences
        /// </summary>
        /// <param name="toCheck">The string to be checked.</param>
        /// <param name="regEx">The regular expression pattern.</param>
        /// <returns>Number of occurences of pattern in toCheck string</returns>
        public static int RegExCount(this string toCheck, string regEx)
        {
            var regexp = new Regex(regEx);
            return regexp.Matches(toCheck).Count;
        }

        /// <summary>
        ///     This function does regular expression matching and returns a true or false value for the results of the pattern
        ///     match.
        /// </summary>
        /// <param name="toCheck">The string to be checked.</param>
        /// <param name="regEx">The regular expression pattern.</param>
        /// <returns>Whether (true) or not (false) the pattern matched.</returns>
        public static bool RegExMatch(this string toCheck, string regEx)
        {
            var regexp = new Regex(regEx);
            return regexp.IsMatch(toCheck);
        }

        public static bool RegExMatch(this string toCheck, string regEx, out MatchCollection matches)
        {
            var regexp = new Regex(regEx);
            matches = regexp.Matches(toCheck);
            return regexp.IsMatch(toCheck);
        }


        /// <summary>
        ///     Attempts to convert a string to a date and checks it in a specified format
        /// </summary>
        /// <param name="value">A string to attempt to convert to a date time</param>
        /// <param name="defaultDate">Default return value in case of error</param>
        /// <returns>DateTime from string or specified default value if String or format was invalid</returns>
        public static DateTime? TryParseDateTime(this string value, DateTime? defaultDate = null)
        {
            if (string.IsNullOrEmpty(value)) return defaultDate;
            try
            {
                return DateTime.Parse(value);
            }
            catch
            {
                // ignored
            }
            return defaultDate;
        }

        /// <summary>
        ///     Checks whether or not a SIN is valid. Does not check formatting, only if the number itself is a valid SIN.
        /// </summary>
        /// <param name="sin">Nine-digit SIN string.</param>
        /// <returns></returns>
        public static bool ValidateSIN(this string sin)
        {
            //See http://www.ryerson.ca/JavaScript/lectures/forms/textValidation/sinProject.html
            // for what this was based on.
            int throwaway;
            if (sin == null || sin.Length != 9 || !int.TryParse(sin, out throwaway))
                return false;
            var ttl = 0;
            var check = int.Parse(sin[8].ToString());
            for (var i = 0; i < 8; i++)
                if (i % 2 == 0)
                {
                    ttl += int.Parse(sin[i].ToString());
                }
                else
                {
                    var digit = int.Parse(sin[i].ToString());
                    if (digit < 5)
                        ttl += digit * 2;
                    else
                        ttl += 2 * (digit - 5) + 1; //Odd number in series = 2n-1
                }
            if (ttl % 10 == 0 && check == 0)
                return true;
            //If the total is not a multiple of 10, subtract the remainder
            //  from 10 and see if it's equal to the check digit
            return 10 - ttl % 10 == check;
        }

        public static string ToAssemblyPath(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetFullPath(path);
        }

        public static string ToDirectoryPath(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

    }

    public enum PhoneType
    {
        Dashed,
        Styled,
        Spaced,
        Plain
    }

    public enum ValidationType
    {
        /// <summary>
        ///     Only letters and spaces allowed.
        /// </summary>
        Name,

        /// <summary>
        ///     Only letters allowed.
        /// </summary>
        NameNoSpace,

        /// <summary>
        ///     Only valid email address formats allowed.
        /// </summary>
        Email,

        /// <summary>
        ///     Only letters, spaces and !#$@%&amp;*+=?^_{|}~- allowed. 8 characters or longer.
        /// </summary>
        Password,

        /// <summary>
        ///     Only postal codes in the format "A1A 1A1" allowed.
        /// </summary>
        Postal,

        /// <summary>
        ///     Postal codes in the format "A1A1A1" or "A1A 1A1" allowed.
        /// </summary>
        PostalAlternate,

        /// <summary>
        ///     Only 5 digit (eg. 12345) or 9 digit (eg. 12345-6789) zip codes allowed.
        /// </summary>
        ZipCode,

        /// <summary>
        ///     Only numbers in the format "###-###-####" allowed.
        /// </summary>
        Phone,

        /// <summary>
        ///     "(###) ###-####" allowed, spaces or dashes with brackets around first three, all optional
        /// </summary>
        PhoneOpen,

        /// <summary>
        ///     Only 10 digit numbers (eg. "##########") allowed.
        /// </summary>
        PhoneAlternate,

        /// <summary>
        ///     Only 1 to 5 digit numbers allowed.
        /// </summary>
        PhoneExt
    }




}