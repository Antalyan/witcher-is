using System.ComponentModel.DataAnnotations;


namespace WitcherProject.PresentationLayer.Model.Administration
{
    public class LoginModel : BaseModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
        public DateTime LoginStarted { get; set; }
    }

}

