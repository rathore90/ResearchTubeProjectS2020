using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchTube.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}