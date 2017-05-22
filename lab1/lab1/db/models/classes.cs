namespace lab1.db.models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Linq;

    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [Required]
        [StringLength(50)]
        public string mail { get; set; }

        public bool Check()
        {
            var validationResult = true;

            var mailValidation = new MailValueValidationRule();
            var loginValidation = new LoginValueValidationRule();
            var passwordValidation = new PasswordValueValidationRule();

            validationResult &= mailValidation.Validate(mail, null).IsValid;
            validationResult &= loginValidation.Validate(username, null).IsValid;
            validationResult &= passwordValidation.Validate(password, null).IsValid;

            return validationResult;
        }

    }
    public partial class DataBaseModel : DbContext
    {
        public DataBaseModel()
            : base("name=DataBaseConnection")
        {
        }

        public virtual DbSet<User> userdata { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.mail)
                .IsUnicode(false);
        }
    }
}
