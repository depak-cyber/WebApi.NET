﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Dto
{
    public class WebDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public  int Occupancy {  get; set; }
        public int Sqft {  get; set; }
       // public DateTime CreatedDate { get; set; }
    }
}
