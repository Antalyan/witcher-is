using System.ComponentModel.DataAnnotations;


namespace WitcherProject.PresentationLayer.Model
{
    public class LoginModel 
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
        public DateTime LoginStarted { get; set; }

        public string Error { get; set; }
    }

}

