using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Models
{
    public class FilterModel
    {
        public int ID { get; set; }
        [Display(Name = "Resistance 1 [Ohm]")]
        [Required(ErrorMessage = "You must enter a value for the Resistance field")]
        [Range(0, 20, ErrorMessage = "Value for {0} must be between {1} and {2}")]

        public double Resistance1 { get; set; }
        [Display(Name = "Resistance 2  [Ohm]")]
        [Required(ErrorMessage = "You must enter a value for the Resistance field")]
        [Range(0, 20, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public double Resistance2 { get; set; }
        [Required(ErrorMessage = "You must enter a value for the Inductance field")]
        [Range(0.000001, 10, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Inductance [H]")]
        public double Inductance { get; set; }
        [Required(ErrorMessage = "You must enter a value for the Capacitance field")]
        [Range(0.000001, 10, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Capacitance [F]")]
        public double Capacitance { get; set; }
        [Required(ErrorMessage = "You must enter a value for the Amplitude of Input Voltage field")]
        [Range(1, 500, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Amplitude of Input Voltage [V]")]
        public double U1 { get; set; }
        [Required(ErrorMessage = "You must enter a value for the Minimum Frequency field")]
        [Range(0.1, 20, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Minimum Frequency [Hz]")]
        public double Fmin { get; set; }
        [Required(ErrorMessage = "You must enter a value for the Max Frequency field")]
        [Range(10, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Maximum Frequency [Hz]")]
        public double Fmax { get; set; }
        [Required(ErrorMessage = "You must enter a value for the Number of points field")]
        [Range(100, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Number Of Points")]
        public int NumOfRows { get; set; }
    }
}
