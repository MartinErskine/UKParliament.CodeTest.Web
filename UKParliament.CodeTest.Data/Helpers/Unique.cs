using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data.Helpers
{
    public class Unique : ValidationAttribute
    {
        private readonly string _propertyNames;

        public Unique(string propertyNames)
        {
            PropertyNames = propertyNames;
        }

        public string PropertyNames { get; private set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var className = validationContext.ObjectType.Name.Split('.').Last();
            var propertyName = validationContext.MemberName;
            var parameterName = string.Format("@{0}", propertyName);

            using var context = new RoomBookingsContext();

            var result = context.People.FromSqlRaw(string.Format("SELECT COUNT(*) FROM {0} WHERE {1}={2}",
                className, propertyName, parameterName)).ToList();

            //if (result[0] > 0)
            //{
            //    return new ValidationResult(string.Format("The '{0}' already exist", propertyName),
            //        new List<string>() { propertyName });
            //}

            return null;
        }
    }
}