using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Data;
using CordiSimple.Models;

namespace CordiSimple.Seeders
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            if (_context.Doctors.Any() || _context.Users.Any() || _context.Appointments.Any())
                return; // Si ya hay datos, no se siembra de nuevo

            // Crear doctores (mínimo 15 doctores, algunos pueden repetirse pero no las fechas)
            var doctors = new List<Doctor>();
            for (int i = 1; i <= 15; i++)
            {
                doctors.Add(new Doctor
                {
                    Name = $"Doctor {i}",
                    Email = $"doctor{i}@gmail.com",
                    Date = DateTime.Now.AddMonths(-i), // Simulamos fechas previas
                    Available = i % 2 == 0, // Algunos están disponibles, otros no
                    Speciality = $"Especialidad {i}",
                    Appointments = new List<Appointment>() // Inicializamos la lista de citas
                });
            }

            // Crear usuarios (mínimo 6 usuarios, 1 es administrador)
            var users = new List<User>();
            for (int i = 1; i <= 6; i++)
            {
                users.Add(new User
                {
                    Name = $"User {i}",
                    Email = $"user{i}@gmail.com",
                    Password = "password123", // En un caso real deberías cifrar la contraseña
                    Role = i == 1 ? UserRole.ADMIN : UserRole.USER, // El primer usuario es admin
                    Appointments = new List<Appointment>() // Inicializamos la lista de citas
                });
            }

            // Crear citas (mínimo 20 citas, repartidas entre los usuarios)
            var appointments = new List<Appointment>();
            var random = new Random();
            for (int i = 0; i < 20; i++)
            {
                var user = users[random.Next(0, users.Count)];
                var doctor = doctors[random.Next(0, doctors.Count)];

                // Si el usuario es admin, no se le asignan citas
                if (user.Role == UserRole.ADMIN) continue;

                // Crear una cita con fecha única (sin repetir con otro doctor)
                appointments.Add(new Appointment
                {
                    UserId = user.Id,
                    DoctorId = doctor.Id,
                    Description = $"Cita con {doctor.Name}",
                    Status = true, // Asumimos que la cita está confirmada
                    Date = DateTime.Now.AddDays(random.Next(1, 30)), // Citas dentro del próximo mes
                    Notes = $"Notas de la cita {i}",
                    User = user,
                    Doctor = doctor
                });
            }

            // Agregar los doctores, usuarios y citas al contexto
            await _context.Doctors.AddRangeAsync(doctors);
            await _context.Users.AddRangeAsync(users);
            await _context.Appointments.AddRangeAsync(appointments);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
    }
}