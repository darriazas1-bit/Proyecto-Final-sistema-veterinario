namespace VetSystem.Dominio;

public class Cita
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string Estado { get; set; } = "Pendiente";
    public int MascotaId { get; set; }
    public int VeterinarioId { get; set; }
}