using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class MemorandumObject
    {
        public int MemorandumObjectId { get; set; }
        public string Value { get; set; }
        public int MemorandumId { get; set; }

        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }
    }
}
