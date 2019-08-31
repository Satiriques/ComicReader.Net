﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Net.Common.Models
{
    public class Character
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public string Name { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}