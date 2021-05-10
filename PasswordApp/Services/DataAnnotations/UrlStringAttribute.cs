using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.DataAnnotations
{
    public class UrlStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string valueString = value as string;
            if (!string.IsNullOrEmpty(valueString))
            {
                if (!Uri.IsWellFormedUriString(valueString, UriKind.Absolute))
                    return false;
                Uri tmp;
                if (!Uri.TryCreate(valueString, UriKind.Absolute, out tmp))
                    return false;
                return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;

            }
            return true;
        }
    }
}
