using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasswordApp.Services.DataAnnotations
{
    public class LatinStringAttribute : ValidationAttribute
    {
        string reg = @"^[A-Za-z0-9\W]+$";

        public override bool IsValid(object value)
        {
            string valueString = value as string;
            if (!string.IsNullOrEmpty(valueString))
            {
                return Regex.Match(valueString, reg).Success;
            }
            return true;
        }
    }
}
