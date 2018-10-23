using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Helper
{
    public static class Toastr
    {
        public static string Added
        {
            get { return "toastr.success('Information successfully Added!', 'Success!');"; }
        }
        public static string Updated
        {
            get { return "toastr.success('Information successfully Updated!', 'Success!');"; }
        }
        public static string Deleted
        {
            get { return "toastr.success('Information successfully Deleted!', 'Success!');"; }
        }

        public static string BadRequest
        {
            get { return "toastr.error('Bad request!', 'Error!');"; }
        }

        public static string HttpNotFound
        {
            get { return "toastr.error('Information not found!', 'Error!');"; }
        }

        public static string DbError(string exceptionMessage)
        {
            return !string.IsNullOrWhiteSpace(exceptionMessage) ?
            @"toastr.error('" + exceptionMessage + "', 'Database Error!');" :
                @"toastr.error('Error occured in database, Try again!', 'Database Error!');";
        }

        public static string CustomError(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentNullException();
            return string.Format("toastr.error('{0}', 'Error!');", errorMessage);
        }

        public static string CustomError(string errorTitle, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorTitle) || string.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentNullException();
            return string.Format("toastr.error('{0}', '{1}');", errorMessage, errorTitle);
        }

        public static string CustomSuccess(string successMessage)
        {
            if (string.IsNullOrWhiteSpace(successMessage)) throw new ArgumentNullException();
            return string.Format("toastr.success('{0}', 'Success!');", successMessage);
        }

        public static string CustomSuccess(string successTitle, string successMessage)
        {
            if (string.IsNullOrWhiteSpace(successTitle) || string.IsNullOrWhiteSpace(successMessage)) throw new ArgumentNullException();
            return string.Format("toastr.success('{0}', '{1}');", successMessage, successTitle);
        }
    }
}