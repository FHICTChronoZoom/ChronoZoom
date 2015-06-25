using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Models
{
    public class Bookmark
    {
        public enum BookmarkType
        {
            Timeline = 0,
            Exhibit = 1,
            ContentItem = 2,
        }
        public class Bookmark
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Url { get; set; }

            public BookmarkType ReferenceType { get; set; }

            public Guid ReferenceId { get; set; }

            public int? LapseTime { get; set; }

            public string Description { get; set; }

            public int SequenceId { get; set; }

            public Bookmark()
            {
                this.Id = new Guid();
            }
        }
    }
}
