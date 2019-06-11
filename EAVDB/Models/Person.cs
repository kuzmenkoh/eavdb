using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EAVDB.Models
{
    public class Person : Entity<Person>
    {
        public IEnumerable<Record> Records { get; set; }
        
        // ------- Various common or required user attributes should be in this model, placed outside of EAV ------- //
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
