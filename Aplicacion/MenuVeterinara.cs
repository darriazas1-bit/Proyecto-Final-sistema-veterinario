using System.Data;
using VetSystem.Dominio;
using VetSystem.Persistencia;

namespace VetSystem.Aplicacion;

internal sealed class MenuVeterinaria
{
    private readonly RepositorioVeterinaria _repositorio;
    private bool _continuar = true;

    public MenuVeterinaria(RepositorioVeterinaria repositorio) => _repositorio = repositorio;

    public void Ejecutar()
    {
        while (_continuar)
        {
            Console.Clear();
            MostrarEncabezado("MENÚ PRINCIPAL");
            Console.WriteLine(" 1. Gestión de Clientes");
            Console.WriteLine(" 2. Gestión de Mascotas");
            Console.WriteLine(" 3. Gestión de Veterinarios");
            Console.WriteLine(" 4. Gestión de Citas");
            Console.WriteLine(" 5. Historial Clínico");
            Console.WriteLine(" 0. Salir");
            Console.Write("\nSeleccione una opción: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": SubMenuClientes(); break;
                case "2": SubMenuMascotas(); break;
                case "3": SubMenuVeterinarios(); break;
                case "4": SubMenuCitas(); break;
                case "5": SubMenuHistorial(); break;
                case "0": _continuar = false; break;
                default: MensajeError("Opción no válida."); break;
            }
        }
    }

    // ==========================================
    // SUBMENÚS
    // ==========================================
    private void SubMenuClientes()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            MostrarEncabezado("MÓDULO: GESTIÓN DE CLIENTES");
            Console.WriteLine(" 1. Registrar cliente");
            Console.WriteLine(" 2. Listar clientes");
            Console.WriteLine(" 3. Actualizar cliente");
            Console.WriteLine(" 4. Eliminar cliente");
            Console.WriteLine(" 0. Volver al menú principal");
            Console.Write("\nOpción: ");

