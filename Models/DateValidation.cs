using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

[AttributeUsage(AttributeTargets.All)]
public class DateValidation : ValidationAttribute
{
    public override bool IsValid(object value){
        DateTime newVal = (DateTime)value;
        if(newVal < DateTime.Now){
            return false;
        }
        return true;
    }

    public override string FormatErrorMessage(string name){
        return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
    }
}