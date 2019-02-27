﻿using System;
using System.Globalization;
using System.Web.Mvc;

namespace Cats.Web.Hub.Helpers
{
    public static class DateHelpers
    {
        public static string ToCTSPreferedDateFormat(this DateTime date, string lang)
        {
            if(lang == "en")
            {
                IFormatProvider provider = new CultureInfo("en-GB");
                return date.ToString("dd-MMM-yyyy", provider);
                
            }else
            {
                Infrastructure.EthiopianDate ethiopianDate = new Infrastructure.EthiopianDate(date);
                return ethiopianDate.ToLongDateString();
            }

        }
        public static string FormatDateFromString(this HtmlHelper helper,string dateAsString)
        {
            DateTime theRealDate = Convert.ToDateTime((dateAsString));
            return ToCTSPreferedDateFormat(theRealDate,"am");
        }
        public static Decimal ToPreferedWeightMeasurment(this Decimal quantity, string weightMeasurment)
        {

            if (weightMeasurment.Equals("qn"))
            {
                return quantity*10;
            }
            return quantity;

        }
        
    }
}