using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPoco.Tests.Common
{
    [TableName("RideSharkTestData")]
    [PrimaryKey( "Id")]
    public class RideSharkTestData
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string TextData { get; set; }

        public bool BoolValue { get; set; }
    }
}
