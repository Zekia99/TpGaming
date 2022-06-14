using System.ComponentModel.DataAnnotations;

namespace AzureGaming.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Abonnement
    {
        //Id : Id de l'abonnement
        [Required(), Key]
        public int Id { get; set; }

        // DateDebut : Date du début de l'abonnement
        [Required(), DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }

        // Duree : Duree de l'abonnement
        [Required(), Range(1, 12)]
        public int Duree { get; set; }

        // TarifMensuel : Tarif Mensuel de l'abonnement
        [Required(), DataType(DataType.Currency)]
        public double TarifMensuel { get; set; }

        //Mail : adresse mail entree lors de l'abonnement
        [Required(), DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

    }
}
