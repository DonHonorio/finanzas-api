using Microsoft.EntityFrameworkCore;
using FinanzasApp.Models; // Asegúrate de ajustar el namespace según donde tengas tus modelos

namespace FinanzasApp.Data
{
    public class FinanzasContext : DbContext
    {
        // Constructor que recibe las opciones configuradas en Program.cs o Startup.cs
        public FinanzasContext(DbContextOptions<FinanzasContext> options)
            : base(options)
        {
        }

        // Propiedad DbSet para la entidad Transaction (transacciones)
        public DbSet<Transaction> Transactions { get; set; }

        // Puedes declarar aquí otros DbSet para las demás entidades, por ejemplo:
        // public DbSet<BankAccount> BankAccounts { get; set; }
        // public DbSet<Category> Categories { get; set; }
        // public DbSet<Tag> Tags { get; set; }
        // public DbSet<Eventuality> Eventualities { get; set; }
        // ... etc.
    }
}
