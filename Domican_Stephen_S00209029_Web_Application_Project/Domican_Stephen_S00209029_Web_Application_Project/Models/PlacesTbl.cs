//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domican_Stephen_S00209029_Web_Application_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PlacesTbl
    {
        [Display(Name = "ID")]
        public int PlacesID { get; set; }
        [Display(Name = "Name")]
        public string PlaceName { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Location Type")]
        public string PlaceType { get; set; }
        public byte[] Image { get; set; }
    }
}
