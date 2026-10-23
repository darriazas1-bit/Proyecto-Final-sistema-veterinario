using System.Data;
using MySql.Data.MySqlClient;
using VetSystem.Dominio;

namespace VetSystem.Persistencia;

internal sealed class RepositorioVeterinaria
{
    private readonly string _cadenaConexion;

    public RepositorioVeterinaria(string cadenaConexion) => _cadenaConexion = cadenaConexion;

    // CLIENTES
    public DataTable ObtenerClientes() => Consultar(
        "SELECT id_cliente AS Id, nombre, telefono, correo, direccion FROM clientes ORDER BY nombre;");

    public void CrearCliente(Cliente cliente) => Ejecutar(
        "INSERT INTO clientes (nombre, telefono, correo, direccion) VALUES (@nombre, @telefono, @correo, @direccion);",
        ("@nombre", cliente.Nombre), ("@telefono", cliente.Telefono),
        ("@correo", cliente.Correo), ("@direccion", cliente.Direccion));

    public void ActualizarCliente(int id, Cliente cliente) => Ejecutar(
        "UPDATE clientes SET nombre = @nombre, telefono = @telefono, correo = @correo, direccion = @direccion WHERE id_cliente = @id;",
        ("@nombre", cliente.Nombre), ("@telefono", cliente.Telefono),
        ("@correo", cliente.Correo), ("@direccion", cliente.Direccion), ("@id", id));

    public void EliminarCliente(int id) => Ejecutar("DELETE FROM clientes WHERE id_cliente = @id;", ("@id", id));

    // MASCOTAS
    public DataTable ObtenerMascotas() => Consultar(@"
        SELECT m.id_mascota AS Id, m.nombre, m.especie, m.raza, m.edad, 
               m.id_cliente AS ClienteId, c.nombre AS Propietario
        FROM mascotas m
        INNER JOIN clientes c ON c.id_cliente = m.id_cliente
        ORDER BY m.nombre;");

    public void CrearMascota(Mascota mascota) => Ejecutar(
        "INSERT INTO mascotas (nombre, especie, raza, edad, id_cliente) VALUES (@nombre, @especie, @raza, @edad, @clienteId);",
        ("@nombre", mascota.Nombre), ("@especie", mascota.Especie),
        ("@raza", mascota.Raza), ("@edad", mascota.Edad), ("@clienteId", mascota.ClienteId));

    public void ActualizarMascota(int id, Mascota mascota) => Ejecutar(
        "UPDATE mascotas SET nombre = @nombre, especie = @especie, raza = @raza, edad = @edad, id_cliente = @clienteId WHERE id_mascota = @id;",
        ("@nombre", mascota.Nombre), ("@especie", mascota.Especie),
        ("@raza", mascota.Raza), ("@edad", mascota.Edad),
        ("@clienteId", mascota.ClienteId), ("@id", id));

    public void EliminarMascota(int id) => Ejecutar("DELETE FROM mascotas WHERE id_mascota = @id;", ("@id", id));

    // VETERINARIOS
    public DataTable ObtenerVeterinarios() => Consultar(
        "SELECT id_veterinario AS Id, nombre, especialidad, telefono FROM veterinarios ORDER BY nombre;");

    public void CrearVeterinario(Veterinario vet) => Ejecutar(
        "INSERT INTO veterinarios (nombre, especialidad, telefono) VALUES (@nombre, @especialidad, @telefono);",
        ("@nombre", vet.Nombre), ("@especialidad", vet.Especialidad), ("@telefono", vet.Telefono));

    public void ActualizarVeterinario(int id, Veterinario vet) => Ejecutar(
        "UPDATE veterinarios SET nombre = @nombre, especialidad = @especialidad, telefono = @telefono WHERE id_veterinario = @id;",
        ("@nombre", vet.Nombre), ("@especialidad", vet.Especialidad), ("@telefono", vet.Telefono), ("@id", id));

    public void EliminarVeterinario(int id) => Ejecutar("DELETE FROM veterinarios WHERE id_veterinario = @id;", ("@id", id));

    // CITAS
    public DataTable ObtenerCitas() => Consultar(@"
        SELECT c.id_cita AS Id, c.fecha, c.motivo, c.estado,
               m.nombre AS Mascota, v.nombre AS Veterinario
        FROM citas c
        INNER JOIN mascotas m ON m.id_mascota = c.id_mascota
        INNER JOIN veterinarios v ON v.id_veterinario = c.id_veterinario
        ORDER BY c.fecha DESC;");

    public void CrearCita(Cita cita) => Ejecutar(
        "INSERT INTO citas (fecha, motivo, estado, id_mascota, id_veterinario) VALUES (@fecha, @motivo, @estado, @mascotaId, @veterinarioId);",
        ("@fecha", cita.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
        ("@motivo", cita.Motivo),
        ("@estado", cita.Estado),
        ("@mascotaId", cita.MascotaId),
        ("@veterinarioId", cita.VeterinarioId));

    public void ActualizarEstadoCita(int id, string nuevoEstado) => Ejecutar(
        "UPDATE citas SET estado = @estado WHERE id_cita = @id;",
        ("@estado", nuevoEstado), ("@id", id));

    public void EliminarCita(int id) => Ejecutar("DELETE FROM citas WHERE id_cita = @id;", ("@id", id));

    // HISTORIAL CLÍNICO
    public DataTable ObtenerHistoriales() => Consultar(@"
        SELECT h.id_historial AS Id, h.fecha, h.diagnostico, h.tratamiento,
               m.nombre AS Mascota, v.nombre AS Veterinario
        FROM historial_clinico h
        INNER JOIN mascotas m ON m.id_mascota = h.id_mascota
        INNER JOIN veterinarios v ON v.id_veterinario = h.id_veterinario
        ORDER BY h.fecha DESC;");

    public void CrearHistorial(HistorialClinico h) => Ejecutar(
        "INSERT INTO historial_clinico (fecha, diagnostico, tratamiento, id_mascota, id_veterinario) VALUES (@fecha, @diag, @trat, @mascotaId, @vetId);",
        ("@fecha", h.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
        ("@diag", h.Diagnostico),
        ("@trat", h.Tratamiento),
        ("@mascotaId", h.MascotaId),
        ("@vetId", h.VeterinarioId));

    public void EliminarHistorial(int id) => Ejecutar("DELETE FROM historial_clinico WHERE id_historial = @id;", ("@id", id));

    // MÉTODOS PRIVADOS REUTILIZABLES
    private DataTable Consultar(string sql)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = sql;
        using var lector = comando.ExecuteReader();
        var tabla = new DataTable();
        tabla.Load(lector);
        return tabla;
    }

    private void Ejecutar(string sql, params (string Nombre, object Valor)[] parametros)
    {
        using var conexion = AbrirConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = sql;
        foreach (var (nombre, valor) in parametros)
        {
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        }
        comando.ExecuteNonQuery();
    }

    private MySqlConnection AbrirConexion()
    {
        var conexion = new MySqlConnection(_cadenaConexion);
        conexion.Open();
        return conexion;
    }
}