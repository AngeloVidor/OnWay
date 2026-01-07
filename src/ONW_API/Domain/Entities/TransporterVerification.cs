using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Domain.Entities
{
    public sealed class TransporterVerification
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public Password Password { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        protected TransporterVerification() { }

        private TransporterVerification(
            string name,
            Email email,
            PhoneNumber phone,
            Password password)
        {
            Id = Guid.NewGuid();
            Code = GenerateCode();

            Name = name;
            Email = email;
            Phone = phone;
            Password = password;

            ExpiresAt = DateTime.UtcNow.AddMinutes(15);
        }

        public static TransporterVerification Create(
            string name,
            Email email,
            PhoneNumber phone,
            Password password)
        {
            return new TransporterVerification(name, email, phone, password);
        }

        public bool IsExpired()
            => DateTime.UtcNow > ExpiresAt;

        private static string GenerateCode()
            => Random.Shared.Next(100000, 999999).ToString();
    }
}