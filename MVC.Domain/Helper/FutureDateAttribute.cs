using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain.Helper;
    public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime date;
        if (value is DateTime)
        {
            date = (DateTime)value;
            return date > DateTime.Now; // Check if expiration date is in the future
        }
        return false;
    }
}

