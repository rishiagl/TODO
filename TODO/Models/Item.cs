using System;

namespace TODO.Models
{
    public enum Priority
    {
        HIGH,
        LOW,
    }

    public enum Status
    {
        PENDING,
        OVERDUE,
    }
    internal class Item
    {
        private static int _id;
        public DateTime Startdate { get; set; }

        public int Id { get; set; }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                if (value.Length <= 20 && value.Length > 0) _Title = value;
                else throw new Exception();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { if (value.Length <= 100 && value.Length > 0) _description = value; else throw new Exception(); }
        }


        public Priority Priority { get; set; }

        public DateTime ExpectedEndDate { get; set; }

        static Item()
        {
            _id = 0;
        }

        public Item()
        {
            Id = _id++;
            Startdate = DateTime.Today;
            Title = " ";
            Description = " ";
            Priority = Priority.LOW;
            ExpectedEndDate = DateTime.Today;
        }
    }
}
