namespace VetSystem.Dominio;

public class HistorialClinico
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Diagnostico { get; set; } = string.Empty;
    public string Tratamiento { get; set; } = string.Empty;
    public int MascotaId { get; set; }
    public int VeterinarioId { get; set; }
}