            try
            {
                switch (Console.ReadLine()?.Trim())
                {
                    case "1": CrearCliente(); break;
                    case "2": ListarClientes(); Pausar(); break;
                    case "3": ActualizarCliente(); break;
                    case "4": EliminarCliente(); break;
                    case "0": volver = true; break;
                    default: MensajeError("Opción no válida."); break;
                }
            }
            catch (Exception ex)
            {
                MensajeError($"Error: {ex.Message}");
            }
        }
    }

    private void SubMenuMascotas()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            MostrarEncabezado("MÓDULO: GESTIÓN DE MASCOTAS");
            Console.WriteLine(" 1. Registrar mascota");
            Console.WriteLine(" 2. Listar mascotas");
            Console.WriteLine(" 3. Actualizar mascota");
            Console.WriteLine(" 4. Eliminar mascota");
            Console.WriteLine(" 0. Volver al menú principal");
            Console.Write("\nOpción: ");

            try
            {
                switch (Console.ReadLine()?.Trim())
                {
                    case "1": CrearMascota(); break;
                    case "2": ListarMascotas(); Pausar(); break;
                    case "3": ActualizarMascota(); break;
                    case "4": EliminarMascota(); break;
                    case "0": volver = true; break;
                    default: MensajeError("Opción no válida."); break;
                }
            }
            catch (Exception ex)
            {
                MensajeError($"Error: {ex.Message}");
            }
        }
    }

    private void SubMenuVeterinarios()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            MostrarEncabezado("MÓDULO: GESTIÓN DE VETERINARIOS");
            Console.WriteLine(" 1. Registrar veterinario");
            Console.WriteLine(" 2. Listar veterinarios");
            Console.WriteLine(" 3. Actualizar veterinario");
            Console.WriteLine(" 4. Eliminar veterinario");
            Console.WriteLine(" 0. Volver al menú principal");
            Console.Write("\nOpción: ");

            try
            {
                switch (Console.ReadLine()?.Trim())
                {
                    case "1": CrearVeterinario(); break;
                    case "2": ListarVeterinarios(); Pausar(); break;
                    case "3": ActualizarVeterinario(); break;
                    case "4": EliminarVeterinario(); break;
                    case "0": volver = true; break;
                    default: MensajeError("Opción no válida."); break;
                }
            }
            catch (Exception ex)
            {
                MensajeError($"Error: {ex.Message}");
            }
        }
    }

    private void SubMenuCitas()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            MostrarEncabezado("MÓDULO: GESTIÓN DE CITAS");
            Console.WriteLine(" 1. Agendar nueva cita");
            Console.WriteLine(" 2. Listar citas");
            Console.WriteLine(" 3. Cambiar estado de cita");
            Console.WriteLine(" 4. Eliminar cita");
            Console.WriteLine(" 0. Volver al menú principal");
            Console.Write("\nOpción: ");

            try
            {
                switch (Console.ReadLine()?.Trim())
                {
                    case "1": CrearCita(); break;
                    case "2": ListarCitas(); Pausar(); break;
                    case "3": ActualizarEstadoCita(); break;
                    case "4": EliminarCita(); break;
                    case "0": volver = true; break;
                    default: MensajeError("Opción no válida."); break;
                }
            }
            catch (Exception ex)
            {
                MensajeError($"Error: {ex.Message}");
            }
        }
    }

    private void SubMenuHistorial()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            MostrarEncabezado("MÓDULO: HISTORIAL CLÍNICO");
            Console.WriteLine(" 1. Registrar entrada médica");
            Console.WriteLine(" 2. Consultar historiales");
            Console.WriteLine(" 3. Eliminar registro de historial");
            Console.WriteLine(" 0. Volver al menú principal");
            Console.Write("\nOpción: ");

            try
            {
                switch (Console.ReadLine()?.Trim())
                {
                    case "1": CrearHistorial(); break;
                    case "2": ListarHistoriales(); Pausar(); break;
                    case "3": EliminarHistorial(); break;
                    case "0": volver = true; break;
                    default: MensajeError("Opción no válida."); break;
                }
            }
            catch (Exception ex)
            {
                MensajeError($"Error: {ex.Message}");
            }
        }
    }

    // ==========================================
    // OPERACIONES CRUD
    // ==========================================
    private void CrearCliente()
    {
        _repositorio.CrearCliente(LeerCliente());
        MensajeExito("Cliente registrado correctamente.");
    }

    private void ListarClientes() => MostrarTabla(_repositorio.ObtenerClientes(), "CLIENTES REGISTRADOS");

    private void ActualizarCliente()
    {
        ListarClientes();
        var id = LeerId("Id del cliente a actualizar");
        _repositorio.ActualizarCliente(id, LeerCliente());
        MensajeExito("Cliente actualizado correctamente.");
    }

    private void EliminarCliente()
    {
        ListarClientes();
        var id = LeerId("Id del cliente a eliminar");
        if (ConfirmarAccion("¿Está seguro de eliminar este cliente? Se eliminarán sus mascotas asociadas."))
        {
            _repositorio.EliminarCliente(id);
            MensajeExito("Cliente eliminado.");
        }
    }

    private void CrearMascota()
    {
        MostrarTabla(_repositorio.ObtenerClientes(), "CLIENTES REGISTRADOS");
        _repositorio.CrearMascota(LeerMascota());
        MensajeExito("Mascota registrada correctamente.");
    }

    private void ListarMascotas() => MostrarTabla(_repositorio.ObtenerMascotas(), "MASCOTAS REGISTRADAS");

    private void ActualizarMascota()
    {
        ListarMascotas();
        var id = LeerId("Id de la mascota a actualizar");
        MostrarTabla(_repositorio.ObtenerClientes(), "CLIENTES REGISTRADOS");
        _repositorio.ActualizarMascota(id, LeerMascota());
        MensajeExito("Mascota actualizada correctamente.");
    }

    private void EliminarMascota()
    {
        ListarMascotas();
        var id = LeerId("Id de la mascota a eliminar");
        if (ConfirmarAccion("¿Está seguro de eliminar esta mascota?"))
        {
            _repositorio.EliminarMascota(id);
            MensajeExito("Mascota eliminada.");
        }
    }

    private void CrearVeterinario()
    {
        _repositorio.CrearVeterinario(LeerVeterinario());
        MensajeExito("Veterinario registrado correctamente.");
    }

    private void ListarVeterinarios() => MostrarTabla(_repositorio.ObtenerVeterinarios(), "VETERINARIOS REGISTRADOS");

    private void ActualizarVeterinario()
    {
        ListarVeterinarios();
        var id = LeerId("Id del veterinario a actualizar");
        _repositorio.ActualizarVeterinario(id, LeerVeterinario());
        MensajeExito("Veterinario actualizado correctamente.");
    }

    private void EliminarVeterinario()
    {
        ListarVeterinarios();
        var id = LeerId("Id del veterinario a eliminar");
        if (ConfirmarAccion("¿Está seguro de eliminar este veterinario?"))
        {
            _repositorio.EliminarVeterinario(id);
            MensajeExito("Veterinario eliminado.");
        }
    }

    private void CrearCita()
    {
        MostrarTabla(_repositorio.ObtenerMascotas(), "MASCOTAS");
        MostrarTabla(_repositorio.ObtenerVeterinarios(), "VETERINARIOS");

        var cita = new Cita
        {
            MascotaId = LeerId("Id de la mascota"),
            VeterinarioId = LeerId("Id del veterinario"),
            Fecha = LeerFechaHora("Fecha y Hora (AAAA-MM-DD HH:MM)"),
            Motivo = LeerTexto("Motivo de la consulta"),
            Estado = "Pendiente"
        };
        _repositorio.CrearCita(cita);
        MensajeExito("Cita programada correctamente.");
    }

    private void ListarCitas() => MostrarTabla(_repositorio.ObtenerCitas(), "CITAS PROGRAMADAS");

    private void ActualizarEstadoCita()
    {
        ListarCitas();
        var id = LeerId("Id de la cita");
        var nuevoEstado = LeerTexto("Nuevo Estado (Atendido / Cancelado / Pendiente)");
        _repositorio.ActualizarEstadoCita(id, nuevoEstado);
        MensajeExito("Estado de la cita actualizado.");
    }

    private void EliminarCita()
    {
        ListarCitas();
        var id = LeerId("Id de la cita a eliminar");
        if (ConfirmarAccion("¿Está seguro de cancelar/eliminar esta cita?"))
        {
            _repositorio.EliminarCita(id);
            MensajeExito("Cita eliminada.");
        }
    }

    private void CrearHistorial()
    {
        MostrarTabla(_repositorio.ObtenerMascotas(), "MASCOTAS");
        MostrarTabla(_repositorio.ObtenerVeterinarios(), "VETERINARIOS");

        var h = new HistorialClinico
        {
            MascotaId = LeerId("Id de la mascota"),
            VeterinarioId = LeerId("Id del veterinario"),
            Fecha = DateTime.Now,
            Diagnostico = LeerTexto("Diagnóstico médico"),
            Tratamiento = LeerTexto("Tratamiento recetado")
        };
        _repositorio.CrearHistorial(h);
        MensajeExito("Entrada de historial clínico guardada.");
    }

    private void ListarHistoriales() => MostrarTabla(_repositorio.ObtenerHistoriales(), "HISTORIALES CLÍNICOS");

    private void EliminarHistorial()
    {
        ListarHistoriales();
        var id = LeerId("Id del registro a eliminar");
        if (ConfirmarAccion("¿Está seguro de eliminar este registro del historial?"))
        {
            _repositorio.EliminarHistorial(id);
            MensajeExito("Registro eliminado.");
        }
    }

    // ==========================================
    // CAPTURA DE DATOS Y AYUDANTES VISUALES
    // ==========================================
    private static Cliente LeerCliente() => new()
    {
        Nombre = LeerTexto("Nombre completo"),
        Telefono = LeerTexto("Teléfono"),
        Correo = LeerTexto("Correo electrónico"),
        Direccion = LeerTexto("Dirección")
    };

    private static Mascota LeerMascota() => new()
    {
        Nombre = LeerTexto("Nombre de la mascota"),
        Especie = LeerTexto("Especie (Ej. Perro, Gato)"),
        Raza = LeerTexto("Raza"),
        Edad = LeerEntero("Edad (en años)"),
        ClienteId = LeerId("Id del propietario")
    };

    private static Veterinario LeerVeterinario() => new()
    {
        Nombre = LeerTexto("Nombre del veterinario"),
        Especialidad = LeerTexto("Especialidad"),
        Telefono = LeerTexto("Teléfono")
    };

    private static int LeerId(string mensaje) => LeerEntero(mensaje);

    private static int LeerEntero(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        if (!int.TryParse(Console.ReadLine(), out var valor))
            throw new ArgumentException("Ingrese un número entero válido.");
        return valor;
    }

    private static DateTime LeerFechaHora(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        if (!DateTime.TryParse(Console.ReadLine(), out var fecha))
            throw new ArgumentException("Ingrese una fecha válida (ej. 2026-10-25 14:30).");
        return fecha;
    }

    private static string LeerTexto(string mensaje)
    {
        Console.Write($"{mensaje}: ");
        var texto = Console.ReadLine();
        return string.IsNullOrWhiteSpace(texto)
            ? throw new ArgumentException($"El campo '{mensaje}' es obligatorio.")
            : texto.Trim();
    }

    private static bool ConfirmarAccion(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"\n{mensaje} (S/N): ");
        Console.ResetColor();
        var respuesta = Console.ReadLine()?.Trim().ToUpper();
        return respuesta == "S";
    }

    private static void MostrarEncabezado(string titulo)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================");
        Console.WriteLine($"  {titulo}");
        Console.WriteLine("=========================================================");
        Console.ResetColor();
    }

    private static void MensajeExito(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✓ {mensaje}");
        Console.ResetColor();
        Pausar();
    }

    private static void MensajeError(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n✗ {mensaje}");
        Console.ResetColor();
        Pausar();
    }

    private static void MostrarTabla(DataTable tabla, string titulo)
    {
        Console.WriteLine($"\n--- {titulo} ---");
        if (tabla.Rows.Count == 0)
        {
            Console.WriteLine("No hay registros en la base de datos.");
            return;
        }

        foreach (DataColumn columna in tabla.Columns) 
            Console.Write($"{columna.ColumnName,-18}");
        Console.WriteLine();
        Console.WriteLine(new string('-', tabla.Columns.Count * 18));

        foreach (DataRow fila in tabla.Rows)
        {
            foreach (var valor in fila.ItemArray) 
                Console.Write($"{valor,-18}");
            Console.WriteLine();
        }
    }

    private static void Pausar()
    {
        if (Console.IsInputRedirected) return;
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}