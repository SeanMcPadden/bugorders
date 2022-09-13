using System;

namespace BugOrdersApp.Models
{
    public class BugOrderUpdate
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public DateTime DateTime { get; set; }

        public string UserName { get; set; }
    }
}
