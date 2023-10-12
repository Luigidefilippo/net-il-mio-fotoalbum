using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Devi inserire l'email per mandare il messaggio")]
        [EmailAddress(ErrorMessage = "Inserisci una mail corretta")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Devi inserire un messaggio!")]
        [StringLength(150, ErrorMessage = "Lunghezza massima della descrizione 150 caratteri")]
        public string Text { get; set; }

        public Message(string title, string text)
        {
            Title = title;
            Text = text;
        }
        public Message()
        { }

    }
}
