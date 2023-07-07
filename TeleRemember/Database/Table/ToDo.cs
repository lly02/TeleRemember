using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleRemember.Database.Table
{
    [Table("ToDo")]
    public class ToDo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public required Guid Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public required string Title { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string? Description { get; set; }
        [Column(TypeName = "varchar(2048)")]
        public string? Link { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Due { get; set; }
        [Column(TypeName = "varchar(50)")]
        public required string Priority { get; set; }
        [Column(TypeName = "varchar(100)")]
        public required string CreatedBy { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "datetime")]
        public required DateTime CreatedDate { get; set; }
    }
}
