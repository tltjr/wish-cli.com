using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace Display.Models
{
    public class Post
    {
        private const int SummaryLength = 417;

        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumMonth
        {
            get { return CreatedAt.Month; }
        }

        public int Year
        {
            get { return CreatedAt.Year; }
        }

        public string Month
        {
            get { return ConvertMonthToString(CreatedAt.Month); }
        }

        public string Day
        {
            get
            {
                var day = CreatedAt.Day;
                if(day / 10 == 0)
                {
                    return "0" + day;
                }
                return day.ToString();
            }
        }

        public string Url { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; } 
            set
            {
                _title = value;
                var lower = Title.ToLower();
                var spacesParsed = Regex.Replace(lower, @" ", "_");
                Slug = Regex.Replace(spacesParsed, @"[^a-z0-9_]", String.Empty);
                Url = CreatedAt.Year + "/" + CreatedAt.Month + "/" + Slug + "/";
            }
        }
        public IEnumerable<string> Tags { get; set; }
        public string TagsRaw { get; set; }
        private string _body;

        [DataType(DataType.MultilineText)]
        public string Body
        {
            get { return _body; } 
            set
            {
                _body = value;
                More = _body.Length >= SummaryLength;
                Summary = More ? _body.Substring(0, SummaryLength) : _body;
            }
        }

        public string Summary { get; set; }
        public bool More { get; set; }

        public string Slug { get; set; }
                
        private static string ConvertMonthToString(int month)
        {
            switch (month)
            {
                case 1:
                    return "JAN";
                case 2:
                    return "FEB";
                case 3:
                    return "MAR";
                case 4:
                    return "APR";
                case 5:
                    return "MAY";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AUG";
                case 9:
                    return "SEP";
                case 10:
                    return "OCT";
                case 11:
                    return "NOV";
                default:
                    return "DEC";
            } 
        }
    }
}